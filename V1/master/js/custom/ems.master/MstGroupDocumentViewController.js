(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstGroupDocumentViewController', MstGroupDocumentViewController);

    MstGroupDocumentViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstGroupDocumentViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstGroupDocumentViewController';
        $scope.groupdocument_gid = $location.search().lsgroupdocument_gid;
        var groupdocument_gid = $scope.groupdocument_gid;

        activate();

        function activate() {
            $scope.IsLogShow = false; 
            var param = {

                groupdocument_gid: groupdocument_gid
            };
            var url = 'api/MstApplication360/GetGroupCheckListEditList';

            SocketService.getparams(url, param).then(function (resp) {
                $scope.checklistview_list = resp.data.checklist_list;
                unlockUI();
            });
            var params = {
                groupdocument_gid: groupdocument_gid
            }
            var url = 'api/MstApplication360/EditGroupDocument';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lblgroupdocument_name = resp.data.groupdocument_name;
                $scope.lbldocument = resp.data.documenttype_name;
                $scope.lbldocumentseverity = resp.data.documentseverity_name;
                $scope.lbllms_code = resp.data.lms_code;
                $scope.lblbureau_code = resp.data.bureau_code;
                $scope.lblCovenanttype = (resp.data.covenant_type == 'N') ? 'No' : 'Yes';
                $scope.lbldocumentcode = resp.data.document_code;
                $scope.lbldisplayorder = resp.data.display_order;
                $scope.lblremarks = resp.data.document_remarks; 
                $scope.lblcreated_date = resp.data.created_date;
                $scope.lblcreated_by = resp.data.created_by;
                $scope.lblstatus = (resp.data.Status == 'N') ? 'No' : 'Yes';
                $scope.lblcheck_point_count = resp.data.check_list;
                $scope.CboProgram_list = resp.data.CboProgram_list;              

                unlockUI();
            });
        }

        $scope.Back = function () {
            $state.go('app.MstGroupDocument');

        }
        $scope.logdetails=function() {

            if($scope.IsLogShow == true) {
                $scope.IsLogShow = false; 
            }
            else {
                
                $scope.IsLogShow=true;
                
               var url = 'api/MstApplication360/GetGroupdocumentupdatelog';
        
               var params = {
                groupdocument_gid: groupdocument_gid
                };
            
                SocketService.getparams(url,params).then(function (resp) {
                    if(resp.data.status==true){
                        $scope.documentupdatelog_list = resp.data.documentupdatelog_list;
                    }
                    else{
                       
                    }
                    
                }); 
            }
        }
    }
})();
