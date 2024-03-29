export const environment = {
  production: true,
  // SignalR
  notificationHubUrl: '/notifications',

  // notifications
  notificationsApiUrl: `notifications-module/`,
  getNotificationsUrl: `notifications-module/notifications`,

  // users
  usersApiUrl: `users-module/`,
  getUserByIdUrl: `users-module/users/`,
  getUsersUrl: `users-module/users/users`,
  getLoggedUserUrl: `users-module/users/me`,
  postUserUrl: `users-module/users/users`,

  signUpUrl: `users-module/users/sign-up`,
  signInUrl: `users-module/users/sign-in`,
  signOutUrl: `users-module/users/sign-out`,

  // vehicles
  vehiclesApiUrl: `vehicles-module/`,

  // customers
  customersApiUrl: `customers-module/`,
  getCustomerUrl: `customers-module/customers/`,
  updateCustomerUrl: `customers-module/customers/`,

  // reservations
  reservationsApiUrl: `reservations-module/`,

  baseUrl: 'https://localhost:7283/',
};
