var baseConfig = {
    baseUrl: 'http://localhost:7777',  // backend server base address
    endpoint: null,  // use Aurelia HttpClient instead of 'aurelia-api' client
    configureEndpoints: [],
    loginRedirect: '#/customer',
    logoutRedirect: '#/',
    loginRoute: '/login',
    loginOnSignup: true,
    providers: {
        linkedin: {
            url: 'api/Account/LoginLinkedin',
            clientId: '77d08hsurfp6gr',
            scope: ['r_emailaddress', 'r_basicprofile'],
            state: 'll'
        }
    }
};

export default baseConfig;