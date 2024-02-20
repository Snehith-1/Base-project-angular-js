
'use strict'
angular
    .module('angle')
.factory('PreviewdocumentService', ['SocketService', 'apiManage', '$state', '$cookieStore', 'ngDialog',
    function (SocketService, apiManage, $state, $cookieStore, ngDialog) {
        var csfactory = {};
        csfactory.PreviewPopUp = function (url) {
            ngDialog.open({
                closeByDocument: false,
                plain: false,
                template: 'app/views/preview.html',
                controller: ['$scope', 'ScopeValueService', function ($scope, ScopeValueService) {
                    function pop() {                        
                        $scope.pdf_file_url = url;
                    }
                    pop();

                }],
                closeButton: true,
            });
        }
        return csfactory;

    }]);