(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMstDocTemplateController', idasMstDocTemplateController);

        idasMstDocTemplateController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function idasMstDocTemplateController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {

        $scope.title = 'idasMstDocTemplateController';
        var vm = this;
        $scope.documentlist_gid = $location.search().documentlist_gid;
        activate();
       
        function activate() {
            var params = {
                doclist_gid: $scope.documentlist_gid
            }
            var url = 'api/IdasMstDocList/GetEditDocList';
            SocketService.getparams(url, params).then(function (resp) {
               
                $scope.documentNameEdit = resp.data.document_name;
                $scope.documentCodeEdit = resp.data.document_code;
                $scope.template_contentEdit = resp.data.template_content;
            });
        };

        $scope.updateTemplate = function () {
            if($scope.template_contentEdit == '' || $scope.template_contentEdit == null || $scope.template_contentEdit == undefined){
                Notify.alert('Kindly Enter Template Content', 'warning');
            }
            else{
                var params = {
                    template_content: $scope.template_contentEdit,
                    documentlist_gid: $scope.documentlist_gid
                }
                var url = 'api/IdasMstDocList/PostDocTemplate';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $state.go('app.idasMstDocList');
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
    
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'Warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
    
                    }
                });
            }
        }

        $scope.back = function () {
            $state.go('app.idasMstDocList');

        }
    }
})();