(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnDeploymentAddController', sdcTrnDeploymentAddController);

    sdcTrnDeploymentAddController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnDeploymentAddController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnDeploymentAddController';

        activate();

        function activate() {

            var url = 'api/SdcMstModule/GetModuleSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.modulemaster_summary = resp.data.moduledtl;
                unlockUI();
            });

            var url = 'api/SdcTrnTestDeployment/TmpDocDelete';
            lockUI();
            SocketService.get(url).then(function (resp) {
                
                unlockUI();
            });
        }

        $scope.onselectednp_yes = function () {
            if ($scope.newpage == 'Yes') {
               
                $scope.new_pages = true;
                $scope.no_pages = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_pages = false;
                $scope.no_pages = false;
                $scope.new_row = true;

            }
           
        }
        // For New DLL
        $scope.onselectedDll_yes = function () {
            if ($scope.newdll == 'Yes') {
                $scope.new_dll = true;
                $scope.new_row = true;
            }
            else
            {
                $scope.new_dll = false;
                $scope.new_row = false;
            }

            

        }

        // For New Dependency
        $scope.onselectedDep_yes = function () {
            if ($scope.newDependency == 'Yes') {
                $scope.new_dependency = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dependency = false;
                $scope.new_row = false;
            }

            if ($scope.newdll == 'Yes') {
                $scope.new_dll = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dll = false;
                $scope.new_row = false;
            }

        }
        $scope.onselectednr_yes = function () {
            if ($scope.newReports == 'Yes') {

                $scope.new_reports = true;
                $scope.no_reports = true;
                $scope.row2_details = true;
            }
            else {
                $scope.new_reports = false;
                $scope.no_reports = false;
                $scope.row2_details = false;

            }

        }

        //$scope.onselectednw_yes = function () {
        //    if ($scope.newWebconfig == 'Yes') {

        //        $scope.new_webconfig = true;
                
        //    }
        //    else {
        //        $scope.new_webconfig = false;
                
        //    }

        //}

        $scope.onselectedjs_yes = function () {
 
            if ($scope.newjs == 'Yes') {

                $scope.app_js = true;
                $scope.row2_details = true;

            }
            else {
                $scope.app_js = false;
                $scope.row2_details = false;
            }

            if ($scope.newReports == 'Yes') {

                $scope.new_reports = true;
                $scope.row2_details = true;
            }
            else {
                $scope.new_reports = false;
                $scope.row2_details = false;

            }

        }

        $scope.onselectedscript_yes = function () {
           
            if ($scope.script == 'Yes') {

                $scope.upload_script = true;
            }
            else {
                $scope.upload_script = false;
           
            }

        }

        $scope.jsupload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };

            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            $scope.uploadfrm = frm;

            var url = 'api/SdcTrnTestDeployment/Uploadjsdocument';

            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.jsfilename_list = resp.data.uploadjs_list;

                $("#uploadjs").val('');
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }

            });

        }

        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };

            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            $scope.uploadfrm = frm;

            var url = 'api/SdcTrnTestDeployment/Uploaddocument';

            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.upload_list;

                $("#addupload").val('');
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }

            });

        }
        $scope.versionupload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };

            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            $scope.uploadfrm = frm;

            var url = 'api/SdcTrnTestDeployment/Versionuploaddocument';

            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.versionfilename_list = resp.data.versionupload_list;

                $("#versionaddupload").val('');
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }

            });

        }

        $scope.jsdocument_cancelclick = function (val, data) {
            var params = { uploaddocument_gid: val };
            var url = 'api/SdcTrnTestDeployment/GettmpJsDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.jsfilename_list = resp.data.uploadjs_list;

                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }

        $scope.document_cancelclick = function (val, data) {
            var params = { uploaddocument_gid: val };
            var url = 'api/SdcTrnTestDeployment/GettmpDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.filename_list = resp.data.upload_list;

                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }
        $scope.versiondocument_cancelclick = function (val, data) {
            var params = { uploaddocument_gid: val };
            var url = 'api/SdcTrnTestDeployment/GetversiontmpDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.versionfilename_list = resp.data.versionupload_list;

                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }

        $scope.submitDeployment = function () {
            lockUI();
            var params = {
                module_gid: $scope.cbomodule.module_gid,
                module_prefix: $scope.cbomodule.module_prefix,
                test_Objective: $scope.cboObjective,
                new_pages: $scope.newpage_names,
                new_reports: $scope.newreport_names,
                //new_webconfig: $scope.newwebconfig_names,
                test_description: $scope.test_description,
                file_description: $scope.cbofiles,
                customerdtl: $scope.cbocustomer,
                newdll_flag: $scope.newdll,
                newdll_name: $scope.newdll_name,
                dependency_name: $scope.dependency_name,
                appjs_text: $scope.appjs_text,
                script_flag: $scope.script,
                newjs_flag: $scope.newjs,
                newDependency_flag: $scope.newDependency,
                newpage_flag: $scope.newpage,
                newReports_flag: $scope.newReports,
            }
            //console.log(params);
            
            var url = "api/SdcTrnTestDeployment/PostAddTestDeployment"
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.sdcTrnDeploymentSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });

        }
        $scope.cboChange = function (val)
        {
            var params= {
                module_gid : val
            }

            var url = 'api/SdcMstModule/GetModule2Customer';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.module2customerdtl = resp.data.moduledtl;
            });

        }

        $scope.back = function () {
            $state.go('app.sdcTrnDeploymentSummary');
        }
    }
})();
