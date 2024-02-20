(function () {
    'use strict';
    angular
        .module('angle')
        .controller('AtmTrnSamplingViewController', AtmTrnSamplingViewController);

    AtmTrnSamplingViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmTrnSamplingViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnSamplingViewController';

        var sampleimport_gid = $location.search().sampleimport_gid;
        $scope.auditcreation_gid = $location.search().auditcreation_gid;
        var auditcreation_gid = $location.search().auditcreation_gid;
        activate();
        function activate() {
            var params = {
                auditcreation_gid: auditcreation_gid,
                sampleimport_gid: sampleimport_gid
            };
            //var url = 'api/AtmTrnSampling/GetSampleAuditor';
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI()
            //    $scope.sample_list = resp.data.sample_list

            //});


            //var url = 'api/AtmTrnSampling/GetSampleDynamicdata';
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI()
            //    if (resp.data.status == true) {
            //        $scope.SampleDynamicdata = JSON.parse(resp.data.JSONdata);
            //        $scope.getsampledata = $scope.SampleDynamicdata.filter(a => a.sampleimport_gid === sampleimport_gid);
            //        var test = [];
            //        var res = [];
            //        $scope.getsampledata.forEach(element => {
            //            element.forEach(el => {
            //                res.push(el);
            //            });
            //        }); 
            //        console.log( $scope.getsampledata,simplifyArray);
            //    }  
            //});

            var url = 'api/AtmTrnSampling/GetEmployeeName';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.employee_name = resp.data.employee_name;
            });

            var url = 'api/AtmTrnSampling/GetSampleView';
            var params = {
                sampleimport_gid: sampleimport_gid,
                auditcreation_gid: auditcreation_gid
            }
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtsample_name = resp.data.sample_name,
                $scope.txtsamfin_code = resp.data.samfin_code,
                $scope.txtsamagro_code = resp.data.samagro_code,
                $scope.txtcodecreation_date = resp.data.codecreation_date,
                $scope.txtfield1 = resp.data.field1,
                $scope.txtfield2 = resp.data.field2,
                $scope.txtfield3 = resp.data.field3,
                $scope.txtfield4 = resp.data.field4,
                $scope.txtfield5 = resp.data.field5,
                $scope.txtfield6 = resp.data.field6,
                $scope.txtfield7 = resp.data.field7,
                $scope.txtfield8 = resp.data.field8,
                $scope.txtfield9 = resp.data.field9,
                $scope.txtfield10 = resp.data.field10,
                  unlockUI();
            });



        }

        $scope.back = function () {
            var auditcreation_gid = $scope.auditcreation_gid;
            $location.url('app/AtmTrnAuditorMakerSummary?auditcreation_gid=' + auditcreation_gid)

        }

        $scope.Back = function () {
            var auditcreation_gid = $scope.auditcreation_gid;
            $location.url('app/AtmTrnAuditCreationView?auditcreation_gid=' + auditcreation_gid)

        }

    }

})();