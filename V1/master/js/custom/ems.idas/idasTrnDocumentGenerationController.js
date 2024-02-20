(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnDocumentGenerationController', idasTrnDocumentGenerationController);

        idasTrnDocumentGenerationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function idasTrnDocumentGenerationController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {

        $scope.title = 'idasTrnDocumentGenerationController';
        var vm = this;
        $scope.documentlist_gid = $location.search().documentlist_gid;
        $scope.sanction_gid = $location.search().sanction_gid;
        $scope.document_code = $location.search().document_code;
        $scope.doctemplate_flag = $location.search().doctemplate_flag;
        var lspage = $location.search().lspage;

        var sanction_gid = $scope.sanction_gid;

        activate();
       
        function activate() {
            lockUI();
            if($scope.doctemplate_flag == 'Y')
            {
                var params = {
                    document_gid: $scope.documentlist_gid,
                    sanction_gid: $scope.sanction_gid
                }
                var url = 'api/IdasMstDocList/GetEditDoc2sanction';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.documentNameEdit = resp.data.document_name;
                    $scope.documentCodeEdit = resp.data.document_code;
                    $scope.template_content = resp.data.template_content;
                });
            }
           else{
                var params = {
                    doclist_gid: $scope.documentlist_gid
                }
                var url = 'api/IdasMstDocList/GetEditDocList';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.documentNameEdit = resp.data.document_name;
                    $scope.documentCodeEdit = resp.data.document_code;
                });

                var url = 'api/IdasMstDocList/GetDocTemplateContent';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.template_content = resp.data.template_content;
                    unlockUI();
                });
           }
        };

        $scope.generatedocument = function () {
            var param = {
                documentlist_gid: $scope.documentlist_gid,
                template_content: $scope.template_content,
                document_code: $scope.document_code,
                sanction_gid: $scope.sanction_gid,
            };
            var url = 'api/IdasMstDocList/PostDocTemplateGenerate';
            lockUI();
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if(resp.data.status == true){
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lspage == 'createprefil') {
                        $state.go('app.idasTrnPreFilManagement');
                    } else {
                        $location.url('app/idasTrnPreFilGeneration?sanction_gid=' + sanction_gid + '&lspage=' + lspage);
                    }      
                }
                else{
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.back = function () {
            if (lspage == 'createprefil') {
                $state.go('app.idasTrnPreFilManagement');
            } else {
                $location.url('app/idasTrnPreFilGeneration?sanction_gid=' + sanction_gid + '&lspage=' + lspage);
            }
        }
    }
})();