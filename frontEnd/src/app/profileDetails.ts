import {autoinject} from 'aurelia-framework';
import {UserProfileService, UserInfo} from 'api/userProfileService'

@autoinject
export class Welcome {
  private userProfileService: UserProfileService;
  private userInfo: UserInfo;

  constructor(userProfileService: UserProfileService) {
    this.userProfileService = userProfileService;
  }

  activate() {
    return this.userProfileService.getUserInfo().then(x => this.userInfo = x);
  }

  get displayName() {
    return this.userInfo.displayName;
  }
}
