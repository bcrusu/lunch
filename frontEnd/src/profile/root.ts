import {Router, RouterConfiguration} from 'aurelia-router'

export class Root {
  router: Router;

  constructor() {

  }

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Welcome';
    config.map([
      { route: ['', 'welcome'], name: 'welcome', moduleId: 'welcome', nav: true, title: 'Welcome' },
    ]);

    this.router = router;
  }


  canDeactivate() {
    return true;
  }
}
