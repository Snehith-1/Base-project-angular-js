
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADGroupAssignmentEditController', MstCADGroupAssignmentEditController);

        MstCADGroupAssignmentEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCADGroupAssignmentEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADGroupAssignmentEditController';
        $scope.cadgroupassign_gid = $location.search().cadgroupassign_gid;
        var cadgroupassign_gid = $scope.cadgroupassign_gid;

        activate();

        function activate() { 
            $scope.cbomaker_name=[];
            $scope.cbochecker_name=[];
            $scope.cboapprover_name=[];
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
           
            var params = {
                cadgroupassign_gid :cadgroupassign_gid
            }
            lockUI();
            var url = 'api/MstCADGroupAssignment/GetCADGroupAssignmentEdit';
            SocketService.getparams(url,params).then(function (resp){
                $scope.cbovertical_name =resp.data.vertical_name;
                $scope.cbovertical_name =resp.data.vertical_gid;
                $scope.cboprogram_name = resp.data.program_gid;
                $scope.cbocadgroup_name = resp.data.cadgroup_gid;
                //$scope.cbomenu_name =resp.data.menu_gid;
                //$scope.cbocheckeredit_list = resp.data.checker;
                //$scope.cboapprover_name = resp.data.approver_gid;
                //var size = resp.data.maker.length;
                //var size2 = resp.data.checker.length;
                //var size3 = resp.data.approver.length;
                //$scope.cbomakeredit_list=resp.data.maker;
                //$scope.cboapproveredit_list=resp.data.approver;
                var params ={
                   cadgroup_gid:  $scope.cbocadgroup_name    
                }
                var url = 'api/MstCADGroup/GetCADGroupEdit';
                lockUI();
                SocketService.getparams(url,params).then(function (resp) {
                    //$scope.maker_list = resp.data.cadmembers;
                    //$scope.checker_list = resp.data.cadmembers;
                    //$scope.approver_list = resp.data.cadmanager;
                    
                    //if ($scope.cbomakeredit_list != null) {
                    //    var count = size;
                    //     for (var i = 0; i < count; i++) {
                    //         var indexs = $scope.maker_list.findIndex(x => x.employee_gid ===  $scope.cbomakeredit_list[i].employee_gid);
                    //         $scope.cbomaker_name.push($scope.maker_list[indexs]);
                    //         $scope.$parent.cbomaker_name = $scope.cbomaker_name;
                    //    }
                    //}
                    //if ($scope.cbocheckeredit_list != null) {
                    //    var count = size2;
                    //    for (var i = 0; i < count; i++) {
                    //        var indexs = $scope.checker_list.findIndex(x => x.employee_gid === $scope.cbocheckeredit_list[i].employee_gid);
                    //        $scope.cbochecker_name.push($scope.checker_list[indexs]);
                    //        $scope.$parent.cbochecker_name = $scope.cbochecker_name;
                    //    }
                    //}
                    //if ($scope.cboapproveredit_list != null) {
                    //    var count = size3;
                    //    for (var i = 0; i < count; i++) {
                    //        var indexs = $scope.approver_list.findIndex(x => x.employee_gid === $scope.cboapproveredit_list[i].employee_gid);
                    //        $scope.cboapprover_name.push($scope.approver_list[indexs]);
                    //        $scope.$parent.cboapprover_name = $scope.cboapprover_name;
                    //    }
                    //}
                    //unlockUI();
                }); 
                unlockUI();
            });
                
        }
        //$scope.maker=function(){
        //    if ($scope.cbocadgroup_name != undefined || $scope.cbocadgroup_name != null) {
        //         var   lscadgroup_gid='';
        //         lscadgroup_gid = $scope.cbocadgroup_name;
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
        $scope.updateCADGroupSubmit = function (){
            var lsvertical_gid='';
            var lsvertical_name ='';
            var lsprogram_gid ='';
            var lsprogram_name ='';
            var lscadgroup_gid ='';
            var lscadgroup_name ='';
            //var lsmenu_gid ='';
            //var lsmenu_name ='';
            //var lsapprover_gid ='';
            //var lsapprover_name ='';

            var lsvertical_name = $('#vertical_name :selected').text();
             var lsprogram_name = $('#program_name :selected').text();
             var lscadgroup_name = $('#cadgroup_name :selected').text();
             //var lsmenu_name = $('#menu_name :selected').text();
             //var lsapprover_name = $('#approver_name :selected').text();

            if ($scope.cbovertical_name != undefined || $scope.cbovertical_name != null) {
                lsvertical_gid = $scope.cbovertical_name;
            }
            if ($scope.cboprogram_name != undefined || $scope.cboprogram_name != null) {
                lsprogram_gid = $scope.cboprogram_name;
            }
            if ($scope.cbocadgroup_name != undefined || $scope.cbocadgroup_name != null) {
                lscadgroup_gid = $scope.cbocadgroup_name;
            }
            //if ($scope.cbomenu_name != undefined || $scope.cbomenu_name != null) {
            //    lsmenu_gid = $scope.cbomenu_name;
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
                //maker:$scope.$parent.cbomaker_name,
                //checker:$scope.$parent.cbochecker_name,
                //approver_name:lsapprover_name,
                //approver_gid:lsapprover_gid,
                cadgroupassign_gid : cadgroupassign_gid
            }
            var url = 'api/MstCADGroupAssignment/CADGroupAssignedUpdate';
            SocketService.post(url,params).then(function (resp){
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