(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADGroupAssignmentAddController', MstCADGroupAssignmentAddController);

        MstCADGroupAssignmentAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCADGroupAssignmentAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADGroupAssignmentAddController';
       

        activate();

        function activate() { 
            var url = 'api/vertical/vertical'
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
                unlockUI();               
            });
            var url = 'api/MstApplication360/GetProgram';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.program_list = resp.data.application_list;
                unlockUI();
            });
            var url = 'api/MstCADGroup/GetCADGroup';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.cadgroup_list = resp.data.cadgroup;
                unlockUI();
            }); 
            var url = 'api/MstCADGroupAssignment/GetMenu';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.menu_list = resp.data.menu_list;
                unlockUI();
            });
        }
        //$scope.maker=function(){
        //    if ($scope.cbocadgroup_name != undefined || $scope.cbocadgroup_name != null) {
        //         var   lscadgroup_gid='';
        //         lscadgroup_gid = $scope.cbocadgroup_name.cadgroup_gid;
        //    }
        //    var params ={
        //        cadgroup_gid:  lscadgroup_gid     
        //    }
        //    var url = 'api/MstCADGroup/GetCADGroupEdit';
        //    lockUI();
        //    SocketService.getparams(url,params).then(function (resp) {
        //        $scope.maker_list = resp.data.cadmembers;
        //        $scope.checker_list = resp.data.cadmembers;
        //        $scope.approver_list = resp.data.cadmanager;
        //        unlockUI();
        //    }); 
        //}
        $scope.ok = function(){
            $location.url('app/MstCADGroupAssignmentSummary');
        }
        $scope.assignCADGroupSubmit = function(){
            var lsvertical_gid ='';
            var lsvertical_name ='';
            var lsprogram_gid ='';
            var lsprogram_name ='';
            var lscadgroup_gid ='';
            var lscadgroup_name ='';
            var lsmenu_gid ='';
            var lsmenu_name ='';
            //var lsmaker_gid ='';
            //var lschecker_gid ='';
            //var lsapprover_gid ='';
            //var lsapprover_name ='';

            if ($scope.cbovertical!=undefined|| $scope.cbovertical!=null)
            {
                 lsvertical_gid= $scope.cbovertical.vertical_gid;
                 lsvertical_name = $scope.cbovertical.vertical_name;
            }
            if ($scope.cboprogram!= undefined || $scope.cboprogram!= null) {
                lsprogram_gid = $scope.cboprogram.program_gid;
                lsprogram_name = $scope.cboprogram.program;
            }
            if ($scope.cbocadgroup_name != undefined || $scope.cbocadgroup_name != null) {
                lscadgroup_gid = $scope.cbocadgroup_name.cadgroup_gid;
                lscadgroup_name = $scope.cbocadgroup_name.cadgroup_name;
            }
            //if ($scope.cbomenu_name != undefined || $scope.cbomenu_name != null) {
            //    lsmenu_gid = $scope.cbomenu_name.menu_gid;
            //    lsmenu_name = $scope.cbomenu_name.menu_name;
            //}
            //if ($scope.cbomaker_name != undefined || $scope.cbomaker_name != null) {
            //    lsmaker_gid = $scope.cbomaker_name;
            //}
            //if ($scope.cbochecker_name != undefined || $scope.cbochecker_name != null) {
            //    lschecker_gid = $scope.cbochecker_name;
            //}
            //if ($scope.cboapprover_name != undefined || $scope.cboapprover_name != null) {
            //    lsapprover_gid = $scope.cboapprover_name;
            //}
            var  params = {
                vertical_gid: lsvertical_gid,
                vertical_name: lsvertical_name,
                program_name :lsprogram_name,
                program_gid :lsprogram_gid,
                cadgroup_name:lscadgroup_name,
                cadgroup_gid:lscadgroup_gid,
                //menu_name:lsmenu_name,
                //menu_gid:lsmenu_gid,
                //maker : $scope.cbomaker_name,
                //checker :$scope.cbochecker_name,       
                //approver:$scope.cboapprover_name
            }
            var url = 'api/MstCADGroupAssignment/PostCADGroupAssign';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $location.url('app/MstCADGroupAssignmentSummary');
            });
        }  
    }
})();