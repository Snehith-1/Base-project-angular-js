(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcMstModuleController', sdcMstModuleController);

    sdcMstModuleController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcMstModuleController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcMstModuleController';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/SdcMstModule/GetModuleSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.modulemaster_summary = resp.data.moduledtl;
                unlockUI();
            });
        }
        //$scope.loadMore = function (pagecount) {
        //    if (pagecount == undefined) {
        //        Notify.alert("Enter the Total Summary Count", "warning");
        //        return;
        //    }
        //    lockUI();

        //    var Number = parseInt(pagecount);
        //    // new code start
        //    if ($scope.modulemasterlist != null) {

        //        if (pagecount < $scope.modulemasterlist.length) {
        //            $scope.totalDisplayed += Number;
        //            if ($scope.modulemasterlist.length < $scope.totalDisplayed) {
        //                $scope.totalDisplayed = $scope.modulemasterlist.length;
        //                Notify.alert(" Total Summary " + $scope.modulemasterlist.length + " Records Only", "warning");
        //            }
        //            unlockUI();
        //        }
        //        else {
        //            unlockUI();
        //            Notify.alert(" Total Summary " + $scope.modulemasterlist.length + " Records Only", "warning");
        //            return;
        //        }
        //    }
        //    // new code end
        //    unlockUI();
        //};
        // Add Code Starts
        $scope.popupmodule = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addModuleContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
              
                $scope.activitySubmit = function () {
                    lockUI();
                    var params = {
                        module_name: $scope.module_name,
                        module_prefix: $scope.module_prefix,
                        availability: $scope.availability,
                    }
                    //console.log(params);
                    var url = 'api/SdcMstModule/PostModuleAdd';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }

            }
        }
         //Add Code Ends

        // Edit Code Starts
        $scope.edit = function (module_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ModuleModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    module_gid: module_gid
                }
                var url = 'api/SdcMstModule/GetModuleView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.module_gid = resp.data.module_gid,
                    $scope.modulenameedit = resp.data.module_name,
                    $scope.module_codeedit = resp.data.module_code;
                    $scope.moduleprefixedit = resp.data.module_prefix;
                    $scope.availabilityedit = resp.data.availability;
                   
                });
                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.moduleUpdate = function () {
                    var params = {
                        module_gid: module_gid,
                        module_name: $scope.modulenameedit,
                        availability: $scope.availabilityedit,
                        module_prefix: $scope.moduleprefixedit
                    }
                    console.log(params);
                    var url = 'api/SdcMstModule/PostModuleUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {

                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }
        // Edit Code Ends

        // Delete Code Starts
        $scope.delete = function (val) {
            var params = {
                module_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/SdcMstModule/GetModuleDelete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });

                }

            });
        }
        // Delete Code End

        $scope.tagCustomer = function (module_gid) {
            $scope.module_gid = module_gid;
            $scope.module_gid = localStorage.setItem('module_gid', module_gid);
            $state.go('app.sdcMstTagCustomer');

        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcMstTagCustomerController', sdcMstTagCustomerController);

    sdcMstTagCustomerController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcMstTagCustomerController($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcMstTagCustomerController';

        activate();
        function activate() {

            //var url = 'api/SdcMstModule/GetCustomersList';
            //SocketService.get(url).then(function (resp) {
            //    $scope.customerlist = resp.data.customerlist;
            //});
             

            var params = {
                module_gid: localStorage.getItem('module_gid')
            };
            console.log(params);

            var url = 'api/SdcMstModule/GetCustomersList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerlist = resp.data.customerlist;
            });


            var url = 'api/SdcMstModule/GetModuleView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.module_gid = resp.data.module_gid,
                $scope.module_name = resp.data.module_name,
                $scope.module_code = resp.data.module_code;

            });
            //var url = 'api/ProductGroup/UnassignedVendor';
            //var param = {
            //    productgroup_gid: localStorage.getItem('productgroup_gid')
            //};
            //lockUI();
            //SocketService.getparams(url, param).then(function (resp) {
            //    unlockUI();
            //    $scope.UnassignedVendor_list = resp.data.UnassignedVendor_list;
            //});

            //var url = 'api/ProductGroup/EditProductGroup';
            //var param = {
            //    productgroup_gid: localStorage.getItem('productgroup_gid')
            //};

            //lockUI();
            //SocketService.getparams(url, param).then(function (resp) {
            //    $scope.productgroupdetails = resp.data;
            //    $scope.productgroup_code = resp.data.productgroup_code;
            //    $scope.productgroup_name = resp.data.productgroup_name;

            //    unlockUI();

            //});


        };



        $scope.checkall = function (selected) {
            //console.log(selected);
            angular.forEach($scope.customerlist, function (val) {
                val.checked = selected;
            });
        }

        $scope.back = function (val) {
            $state.go('app.sdcMstModuleSummary');
        }



        $scope.assign = function () {
            var assignList = [];

            angular.forEach($scope.customerlist, function (val) {

                if (val.checked == true) {
                    var customer_gid = val.customer_gid;
                    assignList.push(customer_gid);
                    console.log(assignList);
                }
            });
        
        


            var params = {
                customer_gid: assignList,
                module_gid: localStorage.getItem('module_gid')
                //productgroup_gid: $scope.productgroup_gid;
            }
            console.log(params);
            var url = 'api/SdcMstModule/PostCustomerAssign';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert('Customer Assigned Successfully..!!', 'success');
                    $state.go('app.sdcMstModuleSummary');
                }
                else
                {
                    Notify.alert('Select Atleast One..!!', 'warning')
                }

            });

        }

    }
})();
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnDeploymentSummaryController', sdcTrnDeploymentSummaryController);

    sdcTrnDeploymentSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnDeploymentSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcMstModuleController';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/SdcMstModule/GetModuleSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.modulemaster_summary = resp.data.moduledtl;
                unlockUI();
            });

            var url = 'api/SdcTrnTestDeployment/GetTestSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.testsummary_list = resp.data.testsummary_list;
                unlockUI();

            });

        }

        $scope.checkall = function (selected) {
           
            angular.forEach($scope.testsummary_list, function (val) {
                val.checked = selected;
            });
        }
       
        // Add Code Starts
        $scope.popupmodule = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addModuleContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.activitySubmit = function () {
                    lockUI();
                    var params = {
                        module_name: $scope.module_name,
                        module_prefix: $scope.module_prefix,
                        availability: $scope.availability,
                    }
             
                    var url = 'api/SdcMstModule/PostModuleAdd';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }

            }
        }
        //Add Code Ends

        // Edit Code Starts
        $scope.updateInProgressStatus = function (test_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/updateInprogressStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    test_gid: test_gid
                }
     
                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.cancel = function () {
                    modalInstance.close('closed');
                };
                $scope.InprogressStatus = function () {
                    var params = {
                        test_gid: test_gid,
                        test_status: $scope.status,
                    }
                    var url = 'api/SdcTrnTestDeployment/PostStatusUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {

                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }
        // Edit Code Ends


        // Update Deploy Status Code Starts
        $scope.updateDeployStatus = function (test_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/updateDeployStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    test_gid: test_gid
                }
           
                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.cancel = function () {
                    modalInstance.close('closed');
                };
                $scope.deployStatus = function () {
                    
                    if ($scope.mail == true)
                    {
                       var mail_flag = 'Y';
                    }
                    else
                    {
                       var mail_flag = 'N';
                    }
                    var params = {
                        test_gid: test_gid,
                        test_status: $scope.statusDeployed,
                        mail_flag: mail_flag,
                    }
                  
                    var url = 'api/SdcTrnTestDeployment/PostDeployStatusUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {

                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }






        $scope.demo1 = function () {
        
         
            
            var modalInstance = $modal.open({
                templateUrl: '/updateUatStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.cancel = function () {
                    modalInstance.close('closed');
                };

               

                $scope.TestStatus = function () {
                    angular.forEach($scope.testsummary_list, function (val) {

                        if (val.checked == true) {
                            var test_gid = val.test_gid;
                            uatList.push(test_gid);
                            
                        }
                    });
                    var params = {
                        test_gid: uatList,
                        //module_gid: localStorage.getItem('module_gid')
                        //productgroup_gid: $scope.productgroup_gid;
                    }

                    var url = "api/SdcTrnTestDeployment/GetMovetoUat";
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Done Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            unlockUI();
                        }
                    });
                }
            }
        
        };






        $scope.movetoUATAll = function () {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to move the files to UAT..!',
                //type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes..!',
                CancelButtonColor: '#DD6B55',
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();

                    var uatList = [];

                    angular.forEach($scope.testsummary_list, function (val) {

                        if (val.checked == true) {
                            var test_gid = val.test_gid;
                            uatList.push(test_gid);
                        }
                    });

                    if (uatList.length == 0)
                    {
                        Notify.alert('Select Atleast One Record!');
                        return false;
                        unlockUI();
                    }
                 
                    var params = {
                        test_gid: uatList,
                    }
                   
                    var url = "api/SdcTrnTestDeployment/GetMovetoUat";
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Done Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            unlockUI();
                        }
                    });
                    unlockUI();
                }
                

            }

            );
        };


        $scope.movetoUAT = function (val) {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to move the file to UAT..!',
                //type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes..!',
                CancelButtonColor: '#DD6B55',
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();

                    
                    var params = {
                        test_gid: [val],
                    }
                 

                    var url = "api/SdcTrnTestDeployment/GetMovetoUat";
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Done Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            unlockUI();
                        }
                    });

                }

            }

            );
        };

        // Delete Code Starts
        $scope.delete = function (val) {
            var params = {
                test_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/SdcTrnTestDeployment/TestDelete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });

                }

            });
        }
        // Delete Code End

        $scope.addDeployment = function () {

            $state.go('app.sdcTrnAddDeployment');

        }

        // View Code Starts
        $scope.view = function (test_gid) {

            localStorage.setItem('test_gid', test_gid)
            $location.url('app/sdcTrnTestDeploymentView');
        }

        $scope.view = function (test_gid) {

            localStorage.setItem('test_gid', test_gid)
            $location.url('app/sdcTrnTestDeploymentView');
        }
        $scope.testview = function (test_gid) {
            localStorage.setItem('test_gid', test_gid)
            $location.url('app/sdcTrnTestView');
        }
        
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnLiveDeploymentSummaryController', sdcTrnLiveDeploymentSummaryController);

    sdcTrnLiveDeploymentSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnLiveDeploymentSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnLiveDeploymentSummaryController';

        activate();

        function activate() {

            var url = 'api/SdcTrnLiveDeployment/GetLiveSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.livesummary_list = resp.data.livesummary_list;
                unlockUI();

            });

        }


        // Update Code Starts
        $scope.updateInProgressStatus = function (live_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/updateInprogressStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
              
                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.cancel = function () {
                    modalInstance.close('closed');
                };
                $scope.InprogressStatus = function () {
                    var params = {
                        live_gid: live_gid,
                        live_status: $scope.status,
                    }
                    var url = 'api/SdcTrnLiveDeployment/PostStatusUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {

                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }
        // Update Code Ends


        // Update Deploy Status Code Starts
        $scope.updateDeployStatus = function (live_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/updateDeployStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
               
                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.cancel = function () {
                    modalInstance.close('closed');
                };
                $scope.deployStatus = function () {

                    if ($scope.mail == true) {
                        var mail_flag = 'Y';
                    }
                    else {
                        var mail_flag = 'N';
                    }
                    var params = {
                        live_gid: live_gid,
                        live_status: $scope.statusDeployed,
                        mail_flag: mail_flag,
                    }
                    console.log(params);
                    var url = 'api/SdcTrnLiveDeployment/PostDeployStatusUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {

                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }


        $scope.viewLive = function (live_gid) {

            localStorage.setItem('live_gid', live_gid)
            $location.url('app/sdcTrnLiveDeploymentView');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnLiveDeploymentViewController', sdcTrnLiveDeploymentViewController);

    sdcTrnLiveDeploymentViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function sdcTrnLiveDeploymentViewController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnLiveDeploymentViewController';

        activate();


        function activate() {

            lockUI();
            var url = window.location.href;
            var relPath = url.split("lstab=");
            $scope.relpath1 = relPath[1];

            var url = "api/SdcTrnLiveDeployment/LiveDeploymentView"
            var param = {
                live_gid: localStorage.getItem('live_gid')
            };
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();

                $scope.module_prefix = resp.data.module_prefix;
                $scope.test_description = resp.data.test_description;
                $scope.new_pages = resp.data.new_pages;
                $scope.new_reports = resp.data.new_reports;
                $scope.test_Objective = resp.data.test_Objective;
                $scope.test_status = resp.data.test_status;
                $scope.newdll_name = resp.data.newdll_name;
                $scope.dependency_name = resp.data.dependency_name;
                $scope.appjs_text = resp.data.appjs_text;
                $scope.filedescription = resp.data.filedescription;
                $scope.script = resp.data.script;
                $scope.upload_list = resp.data.upload_list;
                $scope.uploadjs_list = resp.data.uploadjs_list;
                $scope.uploadversion_list = resp.data.uploadversion_list;
                $scope.filedesc_list = resp.data.filedesc_list;
                //if ($scope.script == 'Yes') {
                //    $scope.script_doc = true;
                //}
                //else {
                //    $scope.script_doc = false;
                //}
            });

        }
        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.LiveViewBack = function () {
            $state.go('app.sdcTrnLiveDeploymentSummary');
        }

        $scope.ViewBack = function () {
            $state.go('app.sdcTrnLiveSummary');
        }
    }

})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnLiveReportController', sdcTrnLiveReportController);

    sdcTrnLiveReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnLiveReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnLiveReportController';

        activate();

        function activate() {

            var url = 'api/SdcTrnReport/GetLiveSummaryReport';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.livesummary_list = resp.data.livesummary_list;
                unlockUI();

            });

        }

        $scope.export = function () {
            lockUI();

            var url = 'api/SdcTrnReport/GetLiveReportExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    var phyPath = resp.data.lspath;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = resp.data.lsname.split('.');
                    link.download = name[0];
                    var uri = str;
                    link.href = uri;
                    link.click();

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')
                    activate();

                }

            });
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnLiveSummaryController', sdcTrnLiveSummaryController);

    sdcTrnLiveSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnLiveSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnLiveSummaryController';

        activate();

        function activate() {

            var url = 'api/SdcTrnLiveDeployment/GetLiveSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.livesummary_list = resp.data.livesummary_list;
                console.log($scope.livesummary_list);
                unlockUI();

            });

        }


        // Add Code Starts
        $scope.popupmodule = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addModuleContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.activitySubmit = function () {
                    lockUI();
                    var params = {
                        module_name: $scope.module_name,
                        module_prefix: $scope.module_prefix,
                        availability: $scope.availability,
                    }
                    //console.log(params);
                    var url = 'api/SdcMstModule/PostModuleAdd';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }

            }
        }
        //Add Code Ends

   
        // Update Deploy Status Code Starts
        $scope.updateDeployStatus = function (test_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/updateDeployStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    test_gid: test_gid
                }

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.cancel = function () {
                    modalInstance.close('closed');
                };
                $scope.deployStatus = function () {

                    if ($scope.mail == true) {
                        var mail_flag = 'Y';
                    }
                    else {
                        var mail_flag = 'N';
                    }
                    var params = {
                        test_gid: test_gid,
                        test_status: $scope.statusDeployed,
                        mail_flag: mail_flag,
                    }
                    console.log(params);
                    var url = 'api/SdcTrnTestDeployment/PostDeployStatusUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {

                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }

        $scope.liveview = function (live_gid) {
            localStorage.setItem('live_gid', live_gid)
            $location.url('app/sdcTrnLiveView');
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnTestDeploymentViewController', sdcTrnTestDeploymentViewController);

    sdcTrnTestDeploymentViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function sdcTrnTestDeploymentViewController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnTestDeploymentViewController';

        activate();


        function activate() {

            lockUI();
            var url = window.location.href;
            var relPath = url.split("lstab=");
            $scope.relpath1 = relPath[1];
           
            var url = "api/SdcTrnTestDeployment/TestDeploymentView"
            var param = {
                test_gid: localStorage.getItem('test_gid')
            };
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();

                $scope.module_prefix = resp.data.module_prefix;
                $scope.test_description = resp.data.test_description;
                $scope.new_pages = resp.data.new_pages;
                $scope.new_reports = resp.data.new_reports;
                //$scope.new_webconfig = resp.data.new_webconfig;
                $scope.test_Objective = resp.data.test_Objective;
                $scope.test_status = resp.data.test_status;
                $scope.newdll_name = resp.data.newdll_name;
                $scope.dependency_name = resp.data.dependency_name;
                $scope.appjs_text = resp.data.appjs_text;
                $scope.filedescription = resp.data.filedescription;
                $scope.script = resp.data.script;
                //$scope.designation_name = resp.data.designation_name;
                //$scope.department_name = resp.data.department_name;
                //$scope.branch_name = resp.data.branch_name;
                //$scope.employee_photo = resp.data.employee_photo;
                //$scope.txtremarks = resp.data.remarks;
                //$scope.list = resp.data.document_list;
                $scope.upload_list = resp.data.upload_list;
                $scope.uploadjs_list = resp.data.uploadjs_list;
                $scope.versionupload_list = resp.data.versionupload_list;
                $scope.customer_list = resp.data.customer_list;
                console.log(params)
            });
           
        }
        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
           
            var link = document.createElement("a");
            var name = val2;
            link.download = name;
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.jsdownloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            console.log(relPath);
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
           
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.versiondocumentdownloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            console.log(relPath);
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.testViewBack = function () {
            $state.go('app.sdcTrnTestDeploymentSummary');
        }

        $scope.ViewBack = function () {
            $state.go('app.sdcTrnDeploymentSummary');
        }
    }

})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnTestReportController', sdcTrnTestReportController);

    sdcTrnTestReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnTestReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnTestReportController';

        activate();

        function activate() {
           
            var url = 'api/SdcTrnReport/GetTestSummaryReport';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.testsummary_list = resp.data.testsummary_list;
                unlockUI();

            });

        }

        $scope.export = function () {
            lockUI();

            var url = 'api/SdcTrnReport/GetTestReportExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    var phyPath = resp.data.lspath;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = resp.data.lsname.split('.');
                    link.download = name[0];
                    var uri = str;
                    link.href = uri;
                    link.click();

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')
                    activate();

                }

            });
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnUatDeploymentSummaryController', sdcTrnUatDeploymentSummaryController);

    sdcTrnUatDeploymentSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnUatDeploymentSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnUatDeploymentSummaryController';

        activate();

        function activate() {
          
            var url = 'api/SdcTrnUatDeployment/GetUatSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.uatsummary_list = resp.data.uatsummary_list;
                unlockUI();

            });

        }

      
        // Update Code Starts
        $scope.updateInProgressStatus = function (uat_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/updateInprogressStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    uat_gid: uat_gid
                }

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.cancel = function () {
                    modalInstance.close('closed');
                };
                $scope.InprogressStatus = function () {
                    var params = {
                        uat_gid: uat_gid,
                        uat_status: $scope.status,
                    }
                    var url = 'api/SdcTrnUatDeployment/PostStatusUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {

                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }
        // Update Code Ends


        // Update Deploy Status Code Starts
        $scope.updateDeployStatus = function (uat_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/updateDeployStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    uat_gid: uat_gid
                }

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.cancel = function () {
                    modalInstance.close('closed');
                };
                $scope.deployStatus = function () {

                    if ($scope.mail == true) {
                        var mail_flag = 'Y';
                    }
                    else {
                        var mail_flag = 'N';
                    }
                    var params = {
                        uat_gid: uat_gid,
                        uat_status: $scope.statusDeployed,
                        mail_flag: mail_flag,
                    }
                    console.log(params);
                    var url = 'api/SdcTrnUatDeployment/PostDeployStatusUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {

                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }






        $scope.demo1 = function () {



            var modalInstance = $modal.open({
                templateUrl: '/updateUatStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.cancel = function () {
                    modalInstance.close('closed');
                };



                $scope.TestStatus = function () {
                    angular.forEach($scope.testsummary_list, function (val) {

                        if (val.checked == true) {
                            var uat_gid = val.uat_gid;
                            uatList.push(uat_gid);
                            console.log(uatList);
                        }
                    });
                    var params = {
                        uat_gid: uatList,
                        //module_gid: localStorage.getItem('module_gid')
                        //productgroup_gid: $scope.productgroup_gid;
                    }

                    var url = "api/SdcTrnTestDeployment/GetMovetoUat";
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Done Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            unlockUI();
                        }
                    });
                }
            }

        };






        $scope.movetoUATAll = function () {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to move the files to UAT..!',
                //type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes..!',
                CancelButtonColor: '#DD6B55',
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();

                    var uatList = [];

                    angular.forEach($scope.testsummary_list, function (val) {

                        if (val.checked == true) {
                            var uat_gid = val.uat_gid;
                            uatList.push(uat_gid);
                        }
                    });

                    if (uatList.length == 0) {
                        Notify.alert('Select Atleast One Record!');
                        return false;
                        unlockUI();
                    }

                    var params = {
                        uat_gid: uatList,
                    }
                    console.log(params);
                    var url = "api/SdcTrnTestDeployment/GetMovetoUat";
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Done Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            unlockUI();
                        }
                    });
                    unlockUI();
                }


            }

            );
        };


        $scope.movetoUAT = function (val) {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to move the file to UAT..!',
                //type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes..!',
                CancelButtonColor: '#DD6B55',
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();


                    var params = {
                        uat_gid: [val],
                    }
                    console.log(params);

                    var url = "api/SdcTrnTestDeployment/GetMovetoUat";
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Done Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            unlockUI();
                        }
                    });

                }

            }

            );
        };

        // Delete Code Starts
        $scope.delete = function (val) {
            var params = {
                module_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/SdcMstModule/GetModuleDelete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });

                }

            });
        }
        // Delete Code End

        $scope.viewUat = function (uat_gid) {

            localStorage.setItem('uat_gid', uat_gid)
            $location.url('app/sdcTrnUatDeploymentView');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnUatDeploymentViewController', sdcTrnUatDeploymentViewController);

    sdcTrnUatDeploymentViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function sdcTrnUatDeploymentViewController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnUatDeploymentViewController';

        activate();


        function activate() {

            lockUI();
            var url = window.location.href;
            var relPath = url.split("lstab=");
            $scope.relpath1 = relPath[1];

            var url = "api/SdcTrnUatDeployment/UatDeploymentView";
            var param = {
                uat_gid: localStorage.getItem('uat_gid')
            };
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();

                $scope.module_prefix = resp.data.module_prefix;
                $scope.test_description = resp.data.test_description;
                $scope.new_pages = resp.data.new_pages;
                $scope.new_reports = resp.data.new_reports;
                $scope.test_Objective = resp.data.test_Objective;
                $scope.test_status = resp.data.test_status;
                $scope.newdll_name = resp.data.newdll_name;
                $scope.dependency_name = resp.data.dependency_name;
                $scope.appjs_text = resp.data.appjs_text;
                $scope.filedescription = resp.data.filedescription;
                $scope.script = resp.data.script;
                $scope.upload_list = resp.data.upload_list;
                $scope.uploadjs_list = resp.data.uploadjs_list;
                $scope.versionupload_list = resp.data.versionupload_list;
                $scope.filedesc_list = resp.data.filedesc_list;
            });

        }
        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }
        $scope.jsdownloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            console.log(relPath);
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.versiondocumentdownloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            console.log(relPath);
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }
        $scope.uatViewBack = function () {
            $state.go('app.sdcTrnUatDeploymentSummary');
        }

        $scope.ViewBack = function () {
            $state.go('app.sdcTrnUatSummary');
        }
    }

})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnUatReportController', sdcTrnUatReportController);

    sdcTrnUatReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnUatReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnUatReportController';

        activate();

        function activate() {

            var url = 'api/SdcTrnReport/GetUatSummaryReport';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.uatsummary_list = resp.data.uatsummary_list;
                unlockUI();

            });
        }
        $scope.export = function () {
            lockUI();

            var url = 'api/SdcTrnReport/GetUatReportExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    var phyPath = resp.data.lspath;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = resp.data.lsname.split('.');
                    link.download = name[0];
                    var uri = str;
                    link.href = uri;
                    link.click();

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')
                    activate();

                }

            });
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnUatSummaryController', sdcTrnUatSummaryController);

    sdcTrnUatSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnUatSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnUatSummaryController';

        activate();

        function activate() {
           
            var url = 'api/SdcTrnUatDeployment/GetUatSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.uatsummary_list = resp.data.uatsummary_list;
                unlockUI();

            });

        }

        $scope.checkall = function (selected) {
          
            angular.forEach($scope.uatsummary_list, function (val) {
                val.checked = selected;
            });
        }

        // Add Code Starts
        $scope.popupmodule = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addModuleContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.activitySubmit = function () {
                    lockUI();
                    var params = {
                        module_name: $scope.module_name,
                        module_prefix: $scope.module_prefix,
                        availability: $scope.availability,
                    }

                    var url = 'api/SdcMstModule/PostModuleAdd';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }

            }
        }
        //Add Code Ends

        $scope.movetoLive = function (val) {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to move the file to Live..!',
                //type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes..!',
                CancelButtonColor: '#DD6B55',
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();


                    var params = {
                        uat_gid: [val],
                    }
                  

                    var url = "api/SdcTrnUatDeployment/GetMovetoLive";
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Done Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            unlockUI();
                        }
                    });

                }

            }

            );
        };

        $scope.movetoLiveAll = function () {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to move the files to LIVE..!',
                //type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes..!',
                CancelButtonColor: '#DD6B55',
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();

                    var liveList = [];

                    angular.forEach($scope.uatsummary_list, function (val) {

                        if (val.checked == true) {
                            var uat_gid = val.uat_gid;
                            liveList.push(uat_gid);
                        }
                    });

                    if (liveList.length == 0) {
                        Notify.alert('Select Atleast One Record!');
                        return false;
                        unlockUI();
                    }

                    var params = {
                        uat_gid: liveList,
                    }
                   
                    var url = "api/SdcTrnUatDeployment/GetMovetoLive";
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Done Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            unlockUI();
                        }
                    });
                    unlockUI();
                }


            }

            );
        };

        // Update Deploy Status Code Starts
        $scope.updateDeployStatus = function (test_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/updateDeployStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    test_gid: test_gid
                }

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.cancel = function () {
                    modalInstance.close('closed');
                };
                $scope.deployStatus = function () {

                    if ($scope.mail == true) {
                        var mail_flag = 'Y';
                    }
                    else {
                        var mail_flag = 'N';
                    }
                    var params = {
                        test_gid: test_gid,
                        test_status: $scope.statusDeployed,
                        mail_flag: mail_flag,
                    }
                  
                    var url = 'api/SdcTrnTestDeployment/PostDeployStatusUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {

                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }


        $scope.uatview = function (uat_gid) {
            localStorage.setItem('uat_gid', uat_gid)
            $location.url('app/sdcTrnUatView');
        }

    }
})();
