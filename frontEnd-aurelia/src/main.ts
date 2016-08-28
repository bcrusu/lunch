import {Aurelia} from 'aurelia-framework';
import authConfig from 'authConfig';
import {BaseConfig} from 'aurelia-authentication'

export function configure(aurelia: Aurelia) {
  aurelia.use
    .standardConfiguration()
    .developmentLogging()
    .plugin('aurelia-authentication', (baseConfig: BaseConfig) => {
      baseConfig.configure(authConfig);      
    });

  //Uncomment the line below to enable animation.
  //aurelia.use.plugin('aurelia-animator-css');

  //Anyone wanting to use HTMLImports to load views, will need to install the following plugin.
  //aurelia.use.plugin('aurelia-html-import-template-loader')

  aurelia.start().then(() => aurelia.setRoot('publicRoot'));
}
