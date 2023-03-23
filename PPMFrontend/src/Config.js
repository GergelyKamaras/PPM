let authServicePort = "8000";
let apiServicePort = "7001";

// Endpoint list
export let registrationEndpoint = `:${authServicePort}/api/authentication/register`;
export let loginEndpoint = `:${authServicePort}/api/authentication/login`;

// Access keys for token headers
export const EMAIL_KEY = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
export const NAME_KEY = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
export const ROLE_KEY = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
