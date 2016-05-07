import {autoinject} from 'aurelia-framework';
import {AuthService} from 'aurelia-authentication';

@autoinject
export class Login {
  authenticated: boolean;
  authService: AuthService

  constructor(authService: AuthService) {
    this.authService = authService;
    this.authenticated = false;
  };

  // username/password login
  login(credentialsObject) {
    return this.authService.login(credentialsObject)
      .then(() => {
        this.authenticated = this.authService.isAuthenticated();
      });
  };

  logout() {
    return this.authService.logout()
      .then(() => {
        this.authenticated = this.authService.isAuthenticated();
      });
  }

  authenticateLinkedin() {
    return this.authService.authenticate('linkedin')
      .then(() => {
        this.authenticated = this.authService.isAuthenticated();
      });
  }
}
