/*!
 * 
 * vcx - Bootstrap Admin App + AngularJS Material
 * 
 * Version: 3.1.0
 * Author: @themicon_co
 * Website: http://themicon.co
 * License: https://wrapbootstrap.com/help/licenses
 * 
 */

// APP START
// ----------------------------------- 

(function() {
    'use strict';

    angular
        .module('vcx', [
            'app.core',
            'app.routes',
            'app.sidebar',
            'app.navsearch',
            'app.preloader',
            'app.loadingbar',
            'app.translate',
            'app.settings',
            'app.dashboard',
            'app.icons',
            'app.flatdoc',
            'app.notify',
            'app.bootstrapui',
            'app.elements',
            'app.panels',
            'app.charts',
            'app.forms',
            'app.locale',
            'app.maps',
            'app.pages',
            'app.tables',
            'app.extras',
            'app.mailbox',
            'app.utils',
            'app.material',
            'app.service'
        ]);
         angular.module('vcx').config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push(function ($location) {
            return {
                responseError: function (req) {
                    console.log(req);
                    $location.url('errorPage?errno=401');
                    return req;
                }     
            };
        });
    }]);
})();

