$outputFilePath = ".\nuget_dependencies.txt"

if (Test-Path $outputFilePath) {
    Remove-Item $outputFilePath
}

$csprojFiles = Get-ChildItem -Recurse -Filter "*.csproj"

$testDependencies = @{}
$nonTestDependencies = @{}
$projectSpecificDependencies = @{}
$projectIndex = 0

foreach ($file in $csprojFiles) {
    $projectIndex++
    [xml]$csprojContent = Get-Content $file.FullName
    $isTestProject = $file.Name -like "*tests*"
    $currentDependencies = @{}

    foreach ($packageReference in $csprojContent.Project.ItemGroup.PackageReference) {
        $dependencyName = if ($packageReference.Include) { $packageReference.Include } elseif ($packageReference.Update) { $packageReference.Update }
        $dependencyVersion = $packageReference.Version
        $dependencyKey = "$dependencyName, Version=$dependencyVersion"
        if ($dependencyName -and $dependencyVersion) {
            $currentDependencies[$dependencyKey] = $true
            if ($isTestProject) {
                $testDependencies[$dependencyKey] += 1
            }
            else {
                $nonTestDependencies[$dependencyKey] += 1
            }
        }
    }

    $projectSpecificDependencies["$projectIndex. $($file.Name)"] = @{
        IsTest       = $isTestProject
        Dependencies = $currentDependencies.Keys
    }
}

function Get-CommonDependencies ($dependencies, $projectCount) {
    return $dependencies.Keys | Where-Object { $dependencies[$_] -eq $projectCount }
}

$testProjectCount = ($projectSpecificDependencies.Values | Where-Object { $_.IsTest }).Count
$nonTestProjectCount = ($projectSpecificDependencies.Values | Where-Object { -not $_.IsTest }).Count

$commonTestDependencies = Get-CommonDependencies -dependencies $testDependencies -projectCount $testProjectCount
$commonNonTestDependencies = Get-CommonDependencies -dependencies $nonTestDependencies -projectCount $nonTestProjectCount

Add-Content -Path $outputFilePath -Value "Wspólne zależności dla projektów testowych:"
$commonTestDependencies | ForEach-Object { Add-Content -Path $outputFilePath -Value "    - $_" }

Add-Content -Path $outputFilePath -Value "`nWspólne zależności dla pozostałych projektów:"
$commonNonTestDependencies | ForEach-Object { Add-Content -Path $outputFilePath -Value "    - $_" }

foreach ($project in $projectSpecificDependencies.GetEnumerator()) {
    $uniqueDependencies = if ($project.Value.IsTest) {
        $project.Value.Dependencies | Where-Object { $_ -notin $commonTestDependencies }
    }
    else {
        $project.Value.Dependencies | Where-Object { $_ -notin $commonNonTestDependencies }
    }

    Add-Content -Path $outputFilePath -Value "`n$($project.Name) - Unikalne zależności:"
    if ($uniqueDependencies.Count -gt 0) {
        $uniqueDependencies | ForEach-Object { Add-Content -Path $outputFilePath -Value "    - $_" }
    }
    else {
        Add-Content -Path $outputFilePath -Value "    Brak unikalnych zależności"
    }
}
