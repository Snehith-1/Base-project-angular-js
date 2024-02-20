(function() {
    'use strict';

    angular
        .module('custom', [
            // request the the entire framework
            'vcx',
            // or just modules
            'app.core',
            'app.sidebar'
            /*...*/
        ]);
})();