import {autoinject} from 'aurelia-framework';
import {AuthService} from 'aurelia-authentication';
import {UserProfileService} from 'api/userProfileService'

@autoinject
export class Login {
  authenticated: boolean;
  authService: AuthService
  userProfileService: UserProfileService;

  constructor(authService: AuthService, userProfileService: UserProfileService) {
    this.authService = authService;
    this.authenticated = this.authService.isAuthenticated();
    this.userProfileService = userProfileService;
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

  testGetUserInfo() {
    if (!this.authenticated) {
      alert('no, no, no!');
      return;
    }
    
    return this.userProfileService.getUserInfo()
      .then(x => {
        debugger;
        alert(x.displayName);
        return x;
      })
      .catch(x => {
        debugger;
      });
  }
}
