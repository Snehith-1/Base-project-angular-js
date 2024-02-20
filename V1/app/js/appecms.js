(function () {
    'use strict';

    angular
        .module('angle')
        .controller('addcustomerController', addcustomerController);

    addcustomerController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function addcustomerController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'addcustomerController';
        activate();

        function activate() {         
            $scope.Warning = false;
            var url = 'api/customer/cMmail';
            SocketService.get(url).then(function (resp) {
                $scope.txtccmail = resp.data.ccmail;
            });

            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });
            var url = 'api/customer/Getconstitution';
            SocketService.get(url).then(function (resp) {
                $scope.constitution_list = resp.data.constitution_list;
                console.log(resp.data.constitution_list);
            });
            $scope.txtcountry = "India";
        }

        $scope.urnvalidation=function()
        {           
            var params =
                {
                    urn: $scope.txtcustomerurn,
                }
            var url = 'api/MstCustomerAdd/GetURNInfo';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.Warning = true;
                }
                else {
                    $scope.Warning = false;
                }
            });
        }
        $scope.customerback=function(val)
        {
            $state.go('app.customerMaster');
        }
        $scope.customerSubmit = function () {
            if ($scope.customer == undefined || $scope.customer == null || $scope.customer == "") {
                Notify.alert("Kindly check the customer", 'warning')

            }
            else {
                if ($scope.message == "You can't add this Customer. Tag the customer from master.")
                {
                    Notify.alert("You can't add this Customer. Tag the customer from master.", 'warning')
                }
                else{
                var params =
                    {
                        urn: $scope.txtcustomerurn,
                    }
                var url = 'api/MstCustomerAdd/GetURNInfo';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert('Already this URN is in Imported Customer', 'warning');
                    }
                    else {
                        var vertical_code = $('#vertical_code :selected').text();
                        var zonalHead_name = $('#zonalHead_name :selected').text();
                        var businessHead_name = $('#businessHead_name :selected').text();
                        var regionalHead_name = $('#regionalHead_name :selected').text();
                        var cluster_manager_name = $('#cluster_manager_name :selected').text();
                        var relationshipMgmt_name = $('#relationshipMgmt_name :selected').text();
                        var creditmanager_name = $('#creditmanager_name :selected').text();
                        var state_name = $('#statename :selected').text();
                        var zonlRM_name = $('#zonlRM_name :selected').text();
                        var riskmanager_name = $('#riskmanager_name :selected').text();
                        var RiskMonitoring_Name = $('#RiskMonitoring_Name :selected').text();

                        var params = {
                            vertical_gid: $scope.vertical,
                            vertical_code: vertical_code,
                            customercode: $scope.txtcustomercode,
                            //   customer_name: $scope.customer,
                            customername: $scope.customer,
                            contactperson: $scope.txtcontactperson,
                            customer_urn: $scope.txtcustomerurn,
                            contactnumber: $scope.txtcontactno,
                            mobileno: $scope.txtmobileno,
                            email: $scope.txtemail,
                            address1: $scope.txtaddress1,
                            //address2: $scope.txtaddress2,
                            region: $scope.txtregion,
                            address2: $scope.txtaddress2,
                            state_gid: $scope.state_gid,
                            state: state_name,
                            postalcode: $scope.txtpostalcode,
                            country: $scope.txtcountry,
                            tomail: $scope.txttomail,
                            ccmail: $scope.txtccmail,
                            zonalGid: $scope.zonalHead,
                            businessHeadGid: $scope.businessHead,
                            regionalHeadGid: $scope.regionalHead,
                            relationshipMgmtGid: $scope.relationshipMgmt,
                            clustermanagerGid: $scope.clustermanager,
                            creditmanagerGid: $scope.creditmanager,
                            zonal_name: zonalHead_name,
                            businesshead_name: businessHead_name,
                            regionalhead_name: regionalHead_name,
                            cluster_manager_name: cluster_manager_name,
                            relationshipmgmt_name: relationshipMgmt_name,
                            creditmanager_name: creditmanager_name,
                            gst_number: $scope.gst_number,
                            pan_number: $scope.pan_number,
                            constitution_name: $scope.cboconstitution.constitution_name,
                            constitution_gid: $scope.cboconstitution.constitution_gid,
                            major_corporate: $scope.txtmajor_corporate,
                            zonal_riskmanagerGID: $scope.zonalRM_GID,
                            zonal_riskmanagerName: zonlRM_name,
                            risk_managerGID: $scope.riskmanager_GID,
                            riskmanager_name: riskmanager_name,
                            riskMonitoring_GID: $scope.RiskMonitoring_GID,
                            riskMonitoring_Name: RiskMonitoring_Name
                        }
                        var url = 'api/customer/customerSubmit';
                        lockUI()
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI()
                                activate();
                                $state.go('app.customerMaster');
                                Notify.alert('Customer Created Successfully..!!', 'success')
                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message)
                            }
                            // activate();
                        });

                    }

                });
                }
            }
           
        }

        $scope.complete = function (string) {
            if (string.length >= 3) {
                var url = 'api/customer/CommonCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        var message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        if (resp.data.message == null) {
                            $scope.customer_list = null;
                            $scope.message = "";
                        }
                        else {
                            $scope.customer_list = null;
                            $scope.message = resp.data.message;
                        }
                    }
                });
                $scope.message = "";
            }
            else {
                $scope.message = "";
                $scope.customer_list = null;
            }
        }
        $scope.fillTextbox = function (customer_gid, customer_name) {
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;
           
            var url = 'api/customer/CommonCustomer';
            var params = {
                customername: customer_name
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    $scope.message = "";
                   
                }
                else {
                    if (resp.data.message == null) {
                        $scope.customer_list = null;
                        $scope.message = "";
                    }
                    else{
                    $scope.customer_list = null;
                    $scope.message = resp.data.message;
                    }
                }
            });
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('cadApprovalcontroller', cadApprovalcontroller);

    cadApprovalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function cadApprovalcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'cadApprovalcontroller';

        activate();
        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/deferral/cadApprovalSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                // $scope.deferralSummary = resp.data;
                $scope.cadApproval_data = resp.data.managedeferralSummaryDtls;
                 // new code start   
                 if ($scope.cadApproval_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.cadApproval_data.length;
                                        if ($scope.cadApproval_data.length < 100) {
                                            $scope.totalDisplayed = $scope.cadApproval_data.length;
                                        }
                                    }
                    // new code end
                // $scope.total=$scope.cadApproval_data.length;
               
            });
          
      }

        // $scope.isShowHide = function (param) {
        //     if (param == "show") {
        //         $scope.showval = true;
        //         $scope.hideval = true;
        //     }
        //     else if (param == "hide") {
        //         $scope.showval = false;
        //         $scope.hideval = false;
        //     }
        //     else {
        //         $scope.showval = false;
        //         $scope.hideval = false;
        //     }
        // }

        $scope.popupApprove = function (val,val1,val2) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $scope.tracking_type = localStorage.setItem('tracking_type', val2);
            $state.go('app.defapp');

        }

        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
             // new code start
        if ($scope.cadApproval_data != null) {
       
                if (pagecount < $scope.cadApproval_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.cadApproval_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.cadApproval_data.length;
                        Notify.alert(" Total Summary " + $scope.cadApproval_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.cadApproval_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };
        
    $scope.downloads = function (val1, val2) {
        var phyPath = val1;
        var relPath = phyPath.split("StoryboardAPI");
        var relpath1 = relPath[1].replace("\\", "/");
        var hosts = window.location.host;
        var prefix = location.protocol + "//";
        var str = prefix.concat(hosts, relpath1);
        var link = document.createElement("a");
        var name = val2.split(".")
        link.download = val2;
        var uri = str;
        link.href = uri;
        link.click();
    }
        // Close Modals

    $scope.close = function (val) {
        document.getElementById("userform").reset();
        var doc = document.getElementById(val);
        doc.style.display = 'none';
    }
     
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('checkerApprovalSummaryController', checkerApprovalSummaryController);

    checkerApprovalSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function checkerApprovalSummaryController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'checkerApprovalSummaryController';
        var i = 0;
        var j = 0;
        var deferralGidList = [];
        //$scope.loandata = [];
        var user_code;
        activate();
        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/deferral/CheckerApprovalSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.deferral_data = resp.data.deferralSummaryDtls;
                // new code start  
                unlockUI();
                if ($scope.deferral_data == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.deferral_data.length;
                    if ($scope.deferral_data.length < 100) {
                        $scope.totalDisplayed = $scope.deferral_data.length;
                    }
                }
                    
            });

            var url = 'api/deferral/UserCode';
            SocketService.get(url).then(function (resp) {
                user_code = resp.data.user_code;
                //console.log($scope.UploadDocumentname);
                if (user_code == 'S0537' || user_code == 'S0562' || user_code == 'S0616' || user_code == 'S0448') {
                    //console.log('test');
                    $scope.user_status = "Y";
                }

            });

        }

        $scope.checkall = function (selected) {
            //console.log(selected);
            angular.forEach($scope.deferral_data, function (val) {
                val.checked = selected;
            });
        }

        $scope.bulkVerifyChecker = function () {
            angular.forEach($scope.deferral_data, function (val) {

                if (val.checked == true) {
                    var deferral_gid = val.deferral_gid;
                    deferralGidList.push(deferral_gid);
                    i = i + 1;
                }
            });
           
            if (i == 0) {
                Notify.alert('Select Atleast One Deferral!', 'warning');
                return false;
            }

            if ($scope.deferral_status == "PushBack") {
                //console.log($scope.deferral_status);
                var modalInstance = $modal.open({
                    templateUrl: '/updatecheckerremarks.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                    
                    $scope.RemarksUpdate = function () {

                        var params = {
                            deferral_gid: deferralGidList,
                            deferral_status: 'PushBack',
                            checker_remarks: $scope.checker_remarks
                        }
                        //console.log(params);
                        if ($scope.checker_remarks != undefined) {
                            //console.log('1');
                          
                            $modalInstance.close('closed');
                            var url = 'api/deferral/CheckerBulkVerify';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    //activate();
                                    $scope.deferral_status = '';
                                    Notify.alert('Checker Verification done Successfully!', 'success');
                                    
                                }
                                else {
                                    Notify.alert('Error While Checker Verification', 'warning');

                                }
                             
                                activate();
                            });
                            deferralGidList = [];
                            i = 0;
                            $('input[type="checkbox"]:checked').prop('checked', false);
                            activate();
                            $scope.deferral_status = '';
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert('Update Checker Remarks', 'warning');
                          
                            deferralGidList = [];
                            i = 0;
                            $('input[type="checkbox"]:checked').prop('checked', false);
                            activate();
                        }
                                             
                    }
                }
                $scope.deferral_status = '';
            }
            else if ($scope.deferral_status == "Close") {
                //console.log($scope.deferral_status);
                var modalInstance = $modal.open({
                    templateUrl: '/updateremarks.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };

                    $scope.Update = function () {

                        var params = {
                            deferral_gid: deferralGidList,
                            deferral_status: 'Close',
                            approval_remarks: $scope.approval_remarks,
                            customer_remarks: $scope.customerremarks
                        }
                        //console.log(params);
                        if (($scope.approval_remarks != undefined) || ($scope.customerremarks != undefined)) {
                            //console.log('1');

                            $modalInstance.close('closed');
                            var url = 'api/deferral/CheckerBulkVerify';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    //activate();
                                    $scope.deferral_status = '';
                                    Notify.alert('Checker Verification done Successfully!', 'success');

                                }
                                else {
                                    Notify.alert('Error While Checker Verification', 'warning');

                                }

                                activate();
                            });
                            deferralGidList = [];
                            i = 0;
                            $('input[type="checkbox"]:checked').prop('checked', false);
                            activate();
                            $scope.deferral_status = '';
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert('Update Remarks', 'warning');

                            deferralGidList = [];
                            i = 0;
                            $('input[type="checkbox"]:checked').prop('checked', false);
                            activate();
                        }

                    }
                }
                $scope.deferral_status = '';
            }
            else {
               
                //console.log(i);
                var params = {
                    deferral_gid: deferralGidList,
                    deferral_status: $scope.deferral_status
                }
                //console.log(params);
                
                    var url = 'api/deferral/CheckerBulkVerify';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert('Checker Verification done Successfully!', 'success');
                            $scope.deferral_status = '';
                            activate();
                        }
                        else {
                            Notify.alert('Error While Checker Verification', 'warning');
                            $scope.deferral_status = '';
                        }
                        activate();
                    });
                    deferralGidList = [];
                    i = 0;                   
                    $('input[type="checkbox"]:checked').prop('checked', false);
                    activate();
                    $scope.deferral_status = '';
            }
           
        }


        $scope.export = function () {

            if ($scope.deferral_data==null) {
                Notify.alert('No Records to Export !', 'warning');
                return;
            }
            angular.forEach($scope.deferral_data, function (val) {

                if (val.checked == true) {
                    var deferral_gid = val.deferral_gid;
                    deferralGidList.push(deferral_gid);
                }
            });

            var params = {
                deferral_gid: deferralGidList
            }
            //console.log(params);
            lockUI();
            var url = 'api/deferral/CheckerExcelExport';

            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname);
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // //console.log(str);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')
                    activate();
                }

            });
        }


        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.deferral_data != null) {

                if (pagecount < $scope.deferral_data.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.deferral_data.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.deferral_data.length;
                        Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };

        $scope.verifyChecker = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.checkerApprovalView');
        }


    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('checkerApprovalViewController', checkerApprovalViewController);

    checkerApprovalViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function checkerApprovalViewController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'checkerApprovalViewController';

        activate();
        var user_code;
        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ["dd-MM-yyyy"];
            vm.format = vm.formats[0];

            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/Getcaddoc';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadDocumentname = resp.data;
                $scope.deferrals = resp.data.deferralSummaryDtls;
                $scope.deferral_gid = resp.data.deferral_gid;
                $scope.loanref_no = resp.data.loanref_no;
                $scope.loan_title = resp.data.loan_title;
                $scope.record_id = resp.data.record_id;
                $scope.deferral_name = resp.data.deferral_name;
                $scope.approval_remarks = resp.data.remarks;
                $scope.customerremarks = resp.data.customer_remarks;
                $scope.approval_status = resp.data.approval_status;
                $scope.filename_list = resp.data.filename_list;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.businesshead_name = resp.data.businesshead_name;
                $scope.rm_name = resp.data.rm_name;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.entity_name = resp.data.entity_name;
                $scope.branch_name = resp.data.branch_name;
                $scope.customer_code = resp.data.customer_code;
                $scope.cluster_manager_name = resp.data.cluster_manager_name;
            });
           
            var url = 'api/deferral/UserCode';
            SocketService.get(url).then(function (resp) {
                user_code = resp.data.user_code;                   
                if (user_code == 'S0537' || user_code == 'S0562') {
                    //console.log('test');
                    $scope.user_status = "Y";
                }

            });

            $scope.activeAdd = false;
            $scope.defaultchecker = true;       


           
        }

        $scope.close = function (val) {
            document.getElementById("userform").reset();
            var doc = document.getElementById(val);
            doc.style.display = 'none';
        }

        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('deferral_gid', $scope.deferral_gid);
            frm.append('loan_gid', $scope.loan_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            document.getElementById('load').style.visibility = "visible";
            var url = 'api/deferral/UploadcadDocument';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                unlockUI();
                $scope.filename_list = resp.data.filename_list;

                $("#addupload").val('');

                if (resp.data.status == true) {
                   
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')
                }
                var param = {
                    deferral_gid: localStorage.getItem('deferral_gid')
                };
                var url = 'api/deferral/Getcaddoc';
                SocketService.getparams(url, param).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.filename_list = resp.data.filename_list;
                    }
                });
            });
        }

        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            ////console.log(str);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        var params = {
            deferral_gid: localStorage.getItem('deferral_gid')
        }
        var url = 'api/deferral/checkerlist';
        SocketService.getparams(url, params).then(function (resp) {
            $scope.checker_list = resp.data.checker_list;
            if ($scope.checker_list == null) {
                $scope.approval_history = true;
            }
            else {
                $scope.approval_history = false;
            }
        });



        $scope.onselectedchangedeferral = function (val) {
            $scope.showvalchecker_remarks = false;
            var params = {
                deferral: val
            };
            var url = 'api/loan/getdeferralcriticallity';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.MDLcriticallity = resp.data;
                $scope.criticallity = resp.data.criticallity;
                $scope.remarks = resp.data.comments;
                $scope.customerremarks = resp.data.comments;
                $scope.critical = true
            });
        }

        $scope.onselectedchangecovenant = function (covenanttype) {
            $scope.showvalchecker_remarks = false;
            var params = {
                covenanttype: covenanttype
            };
            var url = 'api/loan/getcovenanttypecriticallity';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.MDLcriticallity = resp.data;
                $scope.criticallity = resp.data.criticallity;
                $scope.remarks = resp.data.comments;
                $scope.customerremarks = resp.data.comments;
                $scope.critical = true
            });
        }
        $scope.btncopy = function () {
            var customer_remarks = $scope.remarks;
            $scope.customerremarks = customer_remarks;
        }

        $scope.onselectedchange = function (deferral_status) {
            if (deferral_status == "PushBack") {
                $scope.showvalchecker_remarks = true;
                $scope.activeAdd = false;
            }
            else {
                $scope.showvalchecker_remarks = false;
                $scope.activeAdd = false;
            }

            if (deferral_status == "Close") {
                $scope.showvalclosed = true;
                $scope.activeAdd = false;
            }
            else {
                $scope.showvalclosed = false;
                $scope.activeAdd = false;
            }
            if (deferral_status == "Approve") {
                popapprove()
                $scope.activeAdd = true;
                $scope.defaultchecker = false;
            }
            else {
                $scope.activeAdd = false;
                $scope.defaultchecker = true;
            }
        }

        function popapprove() {
            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });

            var url = 'api/entity/entity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
            });

            var url = 'api/branch/branch';
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branch_list;
            });

            var url = 'api/deferral/deferral';
            SocketService.get(url).then(function (resp) {
                $scope.deferral_list = resp.data.deferral_list;
            });
            var url = 'api/covenanttype/getcovenanttype';
            SocketService.get(url).then(function (resp) {
                $scope.covenanttype_list = resp.data.covenanttype_list;
            });
            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.clusterlist = resp.data.employee_list;
                $scope.businesshead_list = resp.data.employee_list;
                $scope.relationshiplist = resp.data.employee_list;
                $scope.zonallist = resp.data.employee_list;
                $scope.creditlist = resp.data.employee_list;
            });

            var param = {
                deferral_gid: localStorage.getItem('deferral_gid')
            };
            var url = 'api/deferral/Getcaddoc';
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.filename_list = resp.data.filename_list;
                }
            });

            var param = {
                deferral_gid: localStorage.getItem('deferral_gid')
            };

            var url = 'api/deferral/Getdeferraldetails';
            SocketService.getparams(url, param).then(function (resp) {
                //console.log(resp.data)
                $scope.Getdeferral = resp.data;
                $scope.branchEdit = resp.data.branch_gid;
                $scope.entityEdit = resp.data.entity_gid;
                $scope.customer = resp.data.customer_gid;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.cluster_manager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmgmt_name = resp.data.creditmanager_gid;
                $scope.tracking_type = resp.data.tracking_type;
                $scope.loanRefNoedit = resp.data.loanGID;
                $scope.due_dateedit = resp.data.due_date;
                $scope.due_dateedit = Date.parse($scope.due_dateedit);
                if (resp.data.tracking_type == "Covenant") {
                    $scope.showval = true;
                    $scope.hideval = false;
                }
                else {
                    $scope.showval = false;
                    $scope.hideval = true;
                }
                $scope.deferralname = resp.data.deferraltype_gid;
                $scope.covenanttype = resp.data.covenanttype_gid;
                $scope.deferralcategoryedit = resp.data.deferral_category;
                $scope.vertical = resp.data.vertical_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.criticallity = resp.data.criticallity;
                $scope.remarks = resp.data.remarks;
                $scope.customerremarks = resp.data.customerremarks;
                $scope.checker_status = resp.data.checker_status;
                $scope.checker_remarks = resp.data.checker_remarks;
                //if (resp.data.checker_status == "PushBack") {
                //    $scope.showvalchecker_pushback = true;
                //}
                //else {
                //    $scope.showvalchecker_pushback = false;
                //}

            });

        }

        $scope.deferralback = function (val) {
            $state.go('app.checkerApprovalSummary');
        }

        $scope.isShowHide = function (param) {
            if (param == "show") {
                $scope.showval = true;
                $scope.hideval = false;
            }
            else if (param == "hide") {
                $scope.showval = false;
                $scope.hideval = true;
            }
            else {
                $scope.showval = false;
                $scope.hideval = false;
            }
        }

        $scope.CheckerVerify = function () {
           
           

            //console.log('params', params);
            //var replaced = $scope.checker_remarks.replace(/\s\s+/g, '+');
            //console.log($scope.checker_remarks, 'test');
            if ($scope.deferral_status == 'PushBack') {
                var params = {
                    def_gid: $scope.deferral_gid,
                    deferral_status: $scope.deferral_status,
                    checker_remarks: $scope.checker_remarks,
                    approval_remarks: $scope.approval_remarks,
                    customer_remarks: $scope.customerremarks
                }
                if (($scope.checker_remarks == undefined) || ($scope.checker_remarks.replace(/ +(?= )/g, '+') == '+') ||
                    ($scope.checker_remarks.replace(/\s\s+/g, '+') == '+') || ($scope.checker_remarks.replace('', '+') == '+') ||
                    ($scope.checker_remarks.replace(/\n/g, '+') == '+') || ($scope.checker_remarks.replace(' ', '+') == '+')) {
                   
                    Notify.alert('Enter Checker Remarks', 'warning')
            
                }
                else {
                    var url = 'api/deferral/CheckerVerify';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        //console.log(url, params);
                        unlockUI();
                        if (resp.data.status == true) {
                            $state.go('app.checkerApprovalSummary');
                            Notify.alert('Checker Verification done Successfully..!!', 'success')
                        }
                        else {
                            Notify.alert('Error While Checker Verification', 'warning')
                        }
                    });
                }
            }
            else if ($scope.deferral_status == 'Approve')
            {

                var entity_name_edit;
                var branch_name_edit;
                var deferraltype_name_edit;
                var covenanttype_name_edit;
                var customername_edit;
                var zonalhead_name_edit;
                var businesshead_name_edit;
                var clusterhead_name_edit;
                var relationalmgr_name_edit;
                var creditmanager_name_edit;

                var vertical_code = $scope.vertical_code;
                var entity_index = $scope.entity_list.map(function (e) { return e.entity_gid }).indexOf($scope.entityEdit);
                if (entity_index == -1) { entity_name_edit = ''; } else { entity_name_edit = $scope.entity_list[entity_index].entity_name; };
                var branch_index = $scope.branch_list.map(function (e) { return e.branch_gid }).indexOf($scope.branchEdit);
                if (branch_index == -1) { branch_name_edit = ''; } else { branch_name_edit = $scope.branch_list[branch_index].branch_name; };
                var customer_index = $scope.customer_list.map(function (e) { return e.customer_gid }).indexOf($scope.customer);
                if (customer_index == -1) { customername_edit = ''; } else { customername_edit = $scope.customer_list[customer_index].customername; };
                var zonalhead_index = $scope.zonallist.map(function (e) { return e.employee_gid }).indexOf($scope.zonalHead);
                if (zonalhead_index == -1) { zonalhead_name_edit = ''; } else { zonalhead_name_edit = $scope.zonallist[zonalhead_index].employee_name; }
                var businesshead_index = $scope.businesshead_list.map(function (e) { return e.employee_gid }).indexOf($scope.businessHead);
                if (businesshead_index == -1) { businesshead_name_edit = ''; } else { businesshead_name_edit = $scope.businesshead_list[businesshead_index].employee_name; }
                var clusterhead_index = $scope.clusterlist.map(function (e) { return e.employee_gid }).indexOf($scope.cluster_manager);
                if (clusterhead_index == -1) { clusterhead_name_edit = ''; } else { clusterhead_name_edit = $scope.clusterlist[clusterhead_index].employee_name; }
                var reletinalmgr_index = $scope.relationshiplist.map(function (e) { return e.employee_gid }).indexOf($scope.relationshipMgmt);
                if (reletinalmgr_index == -1) { relationalmgr_name_edit = ''; } else { relationalmgr_name_edit = $scope.relationshiplist[reletinalmgr_index].employee_name; }
                var creditmanager_index = $scope.creditlist.map(function (e) { return e.employee_gid }).indexOf($scope.creditmgmt_name);
                if (creditmanager_index == -1) { creditmanager_name_edit = ''; } else { creditmanager_name_edit = $scope.creditlist[creditmanager_index].employee_name; }
                var deferraltype_index = $scope.deferral_list.map(function (e) { return e.deferraltype_gid }).indexOf($scope.deferralname);
                if (deferraltype_index == -1) { deferraltype_name_edit = '' } else { deferraltype_name_edit = $scope.deferral_list[deferraltype_index].deferral_name; };
                var covenanttype_index = $scope.covenanttype_list.map(function (e) { return e.covenanttype_gid }).indexOf($scope.covenanttype);
                if (covenanttype_index == -1) { covenanttype_name_edit = '' } else { covenanttype_name_edit = $scope.covenanttype_list[covenanttype_index].covenanttype_name; };
                if ($scope.tracking_type == 'Covenant') { $scope.deferralname = ''; deferraltype_name_edit = ''; } else { $scope.covenanttype = ''; covenanttype_name_edit = ''; };

                var params = {
                    deferral_gid: localStorage.getItem('deferral_gid'),
                    def_gid: $scope.deferral_gid,
                    deferral_status: $scope.deferral_status,
                    entity_gid: $scope.entityEdit,
                    entity_name: entity_name_edit,
                    branch_gid: $scope.branchEdit,
                    branch_name: branch_name_edit,
                    customer_gid: $scope.customer,
                    customer_name: customername_edit,
                    loan_gid: $scope.loanRefNoedit,
                    zonalhead_gid: $scope.zonalHead,
                    zonalhead_name: zonalhead_name_edit,
                    businesshead_gid: $scope.businessHead,
                    businesshead_name: businesshead_name_edit,
                    clustermgr_gid: $scope.cluster_manager,
                    clusterhead_name: clusterhead_name_edit,
                    relationmgr_gid: $scope.relationshipMgmt,
                    relationmgr_name: relationalmgr_name_edit,
                    creditmgr_gid: $scope.creditmgmt_name,
                    creditmgr_name: creditmanager_name_edit,
                    category_gid: $scope.deferralcategoryedit,
                    tracking_type: $scope.tracking_type,
                    deferraltype_gid: $scope.deferralname,
                    deferraltype_name: deferraltype_name_edit,
                    covenanttype_gid: $scope.covenanttype,
                    covenanttype_name: covenanttype_name_edit,
                    duedate: $scope.due_dateedit,
                    //duedate: date,
                    criticallity: $scope.criticallity,
                    vertical_code: vertical_code,
                    vertical_gid: $scope.vertical,
                    remarks: $scope.remarks,
                    customerremarks: $scope.customerremarks,
                    checker_status: $scope.checker_status
                }
                if ($scope.tracking_type == 'Deferral') {
                    //console.log($scope.deferralname);
                    if ($scope.deferralname == '') {
                        Notify.alert('Kindly select the deferral', 'warning');
                        return;
                    }

                }
                else if ($scope.tracking_type == 'Covenant') {
                    //console.log($scope.covenanttype);
                    if ($scope.covenanttype == '') {
                        Notify.alert('Kindly select the covenant', 'warning');
                        return;
                    }

                }
                //console.log($scope.remarks, $scope.customerremarks);
                if ($scope.remarks == "") {
                    //console.log('test');
                    Notify.alert('Kindly Enter Internal Remarks', 'warning');
                    return;
                }
 
                if ($scope.customerremarks == "") {
                    //console.log('test');
                    Notify.alert('Kindly Enter Customer Remarks', 'warning');
                    return;
                }

                var url = 'api/deferral/CheckerVerify';
                lockUI();
                SocketService.post(url, params).then(function (resp) {

                    unlockUI();
                    if (resp.data.status == true) {
                        $state.go('app.checkerApprovalSummary');
                        Notify.alert('Checker Verification done Successfully..!!', 'success')
                    }
                    else {
                        Notify.alert('Error While Checker Verification', 'warning')
                    }
                });
            }
            else {
                var params = {
                    def_gid: $scope.deferral_gid,
                    deferral_status: $scope.deferral_status,
                    checker_remarks: $scope.checker_remarks,
                    approval_remarks: $scope.approval_remarks,
                    customer_remarks: $scope.customerremarks
                }
                var url = 'api/deferral/CheckerVerify';
                lockUI();
                SocketService.post(url, params).then(function (resp) {

                    unlockUI();
                    if (resp.data.status == true) {
                        $state.go('app.checkerApprovalSummary');
                        Notify.alert('Checker Verification done Successfully..!!', 'success')
                    }
                    else {
                        Notify.alert('Error While Checker Verification', 'warning')
                    }
                });
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('collateralSummaryController', collateralSummaryController);

    collateralSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function collateralSummaryController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'collateralSummaryController';

        activate();

        function activate() {
            var url = 'api/collateral/getCollateralSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.collateral_data = resp.data.customercollateral_list;
            });
        }
        $scope.popupcollateral=function(){
            $state.go('app.collateral');
        }
        $scope.delete = function (collateral_gid) {
            var params = {
                collateral_gid: collateral_gid
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
                    var url = 'api/collateral/collateralDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                           
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
        $scope.edit = function (collateral_gid) {
            $scope.collateral_gid = localStorage.setItem('collateral_gid', collateral_gid);
          
            $state.go('app.editCollateral');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('covenantcontroller', covenantcontroller);

    covenantcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function covenantcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'covenantcontroller';
       
        activate();


        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/covenanttype/getcovenanttype';
            SocketService.get(url).then(function (resp) {
                $scope.covenant_data = resp.data.covenanttype_list;
                unlockUI();
                // new code start   
                if ($scope.covenant_data== null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.covenant_data.length;
                                        if ($scope.covenant_data.length < 100) {
                                            $scope.totalDisplayed = $scope.covenant_data.length;
                                        }
                                    }
                    // new code endd
                //$scope.total=$scope.covenant_data.length;
              
            });
        }

        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
        if ($scope.covenant_data!= null) {
       
                if (pagecount < $scope.covenant_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.covenant_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.covenant_data.length;
                        Notify.alert(" Total Summary " + $scope.covenant_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.covenant_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };
        $scope.popupcovenenttype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/covenanttype.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/employee/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


                //$scope.onselectedchangecriticallity = function (criticallity) {
                //    if (criticallity == "Critical") {
                //        $scope.showval = true;
                //    }
                //    else {
                //        $scope.showval = false;
                //    }
                //}

                $scope.covenantSubmit = function () {
                    if ($scope.comments == undefined) {
                        $scope.comments = ""
                    }

                var params = {
                    covenanttype_name: $scope.covenantTypename,
                    covenanttype_code: $scope.covenantTypecode,
                    criticallity: $scope.criticality,
                    comments: $scope.comments
                }             
                var url = 'api/covenanttype/createcovenanttype';
              
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        
                        $modalInstance.close('closed');
                        //$scope.close('popupcovenenttype');
                        Notify.alert('Covenant Type created Successfully..!!', {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                  }
                    else {
                        Notify.alert('Error Occurred ', {
                            status: 'warning',
                            pos: 'top-right',
                            timeout: 3000
                        });
                        activate();
                    }
                   
              });
            }
            }
        }

        //Edit Function

        $scope.edit = function (covenanttype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/covenantedit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            //var doc = document.getElementById('edit');
            //doc.style.display = 'block';
            $scope.covenanttype_gid = covenanttype_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                //$scope.customer_gid = customer_gid;
                $scope.covenanttype_gid = localStorage.setItem('covenanttype_gid', covenanttype_gid);
                var params = {
                    covenanttype_gid: covenanttype_gid
                }
                var url = 'api/covenanttype/GetCovenantTypeupdate';
               
                SocketService.getparams(url, params).then(function (resp) {
                   
                    $scope.covenantTypecodeedit = resp.data.covenantTypecodeedit;
                    $scope.covenantTypenameedit = resp.data.covenantTypenameedit;
                    $scope.criticality = resp.data.criticallity;
                    $scope.commentsEdit = resp.data.comments;
                    $scope.covenantTypegid = resp.data.covenanttype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };
                $scope.covenantUpdate = function () {

                    var params = {
                        covenanttypenameedit: $scope.covenantTypenameedit,
                        covenanttypecodeedit: $scope.covenantTypecodeedit,
                        criticallity: $scope.criticality,
                        comments: $scope.commentsEdit,
                        covenanttype_gid: $scope.covenantTypegid
                    }
                    var url = 'api/covenanttype/covenantTypeUpdate';
                   
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            
                            //$scope.close('edit');
                            $modalInstance.close('closed');
                            //SweetAlert.swal('Success!', 'Covenant Type Updated!', 'success');
                            Notify.alert('Covenant Type Updated Successfully..!!', {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            //SweetAlert.swal('Success!', 'Error Occured While Updating Covenant Type', 'success');
                            Notify.alert('Error Occurred ', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            }

        }
    
        $scope.delete = function (covenanttype_gid) {
            var params = {
                covenanttype_gid: covenanttype_gid
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
                    var url = 'api/covenanttype/covenantTypeDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };



        }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('createCollateral', createCollateral);

    createCollateral.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function createCollateral($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'createCollateral';

        activate();

        function activate() {
            //var url = 'api/customer/customer';
            //SocketService.get(url).then(function (resp) {
            //    $scope.customer_list = resp.data.customer_list;
            //    $scope.customer_gid = resp.data.customer_gid;
            //});

            var url = 'api/security/getSecuritytype';
            SocketService.get(url).then(function (resp) {
                $scope.security_data = resp.data.securitytype_list;
            });
        }

        $scope.complete = function (string) {

            if (string.length >= 3) {
                $scope.message = "";
                var url = 'api/customer/ExploreCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        $scope.message = "No Records";
                    }


                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "Type atleast three character";
            }
        }

        $scope.fillTextbox = function (customer_gid, customer_name) {
            //console.log('string', customer_name, customer_gid);
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;




            var params = {
                customer_gid: customer_gid
            }
            var url = 'api/deferral/customer2loan';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.loan = resp.data.loan;

            });
       
        }


        $scope.onselectedchangecustomer = function (customer) {
            var params = {
                customer_gid: customer
            };
            var url = 'api/deferral/customer2loan';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.loan = resp.data.loan;
               
            });
        }

     
        $scope.checkall = function (selected) {
            angular.forEach($scope.loan, function (val) {
                val.checked = selected;
            });
        }

        $scope.collateralSubmit = function () {
           
            var loanGidList = [];
            angular.forEach($scope.loan, function (val) {

                if (val.checked == true) {
                    var loan_gid = val.value;
                 loanGidList.push(loan_gid);
                }

            });

            //var customer_name = $('#customer_name :selected').text();
            var security_type = $('#security_type :selected').text();
            var params = {
            customer_name: $scope.customer,
            customer_gid: $scope.customer_gid,
            security_type: security_type,
            securitytype_gid:$scope.security_type,
            security_description: $scope.security_description,
            account_status: $scope.account_status,
            loan_gid: loanGidList
            }
            //console.log(params);
            var url = 'api/collateral/createCollateral';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                   
                    unlockUI()

                   Notify.alert('Collateral Created Successfully..!!', 'success')

                   $state.go('app.collateralsummary');
                   
                }
                else {
                   
                    unlockUI()
                    Notify.alert('Select Atleast One Loan')
                    
                }
               
               });
        }
        $scope.back=function()
        {
            $state.go('app.collateralsummary');
        }

}
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('createDeferral', createDeferral);

    createDeferral.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function createDeferral($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'createDeferral';
        activate();
        var customer_remarks;

        function activate() {
            $scope.def = false;
            $scope.cov = false;
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats=['dd-MM-yyyy'];
            vm.format=vm.formats[0];

            var url = 'api/entity/entity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
            });

            var url = 'api/branch/branch';
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branch_list;
            });

            var url = 'api/deferral/deferral';
            SocketService.get(url).then(function (resp) {
                $scope.deferral_list = resp.data.deferral_list;
            });
            var url = 'api/covenanttype/getcovenanttype';
            SocketService.get(url).then(function (resp) {
                $scope.covenanttype_list = resp.data.covenanttype_list;
            });
            //var url = 'api/customer/customer';
            //SocketService.get(url).then(function (resp) {
            //    $scope.customer_list = resp.data.customer_list;
            //});
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var params = {
                deferral_gid: ''
            }
            var url = 'api/deferral/Getcaddoc';

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                     
                    $scope.filename_list = resp.data.filename_list;
                }

            });
           
        }


        $scope.complete = function (string) {

            if (string.length >= 3) {
                $scope.message = "";
                var url = 'api/customer/ExploreCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        $scope.message = "No Records";
                    }


                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "Type atleast three character";
            }
        }

        $scope.fillTextbox = function (customer_gid, customer_name) {
            //console.log('string', customer_name, customer_gid);
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;




            var params = {
                customer_gid: customer_gid
            }

            var loandata = '';
          
            var url = 'api/deferral/customer2loan';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.loan = resp.data.loan;

            });

            $scope.showcustomer = true;
            $scope.showdetails = true;

            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.mdlheadsofcustomer = resp.data;
                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmgmt_gid = resp.data.creditmanager_gid;
                $scope.vertical_code = resp.data.vertical_code;
                //$scope.vertical_gid = resp.data.vertical_gid;
                $scope.vertical = true;

            });
        }


            $scope.btncopy = function () {
                customer_remarks = $scope.remarks;
                $scope.customerremarks = customer_remarks;
            }

            $scope.onselectedchangecustomer = function () {
                var loandata = '';
                var params = {
                    customer_gid: $scope.customer_gid
                };
                var url = 'api/deferral/customer2loan';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.loan = resp.data.loan;

                });

                $scope.showcustomer = true;
                $scope.showdetails = true;

                var url = 'api/loan/customer_getheads';

                SocketService.getparams(url, params).then(function (resp) {

                    $scope.mdlheadsofcustomer = resp.data;
                    $scope.zonalHead = resp.data.zonalGid;
                    $scope.businessHead = resp.data.businessHeadGid;
                    $scope.clustermanager = resp.data.clustermanagerGid;
                    $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                    $scope.creditmgmt_gid = resp.data.creditmanager_gid;
                    $scope.vertical_code = resp.data.vertical_code;
                    //$scope.vertical_gid = resp.data.vertical_gid;
                    $scope.vertical = true;
                });

            }

            $scope.onselectedchangedeferral = function (val) {
                if($scope.deferralname==undefined){
                    $scope.MDLcriticallity = '';
                    $scope.criticallity = '';
                    $scope.remarks ='';
                    $scope.customerremarks='';
                    $scope.critical = false;
              
                }
                else{
                    $scope.MDLcriticallity = $scope.deferralname;
                    $scope.criticallity = $scope.deferralname.criticallity;
                    $scope.remarks =$scope.deferralname.comments;
                    $scope.customerremarks=$scope.deferralname.comments;
                    $scope.critical = true
              
                }
              
            }
            $scope.onselectedchangecovenant = function (covenanttype) {
                if($scope.covenanttype==undefined){
                    $scope.MDLcriticallity = '';
                    $scope.criticallity = '';
                    $scope.remarks ='';
                    $scope.customerremarks='';
                    $scope.critical = false
          
                }
                else{
                    $scope.MDLcriticallity = $scope.covenanttype;
                    $scope.criticallity = $scope.covenanttype.criticallity;
                    $scope.remarks = $scope.covenanttype.comments;
                    $scope.customerremarks=$scope.covenanttype.comments;
                    $scope.critical = true
          
                }
              
            }

            $scope.isShowHide = function (param) {
                if (param == "show") {
                    $scope.def = false;
                    $scope.cov = true;
                    $scope.showval = true;
                    $scope.hideval = false;
                    $scope.showdiv = true;
                }
                else if (param == "hide") {
                    $scope.def = true;
                    $scope.cov = false;
                    $scope.showval = false;
                    $scope.hideval = true;
                    $scope.showdiv = true;
                }
                else {
                    $scope.showval = false;
                    $scope.hideval = false;
                    $scope.showdiv = true;
                }
            }

            $scope.upload = function (val, val1, name) {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('deferral_gid', $scope.deferral_gid);
                frm.append('loan_gid', $scope.loan_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/deferral/UploadcadDocument';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.filename_list = resp.data.filename_list;

                    $("#addupload").val('');
                    $("#editupload").val('');
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Document Uploaded Successfully..!!', 'success')
                        var params = {
                            deferral_gid: $scope.deferral_gid
                        }
                        var url = 'api/deferral/Getcaddoc';

                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                $scope.filename_list = resp.data.filename_list;
                            }

                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('File Format Not Supported!')

                    }

                });

            }
            $scope.downloads = function (val1, val2) {
                //var phyPath = val1;
                //var relPath = phyPath.split("StoryboardAPI");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                //var link = document.createElement("a");
                //var name = val2.split('.');
                //link.download = val2;
                //var uri = str;
                //link.href = uri;
                //link.click();
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
    

            $scope.deferralback = function (val) {
                $state.go('app.DeferralManagement');
            }


            $scope.deferralSubmit = function () {
                var deferral_name;
                var deferraltype_gid;
                var covenanttype_name;
                var covenanttype_gid;

                if($scope.trackingtype=='Deferral'){
                    if($scope.deferralname==undefined){
                        Notify.alert('Kindly select the deferral','warning');
                        return;
                    }
                    deferral_name= $('#deferralname :selected').text();
                    deferraltype_gid=$scope.deferralname.deferral_gid
                    covenanttype_name = '';
                    covenanttype_gid='';
                }
                else if($scope.trackingtype=='Covenant'){
                    if($scope.covenanttype==undefined){
                        Notify.alert('Kindly select the covenant','warning');
                        return;
                    }
                    deferral_name='';
                    deferraltype_gid='';
                    covenanttype_name = $('#covenanttype_name :selected').text();
                    covenanttype_gid=$scope.covenanttype.covenanttype_gid
                }
         
                var loandata = $('#loandata :selected').text();
                //var customer_name = $('#customer_name :selected').text();
                var zonal_name = $('#zonal_name :selected').text();
                var businesshead_name = $('#businesshead_name :selected').text();
                var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
                var cluster_manager_name = $('#cluster_manager_name :selected').text();
                var creditmgmt_name = $('#creditmgmt_name :selected').text();
                var entityname = $('#entity_name :selected').text();
                var branchname = $('#branch_name :selected').text();
                var params = {
                    entity_gid: $scope.entity.entity_gid,
                    branch_gid: $scope.branch.branch_gid,
                    entity_name: entityname,
                    branch_name: branchname,
                    customer_gid: $scope.customer_gid,
                    covenanttype_gid:covenanttype_gid,
                    criticallity: $scope.criticallity,
                    deferraltype_gid: deferraltype_gid,
                    loans: $scope.$parent.loan,
                    record_id: $scope.record_id,
                    tracking_type: $scope.trackingtype,
                    vertical_code: $scope.vertical_code,
                    deferral_name: deferral_name,
                    covenanttype_name: covenanttype_name,
                    deferral_gid:deferraltype_gid,
                    deferral_category: $scope.deferralcategory,
                    due_date: $scope.due_date,
                    sanction_refno: $scope.sanctionrefno,
                    sanction_date: $scope.sanctiondate,
                    remarks: $scope.remarks,
                    customerremarks :$scope.customerremarks,
                    customer_name: $scope.customer,
                    zonal_name: zonal_name,
                    businesshead_name: businesshead_name,
                    relationshipmgmt_name: relationshipmgmt_name,
                    cluster_manager_name: cluster_manager_name,
                    creditmgmt_name: creditmgmt_name,
                    zonalGid: $scope.zonalHead,
                    businessHeadGid: $scope.businessHead,
                    relationshipMgmtGid: $scope.relationshipMgmt,
                    clustermanagerGid: $scope.clustermanager,
                    creditmanager_gid: $scope.creditmgmt_gid,
                
                }
                //console.log(params);
                if ($scope.trackingtype == "Deferral") {
                  
                    if (deferral_name == "Select Deferral") {
                        lockUI();
                        Notify.alert('Select Deferral!', 'warning');
                        unlockUI();
                    }
                    else {

                       
                        lockUI();
                        var url = 'api/deferral/createDeferral';

                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI();
                              
                                Notify.alert('Deferral Created Successfully..!!', 'success')
                                $state.go('app.DeferralManagement');
                            }
                            else {
                                unlockUI();
                              
                                Notify.alert('Select Atleast One Loan')
                                $state.go('app.createDeferral');
                            }

                        });
                      
                    }
                }
                else if ($scope.trackingtype == "Covenant") {
                    if (covenanttype_name == "Select Covenant Type") {
                        Notify.alert('Select Covenant Type!', 'warning');
                       
                    }
                    else { 
                        lockUI();
                        var url = 'api/deferral/createDeferral';
                        SocketService.post(url, params).then(function (resp) {                          
                            if (resp.data.status == true) {
                                unlockUI()                             
                                
                                Notify.alert('Deferral Created Successfully..!!', 'success')
                               
                                $state.go('app.DeferralManagement');
                            }
                            else {                             
                                unlockUI()
                                Notify.alert('Select Atleast One Loan')                             
                                $state.go('app.createDeferral');
                            }
                         
                        });
                        
                    }
                };               
            }

            $scope.deferralSave = function () {
                var deferral_name;
                var deferraltype_gid;
                var covenanttype_name;
                var covenanttype_gid;

                var loandata = $('#loandata :selected').text();
                //var customer_name = $('#customer_name :selected').text();
                var zonal_name = $('#zonal_name :selected').text();
                var businesshead_name = $('#businesshead_name :selected').text();
                var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
                var cluster_manager_name = $('#cluster_manager_name :selected').text();
                var creditmgmt_name = $('#creditmgmt_name :selected').text();
                var entityname = $('#entity_name :selected').text();
                var branchname = $('#branch_name :selected').text();

                if($scope.trackingtype=='Deferral'){
                    if($scope.deferralname==undefined){
                        Notify.alert('Kindly select the deferral','warning');
                        return;
                    }
                    deferral_name= $('#deferralname :selected').text();
                    deferraltype_gid=$scope.deferralname.deferral_gid
                    covenanttype_name = '';
                    covenanttype_gid='';
                }
                else if($scope.trackingtype=='Covenant'){
                    if ($scope.covenanttype.covenanttype_name == undefined) {
                        Notify.alert('Kindly select the covenant','warning');
                        return;
                    }
                    deferral_name='';
                    deferraltype_gid='';
                    covenanttype_name = $('#covenanttype_name :selected').text();
                    covenanttype_gid=$scope.covenanttype.covenanttype_gid
                }
         
                var params = {
                    entity_gid: $scope.entity.entity_gid,
                    branch_gid: $scope.branch.branch_gid,
                    entity_name: entityname,
                    branch_name: branchname,
                    customer_gid: $scope.customer_gid,
                    covenanttype_gid:covenanttype_gid,
                    criticallity: $scope.criticallity,
                    deferraltype_gid: deferraltype_gid,
                    loans: $scope.$parent.loan,
                    record_id: $scope.record_id,
                    tracking_type: $scope.trackingtype,
                    vertical_code: $scope.vertical_code,
                    deferral_name: deferral_name,
                    covenanttype_name: covenanttype_name,
                    deferral_gid: deferraltype_gid,
                    deferral_category: $scope.deferralcategory,
                    due_date: $scope.due_date,
                    sanction_refno: $scope.sanctionrefno,
                    sanction_date: $scope.sanctiondate,
                    remarks: $scope.remarks,
                    customerremarks :$scope.customerremarks,
                    customer_name: $scope.customer,
                    zonal_name: zonal_name,
                    businesshead_name: businesshead_name,
                    relationshipmgmt_name: relationshipmgmt_name,
                    cluster_manager_name: cluster_manager_name,
                    creditmgmt_name: creditmgmt_name,
                    zonalGid: $scope.zonalHead,
                    businessHeadGid: $scope.businessHead,
                    relationshipMgmtGid: $scope.relationshipMgmt,
                    clustermanagerGid: $scope.clustermanager,
                    creditmanager_gid: $scope.creditmgmt_gid,
                
                }
                //console.log($scope.customer, $scope.customer_gid);

                lockUI();
                var url = 'api/deferral/createDeferral';

                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Deferral Created Successfully..!!', 'success')
                        $scope.deferralcategory = "";
                        $scope.due_date = "";
                        $scope.deferralname = "";
                        $scope.covenanttype = "";
                        $scope.trackingtype = "";
                        $scope.selectedData = "";
                        $scope.remarks = "";
                        $scope.customerremarks = "";
                        //$scope.test_document = "";
                        $scope.loan = [];
                        var params = {
                            customer_gid: $scope.customer_gid
                        };
                        var url = 'api/deferral/customer2loan';

                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.loan = resp.data.loan;

                        });
                        $scope.critical = false;
                        $scope.showval = false;
                        $scope.hideval = false;
                        document.getElementById("trackingtype").checked = false;
                        var params = {
                            deferral_gid: ''
                        }
                        var url = 'api/deferral/Getcaddoc';

                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                $scope.filename_list = resp.data.filename_list;
                            }

                        });
                    }
                       
                });
                  
                 
                    
            };

         

        }
    })();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('createLoan', createLoan);

    createLoan.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function createLoan($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'createLoan';
       
         
        activate();
        function activate() {

            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };


          
            // var url = 'api/customer/customer';
            // SocketService.get(url).then(function (resp) {
            //     $scope.customer_list = resp.data.customer_list;
            //    
            //     angular.forEach($scope.customer_list, function (value,key) {  
                  
            //         $scope.countryList.push([value.customer_gid, value.customername]);

            //         // list.push(  {customer_gid:value.customer_gid,
            //         //     customername:value.customername})
                      
                   
            // });
          
           
        // });

          
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

        }

            $scope.complete=function(string){
                
               if(string.length >=3){
                $scope.message="";
                var url = 'api/customer/ExploreCustomer';
                var params={
                     customername:string 
                 }
                 SocketService.getparams(url,params).then(function (resp) {
                     if(resp.data.status==true){
                        $scope.message="";
                        $scope.customer_list = resp.data.Customers;
                     }
                     else{
                        $scope.message="No Records";
                     }
                    
                    
             });
       }
       else{
        $scope.customer_list=null;
           $scope.message="Type atleast three character";
       }
              
              
                // var output=[];
                // angular.forEach($scope.countryList,function(country){
                    
                //     if(country[1].toLowerCase().indexOf(string.toLowerCase())>=0){
                //         output.push(country[1]);
                //        list_value.push(  {customer_gid:country[0],
                //         customername:country[1]})                       
                //     }
                // });
                // $scope.filterCountry=list_value;
                // console.log('filtercountry', $scope.filterCountry);
            }
            $scope.fillTextbox=function(customer_gid,customer_name){
             console.log('string',customer_name);
                $scope.customer=customer_name;
              $scope. customer_gid=customer_gid;
                $scope.customer_list=null;


                var params = {
                    customer_gid: customer_gid
                }
    
               
                var url = 'api/loan/customer_getheads';
    
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.mdlheadsofcustomer = resp.data;
                    $scope.zonalHead = resp.data.zonalGid;
                    $scope.businessHead = resp.data.businessHeadGid;
                    $scope.clustermanager = resp.data.clustermanagerGid;
                    $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                    $scope.creditmgmt_name = resp.data.creditmanager_gid;
                    $scope.vertical_code = resp.data.vertical_code;
                    $scope.vertical_code = true;
                    $scope.sanctiondtl = resp.data.sanctiondtl;
                   
                });
            }
        $scope.loanback = function (val) {
            $state.go('app.loanManagement');
        }
       
        
        $scope.sanctionrefnochange = function (sanction_gid) {
            var params = {
                sanction_gid: $scope.cbosanctionrefno.sanction_Gid
            }
            var url = 'api/loan/GetSanctionDate';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionDate = resp.data.sanctiondate;
                $scope.Sanction_Date = resp.data.Sanction_Date;
                $scope.facilitytype = resp.data.facility_type;
                $scope.facilitytype_gid = resp.data.facilitytype_gid;
            });
            var params = {
                customer2sanction_gid: $scope.cbosanctionrefno.sanction_Gid
            }
            var url = 'api/loan/SanctionLoanFacility';
            SocketService.getparams(url,params).then(function (resp) {
                if (resp.data.status == true)
                {
                   
                   
                     $scope.loan_list = resp.data.loanfacility;
                   
                }
              
            });
        }

        $scope.loanSubmit = function () {


            var vertical_code = $('#vertical_code :selected').text();
            var loan_title = $('#facility :selected').text();
            var zonal_name = $('#zonal_name :selected').text();
            var businesshead_name = $('#businesshead_name :selected').text();
            var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
            var cluster_manager_name = $('#cluster_manager_name :selected').text();
            var creditmgmt_name = $('#creditmanager_name :selected').text();
            var sanctionrefno = $('#sanction :selected').text();

            var params = {
                loanRefNo: $scope.loanRefNo,
                loanmaster_gid: $scope.cbofacility.facility_gid,
                sanctionGid: $scope.cbosanctionrefno.sanction_Gid,
                sanctionRefno: sanctionrefno,
                sanctionDate: $scope.Sanction_Date,
                loanTitle: loan_title,
                customerGid: $scope.customer_gid,
                customer_name: $scope.customer,
                vertical_gid: $scope.vertical,
                vertical_code: vertical_code,
                zonal_name: zonal_name,
                businesshead_name: businesshead_name,
                relationshipmgmt_name: relationshipmgmt_name,
                cluster_manager_name: cluster_manager_name,
                creditmanager_name: creditmgmt_name,
                zonalGid: $scope.zonalHead,
                businessHeadGid: $scope.businessHead,
                relationshipMgmtGid: $scope.relationshipMgmt,
                clustermanagerGid: $scope.clustermanager,
                creditmanager_gid: $scope.creditmgmt_name,
            }

          
         
            var url = 'api/loan/createLoan';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    Notify.alert('Loan Created Successfully..!!', 'success')
                    $state.go('app.loanManagement');

                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message,'warning')
                }
                activate();
            });

        }

    }
})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customerAlertcontroller', customerAlertcontroller);

    customerAlertcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function customerAlertcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customerAlertcontroller';

        activate();


        function activate() {

            var url = 'api/customer/CustomerAlert';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.customerlist = resp.data.customer_list;
                //console.log($scope.customerlist);
                $scope.customer_list = resp.data.customer_list;
                var length = $scope.customer_list.length;
                $scope.count_customer = length;
            });
        }

        $scope.deferraldetails = function (customer_gid, id) {
            var params = {
                customer_gid: customer_gid
            };
            var url = 'api/customer/deferraldetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_list[id][customer_gid] = resp.data.customerdeferral_list;
            });
        }
        $scope.rdGenerate = function (val) {
            $scope.customer_gid = val;
            $scope.customer_gid = localStorage.setItem('customer_gid', val);
            $state.go('app.customerAlertGenerate');

        }

        $scope.mailHistory = function (val) {
            $scope.customer_gid = val;
            $scope.customer_gid = localStorage.setItem('customer_gid', val);
            $scope.pageNavigation = localStorage.setItem('mailManagement', 'CustomerAlert');
            $state.go('app.customerAlertHistory');

        }
        $scope.onselectedchangecustomer = function ()
        {
            var url = 'api/customer/CustomerAlertSearch';
            var params = {
                customer_gid: $scope.customer.customer_gid
            };
            //console.log(params);
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
             
                if (resp.data.customer_list != null)
                {
                    $scope.customer_list = resp.data.customer_list;
                }
                else
                {
                    $scope.customer = "";
                    alert("No Record Found");
                    activate();
                   
                }
               
            });
        }
        $scope.refresh=function()
        {
            $scope.customer = "";
            activate();
        }

    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customerAlertGeneratecontroller', customerAlertGeneratecontroller);

    customerAlertGeneratecontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function customerAlertGeneratecontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customerAlertGeneratecontroller';

        activate();

        function activate() {
            $scope.customer_gid = localStorage.getItem('customer_gid');
            var url = 'api/customer/Getcustomerdetails';
            var params = {
                customer_gid: $scope.customer_gid
            };
            SocketService.getparams(url, params).then(function (resp)
            {
                $scope.customeredit = resp.data;
               
            });

            var url = 'api/customer/deferraldetails';
            SocketService.getparams(url, params).then(function (resp) {
                
                $scope.customer_data = resp.data.customerdeferral_list;
                console.log(resp.data.customerdeferral_list);
            });

            var url = 'api/template/Content';

            SocketService.get(url).then(function (resp) {
                $scope.content = resp.data.template_content;

            });
           
        }

        $scope.checkall = function (selected) {
            angular.forEach($scope.customer_data, function (val) {
                val.checked = selected;
            });
        }

        //$scope.onselectedchangeTemplate = function (template) {
        //    var params = {
        //        template_gid: template
        //    }
        //    var url = 'api/template/Content';

        //    SocketService.getparams(url, params).then(function (resp) {
        //        $scope.content = resp.data.template_content;

        //    });

        //}
        $scope.Contentback = function () {
            $state.go('app.customerAlert');
        }

        $scope.ContentSave = function () {
            var def_gid;
            var deferralGidList = [];
            angular.forEach($scope.customer_data, function (val) {

                if (val.checked == true) {
                    var deferral_gid = val.deferral_gid;
                    def_gid = val.deferral_gid;
                    deferralGidList.push(deferral_gid);
                }
               
            });
                var params = {
                    deferral_gid: deferralGidList,
                    customer_gid: localStorage.getItem('customer_gid'),
                    template_content: $scope.content
                }
                
                if (def_gid != undefined)
                {
                    var url = 'api/customerAlertGenerate/Generate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert('Template Content Saved Successfully!', 'success');
                            $state.go('app.customerAlert');
                        }
                        else {
                            unlockUI();
                            Notify.alert('Oops something went wrong!')                
                        }

                    });
                }
                else {
                    Notify.alert('Select Atleast One Deferral!')
                }       
            }
            }
         })();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customerAlertHistorycontroller', customerAlertHistorycontroller);

    customerAlertHistorycontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function customerAlertHistorycontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customerAlertHistorycontroller';
        activate();
        function activate() {



            $scope.customer_gid = localStorage.getItem('customer_gid');
            $scope.pageNavigation = localStorage.getItem('mailManagement');
         
            var params = {
                customer_gid: $scope.customer_gid
            };

            var url = 'api/customerAlertGenerate/mailHistory';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.mail_data = resp.data.mailhistory_list;
                unlockUI();
            });


           
        }

        $scope.view = function (val) {
            $scope.customermail_gid = val;
            $scope.customermail_gid = localStorage.setItem('customermail_gid', val);
            $state.go('app.mailHistoryView');
        }

        $scope.back = function () {
            if ($scope.pageNavigation == "mailManagement")
            {
                $state.go('app.mailManagement');
            }
            else
            {
                $state.go('app.customerAlert');
            }
           
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customeredit', customeredit);

    customeredit.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function customeredit($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customeredit';

        activate();

        function activate() {

            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = 'api/segment/segment';
            SocketService.get(url).then(function (resp) {
                $scope.segment_list = resp.data.segment_list;
            });

            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            $scope.customer_gid = localStorage.getItem('customer_gid');
            var url = 'api/customer/Getcustomerupdatedetails';
            var params = {
                customer_gid: $scope.customer_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerCodeedit = resp.data.customerCodeedit;
                $scope.customerNameedit = resp.data.customerNameedit;
                $scope.contactpersonedit = resp.data.contactPersonedit;
                $scope.contactnumberedit = resp.data.contactnoedit;
                $scope.emailedit = resp.data.emailedit;
                $scope.mobileNoedit = resp.data.mobileNoedit;
                $scope.txtaddress1 = resp.data.addressline1edit;
                $scope.txtaddress2 = resp.data.addressline2edit;
                $scope.segment = resp.data.segment_gid;
                $scope.segment_name = resp.data.segment_name;
                $scope.statename = resp.data.state;
                $scope.state_gid = resp.data.state_gid;
                $scope.employee_gid = resp.data.employee_gid;
                $scope.employee_name = resp.data.employee_name;
                console.log(resp.data.employee_name);
            });
            unlockUI();
        }


        $scope.customereditback = function () {
            $state.go('app.customerMaster');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customerMastercontroller', customerMastercontroller);

    customerMastercontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService','DownloaddocumentService'];

    function customerMastercontroller ($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customerMastercontroller';

        activate();


        function activate() {
          
            $scope.totalDisplayed=100;
            var url = 'api/customer/customerdetail';
            lockUI();
            SocketService.get(url).then(function (resp) {
               
                $scope.customer_data = resp.data.customer_list;
                unlockUI();
                // new code start   
                if ($scope.customer_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.customer_data.length;
                                        if ($scope.customer_data.length < 100) {
                                            $scope.totalDisplayed = $scope.customer_data.length;
                                        }
                                    }
                    // new code end
            });
            //$scope.total=$scope.customer_data.length;
           

           
        }

        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
             // new code start
        if ($scope.customer_data != null) {
       
                if (pagecount < $scope.customer_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.customer_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.customer_data.length;
                        Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };
        $scope.popupcustomer = function () {
          
            $state.go('app.addCustomer');
        }

        $scope.edit = function (val) {
            $scope.customer_gid = val;
            $scope.customer_gid = localStorage.setItem('customer_gid', val);
            $state.go('app.editCustomer');
        }


        $scope.updatecustomerURN = function (customer_gid,customername,customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/updateURN.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.customername=customername;
               
                if (customer_urn != "")
                {
                    $scope.customer_urn = customer_urn;
                }
                else {
                    $scope.customer_urn = "-";
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.UrnUpdate = function () {
                    
                    var params = {
                        customer_gid: customer_gid,
                        newcustomer_urn: $scope.txtnewcustomerURN,
                        currentcustomer_urn: customer_urn
                    }
                    
                    lockUI();
                    var url = "api/customer/GetNewCustomerURN";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
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
        }

        $scope.btntag2legal = function (customer_gid, customername, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/tagtoLegal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.customername = customername;

                if (customer_urn != "") {
                    $scope.customer_urn = customer_urn;
                }
                else {
                    $scope.customer_urn = "-";
                }


                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/Customer/TaggedHistoryList';
                SocketService.getparams(url, params).then(function (resp) {
                    //console.log(resp.data.customertag_list, url);
                    $scope.customertag_list = resp.data.customertag_list;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

       

                $scope.btnconfirm = function () {

                    var params = {
                        customer_gid: customer_gid,
                        customer_name: customername,
                        tag_remarks: $scope.txttag_remarks,
                        currentcustomer_urn: customer_urn
                    }

                    lockUI();
                    var url = "api/customer/TagtoLegal";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
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
        }
       
        $scope.delete = function (customer_gid) {
            var params = {
                customer_gid: customer_gid
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
                    var url = 'api/customer/customerDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };


        $scope.exportcustomerdata = function () {
            lockUI();
            var url = 'api/customer/ExportCustomer';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname);
                    unlockUI();

                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");                 
                    // var relpath1 = relPath[1].replace("\\", "/");             
                    // var hosts = window.location.host;                  
                    // var prefix = location.protocol + "//";                 
                    // var str = prefix.concat(hosts, relpath1);              
                    // var link = document.createElement("a");               
                    // var name = resp.data.lsname.split('.');                 
                    // link.download = name[0];                 
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')
                    activate();
                }

            });
        }

        $scope.btntag2npa = function (customer_gid, customername, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/tagtoNPA.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.customername = customername;

                if (customer_urn != "") {
                    $scope.customer_urn = customer_urn;
                }
                else {
                    $scope.customer_urn = "-";
                }


                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/Customer/TaggedNPAHistoryList';
                SocketService.getparams(url, params).then(function (resp) {
                    //console.log(resp.data.customertag_list, url);
                    $scope.customertagnpa_list = resp.data.customertagnpa_list;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };



                $scope.btnconfirmnpa = function () {

                    var params = {
                        customer_gid: customer_gid,
                        customer_name: customername,
                        tag_remarks: $scope.txttag_remarks,
                        currentcustomer_urn: customer_urn
                    }

                    lockUI();
                    var url = "api/customer/TagtoNPA";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
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
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('defapp', defapp);

    defapp.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function defapp($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'defapp';

        activate();
        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            $scope.tracking_type = localStorage.getItem('tracking_type');

            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/getDeferraldetail';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadDocumentname = resp.data;
                $scope.customerremarks = resp.data.customer_remarks;
                $scope.approval_remarks = resp.data.remarks;
                $scope.deferrals = resp.data.deferralSummaryDtls;
                $scope.deferral_gid = resp.data.deferral_gid;
                $scope.loanref_no = resp.data.loanref_no;
                $scope.loan_title = resp.data.loan_title;
                $scope.record_id = resp.data.record_id;
                $scope.deferral_name = resp.data.deferral_name;
                $scope.approval_status = resp.data.approval_status;
                $scope.def_status = resp.data.def_status;
                $scope.filename_list = resp.data.filename_list;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.businesshead_name = resp.data.businesshead_name;
                $scope.rm_name = resp.data.rm_name;
                $scope.credit_manager = resp.data.credit_manager;
                $scope.customer_code = resp.data.customer_code;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.entity_name = resp.data.entity_name;
                $scope.branch_name = resp.data.branch_name;
                $scope.deferral_approver_name = resp.data.cluster_manager_name;
                $scope.tracking_type = resp.data.tracking_type;

                if (resp.data.tracking_type == "Covenant") {
                    $scope.showrdb = true;
                }
                else {
                    $scope.showrdb = false;
                }

                if (resp.data.approval_status == "Approval Pending" || resp.data.approval_status == "Extension Approval Pending") {
                    $scope.uploaddoc = true;
                }
                else {
                    $scope.uploaddoc = false;
                }
                if (resp.data.approval_status == "Closed") {
                    $scope.uploaddocument = false;
                }
                else {
                    $scope.uploaddocument = true;
                }

            });

            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/checkerlist';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checker_list = resp.data.checker_list;
                if ($scope.checker_list == null) {
                    $scope.approval_history = true;
                }
                else {
                    $scope.approval_history = false;
                }
            });


            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/getdeferralstages';

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.stage_list = resp.data.stage_list;
                }
                else {
                    document.getElementById("stages").style.display = "none";
                }
            });

        }

        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('deferral_gid', $scope.deferral_gid);
            frm.append('loan_gid', $scope.loan_gid);
            frm.append('by', "cad");
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;

            var url = 'api/deferral/uploaddeferraldocumentbycad';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                unlockUI();
                $scope.filename_list = resp.data.filename_list;

                $("#addupload").val('');

                if (resp.data.status == true) {
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')
                }
            });
        }

        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            ////console.log(str);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
            // Close Modals
        }

        $scope.close = function (val) {
            document.getElementById("userform").reset();
            var doc = document.getElementById(val);
            doc.style.display = 'none';
        }
        $scope.Approve = function () {

            if ($scope.tracking_type == "Deferral" && $scope.deferral_status == "Extend") {

                if ($scope.extened_date == undefined) {
                    Notify.alert('Enter Extended Date')
                }
                else {

                    var params = {
                        def_gid: $scope.deferral_gid,
                        deferral_status: $scope.deferral_status,
                        due_date: $scope.extened_date,
                        approval_remarks: $scope.approval_remarks,
                        customer_remarks: $scope.customerremarks
                    }
                    var url = 'api/deferral/deferralApprove';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $state.go('app.cadApproval');
                            Notify.alert('Submitted Successfully..!!', 'success')
                        }
                        else {
                            Notify.alert('Extended Date must be greater than Due Date!')
                        }
                    });
                }
            }
            else if ($scope.tracking_type == "Covenant" && $scope.deferral_status == "Extend") {
                if ($scope.extened_date == undefined) {
                    Notify.alert('Enter Extended Date')
                }
                else if ($scope.rdb_extendtype == undefined) {
                    Notify.alert('Select Extend Type')
                }
                else {

                    var params = {
                        def_gid: $scope.deferral_gid,
                        deferral_status: $scope.deferral_status,
                        due_date: $scope.extened_date,
                        approval_remarks: $scope.approval_remarks,
                        customer_remarks: $scope.customerremarks,
                        extend_type: $scope.rdb_extendtype,
                    }
                    var url = 'api/deferral/deferralApprove';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $state.go('app.cadApproval');
                            Notify.alert('Submitted Successfully..!!', 'success')
                        }
                        else {
                            Notify.alert('Extended Date must be greater than Due Date!')
                        }
                    });
                }
            }
            else {

                var params = {
                    def_gid: $scope.deferral_gid,
                    deferral_status: $scope.deferral_status,
                    due_date: $scope.extened_date,
                    approval_remarks: $scope.approval_remarks,
                    customer_remarks: $scope.customerremarks
                }

                var url = 'api/deferral/deferralApprove';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $state.go('app.cadApproval');
                        Notify.alert('Submitted Successfully..!!', 'success')
                    }
                    else {
                        Notify.alert('Error Occurred While Submitting!')
                    }
                });
            }


        }




        $scope.deferralback = function (val) {
            $state.go('app.cadApproval');
        }


        $scope.onselectedchange = function (deferral_status) {
            if (deferral_status == "Extend") {
                $scope.showval = true;
            }
            else {
                $scope.showval = false;
            }
        }


    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('deferralcontroller', deferralcontroller);

    deferralcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function deferralcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'deferralcontroller';
        activate();
        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/deferral/deferralmasterSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.deferral = resp.data.deferral_list;
                unlockUI();
                console.log($scope.total);
                // new code start   
                if ($scope.deferral == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.deferral.length;
                                        if ($scope.deferral.length < 100) {
                                            $scope.totalDisplayed = $scope.deferral.length;
                                        }
                                    }
                    // new code end
                // $scope.total=$scope.deferral.length;
            });
           
      console.log($scope.totalDisplayed);
        }
        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            if ($scope.deferral != null) {
       
                        if (pagecount < $scope.deferral.length) {
                            $scope.totalDisplayed += Number;
                            if($scope.deferral.length<$scope.totalDisplayed){
                                $scope.totalDisplayed =$scope.deferral.length;
                                Notify.alert(" Total Summary " + $scope.deferral.length + " Records Only", "warning");
                            }
                            unlockUI();
                        }
                        else {
                            unlockUI();
                            Notify.alert(" Total Summary " + $scope.deferral.length + " Records Only", "warning");
                            return;
                        }
                    }
                    // new code end
                    // $scope.totalDisplayed += Number;
                    // console.log(pagecount);
                    unlockUI();
                };      
        /* ADD DEFERRAL */
        $scope.popupdeferral = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.deferralSubmit = function () {
                    if ($scope.comments == undefined) {
                        $scope.comments = ""
                    }

                    var params = {
                        deferral_code: $scope.deferral_code,
                        deferral_name: $scope.deferral_name,
                        criticallity: $scope.criticality,
                        comments: $scope.comments
                    }

                    var url = 'api/deferral/deferralmasterSubmit';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            $modalInstance.close('closed');
                            Notify.alert('Deferral Created Successfully..!!', {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {

                        }
                    });
                }
            }
        }
      

        /* EDIT DEFERRAL */
        $scope.edit = function (deferral_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.deferral_gid = deferral_gid;
                $scope.deferral_gid = localStorage.setItem('deferral_gid', deferral_gid);
                var params = {
                    deferral_gid: deferral_gid
                }
                //console.log(deferral_gid);
                var url = 'api/deferral/Getdeferralupdate';
                SocketService.getparams(url, params).then(function (resp) {
                    console.log(params);
                    $scope.deferralCodeedit = resp.data.deferralCodeedit;
                    $scope.deferralNameedit = resp.data.deferralNameedit;
                    $scope.criticality = resp.data.criticallity;
                    $scope.commentsEdit = resp.data.comments;
                    $scope.deferral_gid = resp.data.deferral_gid;

                });

                $scope.deferralUpdate = function () {

                    var params = {
                        deferralCodeedit: $scope.deferralCodeedit,
                        deferralNameedit: $scope.deferralNameedit,
                        criticallity: $scope.criticality,
                        comments: $scope.commentsEdit,
                        deferral_gid: $scope.deferral_gid
                    }
                    var url = 'api/deferral/deferralUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            $modalInstance.close('closed');
                            Notify.alert('Deferral Updated Successfully..!!', {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            //SweetAlert.swal('Deferral Updated Successfully..!!', 'success');

                        }
                        else {
                            SweetAlert.swal('Error Occurred While Updating Deferral !', 'warning');


                        }
                    });
                }

            }
        }
        /* DELETE DEFERRAL */
        $scope.delete = function (deferral_gid) {
            $scope.deferral_gid = localStorage.setItem('deferral_gid', deferral_gid);
            var params = {
                deferral_gid: deferral_gid
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
                    var url = 'api/deferral/deferralDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Customer!', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        
        }

    }


})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('deferralManagement', deferralManagement);

    deferralManagement.$inject = ['$rootScope', '$scope','$modal', '$state', 'SweetAlert','AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngTableParams'];

    function deferralManagement($rootScope, $scope,$modal, $state,SweetAlert,AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'deferralManagement';
        //$scope.loandata = [];
        activate();
        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/deferral/directDeferralSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.deferral_data = resp.data.deferralSummaryDtls;  
                // new code start  
                unlockUI(); 
                if ($scope.deferral_data== null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.deferral_data.length;
                                        if ($scope.deferral_data.length < 100) {
                                            $scope.totalDisplayed = $scope.deferral_data.length;
                                        }
                                    }
                    // new code end
                // if( $scope.deferral_data ==null){
                //     $scope.total= 0;
                // }   
                // else{
                    // $scope.total = $scope.deferral_data .length;
                // }         
            });
        }

        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
        if ($scope.deferral_data!= null) {
       
                if (pagecount < $scope.deferral_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.deferral_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.deferral_data.length;
                        Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };

        $scope.edit = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.editDeferral');
        }
       


        $scope.delete = function (val) {
            var params = {
                deferral_gid: val
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
                    var url = 'api/deferral/deferraldeleterecords';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                        }
                        else {
                            
                            activate();
                            SweetAlert.swal('Deferral Moved to multiple Stages.You cant Delete it!');
                        }
                    });
                  
                }

            });
           
        }


        $scope.popupdeferral = function () {
            $state.go('app.createDeferral');
        }

        

        $scope.popupView = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.viewDeferral');

        }


    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('deferralReport', deferralReport);

    deferralReport.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function deferralReport($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        activate();
        function activate() {
            $scope.totalDisplayed = 100;
            
            //var url = 'api/customer/customer';
            //SocketService.get(url).then(function (resp) {
            //    $scope.customer_list = resp.data.customer_list;
            //});

            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });
            var url = 'api/branch/branch';
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branch_list;

            });
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;

            });

            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            var url = 'api/entity/entity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
            });
            var url = 'api/deferral/deferralreportsummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.deferral_data = resp.data.deferralSummaryDtls;
                unlockUI();
                // new code start   
                if ($scope.deferral_data== null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.deferral_data.length;
                                        if ($scope.deferral_data.length < 100) {
                                            $scope.totalDisplayed = $scope.deferral_data.length;
                                        }
                                    }
                    // new code endd
                                // $scope.total=$scope.deferral_data.length;

            });
        }


        $scope.complete = function (string) {

            if (string.length >= 3) {
                $scope.message = "";
                var url = 'api/customer/ExploreCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        $scope.message = "No Records";
                    }


                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "Type atleast three character";
            }
        }

        $scope.fillTextbox = function (customer_gid, customer_name) {
            //console.log('string', customer_name, customer_gid);
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;


            
        }



        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };
        $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
        if ($scope.deferral_data!= null) {
       
                if (pagecount < $scope.deferral_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.deferral_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.deferral_data.length;
                        Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };
        $scope.all = function () {
            $scope.entity_gid = "";
            $scope.branch = "";
            $scope.customer_gid = "";
            $scope.customer = "";
            $scope.vertical = "";
            $scope.relationshipMgmt = "";
            $scope.zonalHead = "";
            $scope.businessHead = "";
            $scope.clustermanager = "";
            $scope.creditmanager = "";
            $scope.cbostate = "";
            activate();
        }

        $scope.search = function () {
            var params = {
                customer_gid: $scope.customer_gid,
                vertical_gid: $scope.vertical,
                entity_gid: $scope.entity_gid,
                branch_gid: $scope.branch,
                relationshipMgmt: $scope.relationshipMgmt,
                zonalHead: $scope.zonalHead,
                businessHead: $scope.businessHead,
                clustermanager: $scope.clustermanager,
                creditmanager: $scope.creditmanager,
                state_gid:$scope.cbostate
            }
            
            var url = 'api/deferral/directDeferralSummaryreport';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                $scope.deferral_data = resp.data.deferralSummaryDtls;
              });
        }

        $scope.popupView = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.ReportDetails');

        }

        $scope.export = function () {
            var params = {
                customer_gid: $scope.customer_gid,
                vertical_gid: $scope.vertical,
                branch_gid: $scope.branch,
                entity_gid: $scope.entity_gid,
                relationshipMgmt: $scope.relationshipMgmt,
                zonalHead: $scope.zonalHead,
                businessHead: $scope.businessHead,
                clustermanager: $scope.clustermanager,
                creditmanager: $scope.creditmanager,
                state_gid:$scope.cbostate
            }
            lockUI();
            var url = 'api/deferral/export';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname);
                    unlockUI();
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
                    activate();
                }
            });
        }

        $scope.exportnpa = function () {
            var params = {
                customer_gid: $scope.customer_gid,
                vertical_gid: $scope.vertical,
                branch_gid: $scope.branch,
                entity_gid: $scope.entity_gid,
                relationshipMgmt: $scope.relationshipMgmt,
                zonalHead: $scope.zonalHead,
                businessHead: $scope.businessHead,
                clustermanager: $scope.clustermanager,
                creditmanager: $scope.creditmanager,
                state_gid:$scope.cbostate
            }
            lockUI();
            var url = 'api/deferral/exportnpa';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname);
                    unlockUI();
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
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
        .controller('DtsRptUserReport2', DtsRptUserReport2);

    DtsRptUserReport2.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function DtsRptUserReport2($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'DtsRptUserReport2';

        activate();

        function activate() {
            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });
            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });
            var url = 'api/entity/entity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
            });
            $scope.page = localStorage.getItem('page');
            $scope.totalDisplayed = 100;

            var url = 'api/deferral/User2reportsummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.User2_data = resp.data.deferralSummaryDtls;
                unlockUI();
                if ($scope.User2_data == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.User2_data.length;
                    if ($scope.User2_data.length < 100) {
                        $scope.totalDisplayed = $scope.User2_data.length;
                    }
                }
            });
        }

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);

            if ($scope.User2_data != null) {

                if (pagecount < $scope.User2_data.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.User2_data.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.User2_data.length;
                        Notify.alert(" Total Summary " + $scope.User2_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.User2_data.length + " Records Only", "warning");
                    return;
                }
            }
            $scope.vertical = ""
            $scope.entity_gid=""
            $scope.cbostate=""
            unlockUI();
        };

        $scope.all = function () {
            $scope.entity_gid = "";
            $scope.vertical = "";
            $scope.cbostate = "";
            activate();
        }

        $scope.search = function () {
            var params = {
                vertical_gid: $scope.vertical,
                entity_gid: $scope.entity_gid,
                state_gid:$scope.cbostate
            }
            
            var url = 'api/deferral/User2reportsummarysearch';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                $scope.User2_data = resp.data.deferralSummaryDtls;
              });
        }

        $scope.popupView = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            localStorage.setItem('page', 'UserReport2');
            $state.go('app.reportpagedetails');
        }

        $scope.export = function () {
            if($scope.cbostate == undefined || $scope.cbostate == "")
            {
                $scope.cbostate = "";
            }
           
            var params = {
                vertical_gid: $scope.vertical,
                entity_gid: $scope.entity_gid,
                state_gid:$scope.cbostate        
              }
            lockUI();

            var url = 'api/deferral/UserReport2export';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname);
                    unlockUI();
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
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
        .controller('ecmsDashboard', ecmsDashboard);

    ecmsDashboard.$inject = ['$location', '$scope','SocketService'];

    function ecmsDashboard($location, $scope, SocketService) {
        /* jshint validthis:true */
        var vm = this;
       

        vm.title = 'ecmsDashboard';

        activate();

        function activate() {
            
            $scope.welcome_msg = 'Deferral Tracking System';
            var url = 'api/ecmsdashboard/ecmmsprivilege';
            var user_gid = localStorage.getItem('user_gid');
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {

                var customer = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMMSTCMR");
               
                var deferralmgmt = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNDFM");
                var loanmgmt = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNLON");
                var cadreport = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMRPTRPV");
                var userreport = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMRPTUSE");
                var deferralreport = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMRPTDFR");
                var rmdeferral = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNRMD");
                var transferrm = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNTRM");
                var defapproval = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNDRA");


                var reopendeferrals = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNRE");
                var collateralmanagement = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNCOL");
                var penaltyalert = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNPEA");

                var customeralert = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNCRA");
                var mailmanagement = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNCAM");
                var checkerapproval = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNCKA");

                var userreport2 = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMRPTUS2");
               
                if (customer != -1) {
                    $scope.customer_show = 'Y';
                }
                if (deferralmgmt != -1) {
                    $scope.deferralmgmt_show = 'Y';
                }
                if (loanmgmt != -1) {
                    $scope.loanmgmt_show = 'Y';
                }
                if (cadreport != -1) {
                    $scope.cadreport_show = 'Y';
                }
                if (userreport != -1) {
                    $scope.userreport_show = 'Y';
                }
                if (deferralreport != -1) {
                    $scope.deferralreport_show = 'Y';
                }
                if (rmdeferral != -1) {
                    $scope.rmdeferral_show = 'Y';
                }
                if (transferrm != -1) {
                    $scope.transferrm_show = 'Y';
                }
                if (defapproval != -1) {
                    $scope.deferralapproval_show = 'Y';
                }
                if (reopendeferrals != -1) {
                    $scope.reopendeferrals_show = 'Y';
                }
                if (collateralmanagement != -1) {
                    $scope.coltralmgmnt_show = 'Y';
                }
                else if (collateralmanagement == -1) {
                    $scope.coltralmgmnt_show = 'N';
                }
                if (penaltyalert != -1) {
                    $scope.penaltyalert_show = 'Y';
                }
                if (customeralert != -1) {
                    $scope.customeralert_show = 'Y';
                }
                if (mailmanagement != -1) {
                    $scope.mailmgmt_show = 'Y';
                }
                if (checkerapproval != -1) {
                    $scope.checkerapproval_show = 'Y';
                }
                if(userreport2 != -1)
                {
                    $scope.userreport2_show = 'Y';
                }
            });

        };
        $scope.customer = function () {
            $scope.welcome_msg = 'Customer';
        };
        $scope.loanmgmt = function () {
            $scope.welcome_msg = 'Loan Management';
        };
        $scope.deferralmgmt = function () {
            $scope.welcome_msg = 'Deferral Management';
        };
        $scope.cad_report = function () {
            $scope.welcome_msg = 'CAD Report';
        };
        $scope.user_report = function () {
            $scope.welcome_msg = 'User Report';
        };
        $scope.deferral_report = function () {
            $scope.welcome_msg = 'Deferral Report';
        };

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editcollateralcontroller', editcollateralcontroller);

    editcollateralcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function editcollateralcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editcollateralcontroller';

        activate();

        function activate() {

            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
               
            });

            var url = 'api/security/getSecuritytype';
            SocketService.get(url).then(function (resp) {
                $scope.security_data = resp.data.securitytype_list;
            });
            var param = {
                collateral_gid: localStorage.getItem('collateral_gid')
            };
            var url = "api/collateral/getCollateralDetails";
            SocketService.getparams(url, param).then(function (resp) {
                $scope.customer = resp.data.customer_gid;
                $scope.security_type = resp.data.securitytype_gid;
                $scope.account_status = resp.data.account_status;
                $scope.security_description = resp.data.security_description;
                $scope.loan_data = resp.data.collateralloandetails_list;
              
            });

        }
        $scope.back=function()
        {
            $state.go('app.collateralsummary');
        }
        $scope.update=function(collateral_gid)
        {
           
            var customername = $('#customer :selected').text();
            var securitytype = $('#security_type :selected').text();

            var params = {
                customer_gid: $scope.customer,
                customer_name: customername,
                securitytype_gid: $scope.security_type,
                security_type: securitytype,
                account_status: $scope.account_status,
                security_description: $scope.security_description,
                collateral_gid: localStorage.getItem('collateral_gid')
            };
            var url = "api/collateral/UpdateCollateral";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    console.log('test');
                    unlockUI()

                    Notify.alert('Collateral Updated Successfully..!!', 'success')

                    $state.go('app.collateralsummary');

                }

            });
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editCustomercontroller', editCustomercontroller);

    editCustomercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function editCustomercontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editCustomercontroller';

        activate();
        function activate() {
            $scope.Warning = false;
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.clusterlist = resp.data.employee_list;
                $scope.businesshead_list = resp.data.employee_list;
                $scope.regionalhead_list = resp.data.employee_list;
                $scope.relationshiplist = resp.data.employee_list;
                $scope.zonallist = resp.data.employee_list;
                $scope.creditlist = resp.data.employee_list;
                $scope.zonalrm_list = resp.data.employee_list;
                $scope.rm_list = resp.data.employee_list;
                $scope.riskmonitorning_list = resp.data.employee_list;
            });
            var url = 'api/customer/Getconstitution';
            SocketService.get(url).then(function (resp) {
                $scope.constitution_list = resp.data.constitution_list;
               
            });
            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });

            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            $scope.customer_gid = localStorage.getItem('customer_gid');  
            var url = 'api/customer/Getcustomerupdatedetails';
            var param = {
               customer_gid: $scope.customer_gid
            };

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {

                $scope.customerCodeedit = resp.data.customerCodeedit;
                $scope.customerNameedit = resp.data.customerNameedit;
                $scope.contactPersonedit = resp.data.contactPersonedit;
                $scope.mobileNoedit = resp.data.mobileNo_edit;
                $scope.contactnoedit = resp.data.contactno_edit;
                $scope.emailedit = resp.data.emailedit;
                $scope.txtaddress1 = resp.data.addressline1edit;
                $scope.txtaddress2 = resp.data.addressline2edit;
                $scope.regionedit = resp.data.regionedit;
                $scope.countryedit = resp.data.countryedit;
                $scope.vertical = resp.data.vertical_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.statename = resp.data.state;
                $scope.state_gid = resp.data.state_gid;
              
                $scope.postalcodeedit = resp.data.postalcode_edit;
                $scope.tomailedit = resp.data.tomailedit;
                $scope.ccmailedit = resp.data.ccmailedit;
               
                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.regionalHead = resp.data.regionalHeadGid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.creditmanager = resp.data.creditmanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.customerURNedit = resp.data.customer_urnedit;
                $scope.pan_number = resp.data.pan_number;
                $scope.gst_number = resp.data.gst_number;
                $scope.txtmajor_corporateedit = resp.data.major_corporateedit;
                $scope.cboconstitutionedit = resp.data.constitution_gidedit;
                $scope.ZonalRM = resp.data.zonal_riskmanagerGID;
                $scope.riskmanager = resp.data.risk_managerGID;
                $scope.RiskMonitoringName = resp.data.riskMonitoring_GID;

                unlockUI();
             
            });
            //$("#load").hide();
           
        }
        
        $scope.urnvalidation = function () {
            var params =
                {
                    urn: $scope.customerURNedit,
                }
            var url = 'api/MstCustomerAdd/GetURNInfo';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.Warning = true;
                }
                else {
                    $scope.Warning = false;
                }
            });
        }

        $scope.customereditback = function () {
            $state.go('app.customerMaster');
        }
       

        $scope.customerUpdate = function () {
            var params =
              {
                  urn: $scope.customerURNedit,
              }
            var url = 'api/MstCustomerAdd/GetURNInfo';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert('Already this URN is in Imported Customer', 'warning');
                }
                else {
                    var zonalHead_name = $('#zonalHead_name :selected').text();
                    var businessHead_name = $('#businessHead_name :selected').text();
                    var regionalHead_name = $('#regionalHead_name :selected').text();
                    var vertical_code = $('#vertical_code :selected').text();
                    var cluster_manager_name = $('#cluster_manager_name :selected').text();
                    var relationshipMgmt_name = $('#relationshipMgmt_name :selected').text();
                    var creditmanager_name = $('#creditmanager_name :selected').text();
                    var state_name = $('#statename :selected').text();
                    var constitutionname = $('#constitutionname :selected').text();
                    var zonlRM_name = $('#zonlRM_name :selected').text();
                    var riskmanager_name = $('#riskmanager_name :selected').text();
                    var RiskMonitoring_Name = $('#RiskMonitoring_Name :selected').text();
                    var params = {
                        customer_gid: $scope.customer_gid,
                        customerCodeedit: $scope.customerCodeedit,
                        customerNameedit: $scope.customerNameedit,
                        contactPersonedit: $scope.contactPersonedit,
                        mobileNoedit: $scope.mobileNoedit,
                        contactnoedit: $scope.contactnoedit,
                        emailedit: $scope.emailedit,
                        addressline1edit: $scope.txtaddress1,
                        regionedit: $scope.regionedit,
                        addressline2edit: $scope.txtaddress2,
                        countryedit: $scope.countryedit,
                        vertical_gid: $scope.vertical,
                        vertical_code: vertical_code,
                        state_gid: $scope.state_gid,
                        state: state_name,
                        tomailedit: $scope.tomailedit,
                        ccmailedit: $scope.ccmailedit,
                        postalcodeedit: $scope.postalcodeedit,
                        zonalGid: $scope.zonalHead,
                        businessHeadGid: $scope.businessHead,
                        regionalHeadGid: $scope.regionalHead,
                        clustermanagerGid: $scope.clustermanager,
                        creditmanagerGid: $scope.creditmanager,
                        relationshipMgmtGid: $scope.relationshipMgmt,
                        zonal_name: zonalHead_name,
                        businesshead_name: businessHead_name,
                        regionalhead_name: regionalHead_name,
                        cluster_manager_name: cluster_manager_name,
                        creditmanager_name: creditmanager_name,
                        relationshipmgmt_name: relationshipMgmt_name,
                        customer_urnedit: $scope.customerURNedit,
                        gst_number: $scope.gst_number,
                        pan_number: $scope.pan_number,
                        major_corporateedit: $scope.txtmajor_corporateedit,
                        constitution_nameedit: constitutionname,
                        constitution_gidedit: $scope.cboconstitutionedit,
                        zonal_riskmanagerGID: $scope.ZonalRM,
                        zonal_riskmanagerName: zonlRM_name,
                        risk_managerGID: $scope.riskmanager,
                        risk_managerName: riskmanager_name,
                        riskMonitoring_GID: $scope.RiskMonitoringName,
                        riskMonitoring_Name: RiskMonitoring_Name
                    }
                    var url = 'api/customer/customerUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            $state.go('app.customerMaster');
                            Notify.alert('Customer Updated Successfully..!!', 'success')
                        }

                        else {
                            Notify.alert(resp.data.message)
                        }
                        activate();
                    });
                }
            });
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editDeferral', editDeferral);

    editDeferral.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function editDeferral($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editDeferral';
        var customer_remarks;
        var checker_status;
        activate();
        function activate() {
            $('#loandata').multiselect({ includeSelectAllOption: true });


            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats=["dd-MM-yyyy"];
            vm.format=vm.formats[0];

            //vm.initDate = new Date('2019-10-01');
            //vm.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd-MM-yyyy', 'shortDate'];
            //vm.format = vm.formats[2];
            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });

            var url = 'api/entity/entity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
            });

            var url = 'api/branch/branch';
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branch_list;
            });

            var url = 'api/deferral/deferral';
            SocketService.get(url).then(function (resp) {
                $scope.deferral_list = resp.data.deferral_list;
            });
            var url = 'api/covenanttype/getcovenanttype';
            SocketService.get(url).then(function (resp) {
                $scope.covenanttype_list = resp.data.covenanttype_list;
            });
            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.clusterlist = resp.data.employee_list;
                $scope.businesshead_list = resp.data.employee_list;
                $scope.relationshiplist = resp.data.employee_list;
                $scope.zonallist = resp.data.employee_list;
                $scope.creditlist = resp.data.employee_list;
            });

            var params = {
                deferral_gid: localStorage.getItem('deferral_gid')
            }
            var url = 'api/deferral/checkerlist';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checker_list = resp.data.checker_list;
            });

            var param = {
                deferral_gid: localStorage.getItem('deferral_gid')
            };

            var url = 'api/deferral/Getdeferraldetails';
            SocketService.getparams(url, param).then(function (resp) {
                //console.log(resp.data)
                $scope.Getdeferral = resp.data;
                $scope.branchEdit = resp.data.branch_gid;
                $scope.entityEdit = resp.data.entity_gid;
                $scope.customer = resp.data.customer_gid;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.cluster_manager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmgmt_name = resp.data.creditmanager_gid;
                $scope.tracking_type = resp.data.tracking_type;
                $scope.loanRefNoedit = resp.data.loanGID;
                $scope.due_dateedit = resp.data.due_date;
                $scope.due_dateedit = Date.parse($scope.due_dateedit);
                if (resp.data.tracking_type == "Covenant") {
                    $scope.showval = true;
                    $scope.hideval = false;
                }
                else {
                    $scope.showval = false;
                    $scope.hideval = true;
                }
                $scope.deferralname = resp.data.deferraltype_gid;
                $scope.covenanttype = resp.data.covenanttype_gid;
                $scope.deferralcategoryedit = resp.data.deferral_category;
                $scope.vertical = resp.data.vertical_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.criticallity = resp.data.criticallity;
                $scope.remarks = resp.data.remarks;
                $scope.customerremarks = resp.data.customerremarks;
                $scope.checker_status = resp.data.checker_status;
                $scope.checker_remarks = resp.data.checker_remarks;
                if (resp.data.checker_status == "PushBack") {
                    $scope.showvalchecker_pushback = true;
                }
                else {
                    $scope.showvalchecker_pushback = false;
                }

            });


            $scope.onselectedchangedeferral = function (val) {
                var params = {
                    deferral: val
                };
                var url = 'api/loan/getdeferralcriticallity';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.MDLcriticallity = resp.data;
                    $scope.criticallity = resp.data.criticallity;
                    $scope.remarks = resp.data.comments;
                    $scope.customerremarks=resp.data.comments;
                    $scope.critical = true
                });
            }

            $scope.onselectedchangecovenant = function (covenanttype) {
                var params = {
                    covenanttype: covenanttype
                };
                var url = 'api/loan/getcovenanttypecriticallity';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.MDLcriticallity = resp.data;
                    $scope.criticallity = resp.data.criticallity;
                    $scope.remarks = resp.data.comments;
                    $scope.customerremarks=resp.data.comments;
                    $scope.critical = true
                });
            }
            $scope.btncopy = function () {
                customer_remarks = $scope.remarks;
                $scope.customerremarks = customer_remarks;
             }
            $scope.onselectedchangecustomer = function (customer) {
                var params = {
                    customer_gid: customer
                }
                var url = 'api/loan/customer_getheads';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.vertical_code = resp.data.vertical_code;
                    $scope.zonalHead = resp.data.zonalGid;
                    $scope.businessHead = resp.data.businessHeadGid;
                    $scope.clustermanager = resp.data.clustermanagerGid;
                    $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                    $scope.creditmgmt_name = resp.data.creditmanager_gid;
                });
            }
            var url = 'api/deferral/Getcaddoc';
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.filename_list = resp.data.filename_list;
                }
            });
        }
        $scope.isShowHide = function (param) {
            if (param == "show") {
                $scope.showval = true;
                $scope.hideval = false;
            }
            else if (param == "hide") {
                $scope.showval = false;
                $scope.hideval = true;
            }
            else {
                $scope.showval = false;
                $scope.hideval = false;
            }
        }
        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('deferral_gid', $scope.deferral_gid);
            frm.append('loan_gid', $scope.loan_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            document.getElementById('load').style.visibility = "visible";
            var url = 'api/deferral/UploadcadDocument';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                unlockUI();
                $scope.filename_list = resp.data.filename_list;

                $("#addupload").val('');

                if (resp.data.status == true) {
                    activate();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')
                }
                activate();
            });
        }
        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.deferralback = function () {
            $state.go('app.DeferralManagement');
        }
        $scope.deferralUpdateEdit = function () {
            //var date = new Date($scope.due_dateedit);
            //date = date.getFullYear() + '-' + date.getMonth() + '-' + date.getDate() + ' ' + date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();
           
            var entity_name_edit;
            var branch_name_edit;
            var deferraltype_name_edit;
            var covenanttype_name_edit;
            var customername_edit;
            var zonalhead_name_edit;
            var businesshead_name_edit;
            var clusterhead_name_edit;
            var relationalmgr_name_edit;
            var creditmanager_name_edit;

           // var vertical_code = $('#vertical_code :selected').text();
            var entity_index = $scope.entity_list.map(function (e) { return e.entity_gid }).indexOf($scope.entityEdit);
            if (entity_index == -1) { entity_name_edit = ''; } else { entity_name_edit = $scope.entity_list[entity_index].entity_name; };
            var branch_index = $scope.branch_list.map(function (e) { return e.branch_gid }).indexOf($scope.branchEdit);
            if (branch_index == -1) { branch_name_edit = ''; } else { branch_name_edit = $scope.branch_list[branch_index].branch_name; };
            var customer_index = $scope.customer_list.map(function (e) { return e.customer_gid }).indexOf($scope.customer);
            if (customer_index == -1) { customername_edit = ''; } else { customername_edit = $scope.customer_list[customer_index].customername; };
            var zonalhead_index = $scope.zonallist.map(function (e) { return e.employee_gid }).indexOf($scope.zonalHead);
            if (zonalhead_index == -1) { zonalhead_name_edit = ''; } else { zonalhead_name_edit = $scope.zonallist[zonalhead_index].employee_name; }
            var businesshead_index = $scope.businesshead_list.map(function (e) { return e.employee_gid }).indexOf($scope.businessHead);
            if (businesshead_index == -1) { businesshead_name_edit = ''; } else { businesshead_name_edit = $scope.businesshead_list[businesshead_index].employee_name; }
            var clusterhead_index = $scope.clusterlist.map(function (e) { return e.employee_gid }).indexOf($scope.cluster_manager);
            if (clusterhead_index == -1) { clusterhead_name_edit = ''; } else { clusterhead_name_edit = $scope.clusterlist[clusterhead_index].employee_name; }
            var reletinalmgr_index = $scope.relationshiplist.map(function (e) { return e.employee_gid }).indexOf($scope.relationshipMgmt);
            if (reletinalmgr_index == -1) { relationalmgr_name_edit = ''; } else { relationalmgr_name_edit = $scope.relationshiplist[reletinalmgr_index].employee_name; }
            var creditmanager_index = $scope.creditlist.map(function (e) { return e.employee_gid }).indexOf($scope.creditmgmt_name);
            if (creditmanager_index == -1) { creditmanager_name_edit = ''; } else { creditmanager_name_edit = $scope.creditlist[creditmanager_index].employee_name; }
            var deferraltype_index = $scope.deferral_list.map(function (e) { return e.deferraltype_gid }).indexOf($scope.deferralname);
            if (deferraltype_index == -1) { deferraltype_name_edit = '' } else { deferraltype_name_edit = $scope.deferral_list[deferraltype_index].deferral_name; };
            var covenanttype_index = $scope.covenanttype_list.map(function (e) { return e.covenanttype_gid }).indexOf($scope.covenanttype);
            if (covenanttype_index == -1) { covenanttype_name_edit = '' } else { covenanttype_name_edit = $scope.covenanttype_list[covenanttype_index].covenanttype_name; };
            if ($scope.tracking_type == 'Covenant') { $scope.deferralname = ''; deferraltype_name_edit = ''; } else { $scope.covenanttype = ''; covenanttype_name_edit = ''; };
           
            var params = {
                deferral_gid: localStorage.getItem('deferral_gid'),
                entity_gid:$scope.entityEdit,
                entity_name:entity_name_edit,
                branch_gid:$scope.branchEdit,
                branch_name:branch_name_edit,
                customer_gid: $scope.customer,
                customer_name: customername_edit,
                loan_gid: $scope.loanRefNoedit,
                zonalhead_gid: $scope.zonalHead,
                zonalhead_name: zonalhead_name_edit,
                businesshead_gid: $scope.businessHead,
                businesshead_name: businesshead_name_edit,
                clustermgr_gid: $scope.cluster_manager,
                clusterhead_name: clusterhead_name_edit,
                relationmgr_gid: $scope.relationshipMgmt,
                relationmgr_name: relationalmgr_name_edit,
                creditmgr_gid: $scope.creditmgmt_name,
                creditmgr_name: creditmanager_name_edit,
                category_gid: $scope.deferralcategoryedit,
                tracking_type: $scope.tracking_type,
                deferraltype_gid: $scope.deferralname,
                deferraltype_name: deferraltype_name_edit,
                covenanttype_gid: $scope.covenanttype,
                covenanttype_name: covenanttype_name_edit,
                duedate: $scope.due_dateedit,
                //duedate: date,
                criticallity: $scope.criticallity,
                vertical_code:$scope.vertical_code,
                vertical_gid: $scope.vertical,
                remarks: $scope.remarks,
                customerremarks: $scope.customerremarks,
                checker_status: $scope.checker_status
            }
            if($scope.tracking_type=='Deferral'){
                console.log($scope.deferralname);
                if($scope.deferralname==''){
                    Notify.alert('Kindly select the deferral','warning');
                    return;
                }
            
            }
            else if($scope.tracking_type=='Covenant'){
                console.log($scope.covenanttype);
                if($scope.covenanttype==''){
                    Notify.alert('Kindly select the covenant','warning');
                    return;
                }
               
            }
            var url = 'api/deferral/update';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    $state.go('app.DeferralManagement');
                    Notify.alert('Deferral Updated Successfully..!!', 'success')
                }

                else {
                    Notify.alert('Error Occurred While Updating Deferral !')
                }
            });
        }
    }
})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editLoan', editLoan);

    editLoan.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function editLoan($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        var loanValues;
        vm.title = 'editLoan';
        activate();
        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };


            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.clusterlist = resp.data.employee_list;
                $scope.businesshead_list = resp.data.employee_list;
                $scope.relationshiplist = resp.data.employee_list;
                $scope.zonallist = resp.data.employee_list;
                $scope.creditlist = resp.data.employee_list;
            });


            //var url = 'api/customer/customer';
            //SocketService.get(url).then(function (resp) {
            //    $scope.customer_list = resp.data.customer_list;
            //});



            lockUI();
            $scope.loan_gid = localStorage.getItem('loan_gid');
            var url = 'api/loan/getLoandetails';
            var param = {
                loan_gid: $scope.loan_gid
            };


            SocketService.getparams(url, param).then(function (resp) {
                $scope.customer_gid = resp.data.customer_gid;
                var customer = resp.data.customerName.split("/");
                $scope.customer = customer[1];
                $scope.vertical = resp.data.vertical_gid;
                $scope.loanRefNoedit = resp.data.loanRefNo;
                $scope.sanctionrefnoedit = resp.data.sanctionRefno;
                $scope.sanctionDateedit = resp.data.sanction_date;
                $scope.sanction_Date = resp.data.sanctionDate;
               
                //$scope.sanctionDateedit = Date.parse(resp.data.sanctionDate);
                //$scope.sanctionDateedit = resp.data.sanctionDate;
                $scope.loanmaster_gid = resp.data.loanmaster_gid;
                 
                $scope.zonalHead = resp.data.zonal_gid;
                $scope.businessHead = resp.data.businesshead_gid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipmgmt_gid;
                $scope.creditmgmt_name = resp.data.creditmanager_gid;

                unlockUI();
                //.log(resp.data);

                var params = {
                    customer_gid: resp.data.customer_gid
                }
                var url = 'api/loan/customer_getheads';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.mdlheadsofcustomer = resp.data;
                    $scope.sanctiondtl = resp.data.sanctiondtl;

                });
                $scope.cbosanctionGid = resp.data.sanction_Gid;

                var params = {
                    customer2sanction_gid: $scope.cbosanctionGid
                }
                var url = 'api/loan/SanctionLoanFacility';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        $scope.loan_list = resp.data.loanfacility;

                    }

                });

                //$scope.loan = $scope.loanmaster_gid;

            });




        }


        $scope.complete = function (string) {

            if (string.length >= 3) {
                $scope.message = "";
                var url = 'api/customer/ExploreCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        $scope.message = "No Records";
                    }


                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "Type atleast three character";
            }
        }

        $scope.fillTextbox = function (customer_gid, customer_name) {
            //console.log('string', customer_name, customer_gid);
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;




            var params = {
                customer_gid: customer_gid
            }

            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.mdlheadsofcustomer = resp.data;
                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmgmt_gid = resp.data.creditmanager_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.creditmgmt_name = resp.data.creditmanager_gid;
                $scope.sanctiondtl = resp.data.sanctiondtl;
                $scope.loan_list = {};
                $scope.vertical = true;

            });
        }


        $scope.sanctionrefnochange = function (cbosanctionGid) {
            var params = {
                sanction_gid: cbosanctionGid
            }

            var url = 'api/loan/GetSanctionDate';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.sanctionDateedit = resp.data.sanctiondate;
                $scope.sanction_Date = resp.data.Sanction_Date;

            });
            $scope.loan_list = {};

            var params = {
                customer2sanction_gid: cbosanctionGid
            }
            var url = 'api/loan/SanctionLoanFacility';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                   
                    if (resp.data.count_loan == '1') {
                       
                        $scope.loan_list = resp.data.loanfacility;
                        $scope.loanmaster_gid = resp.data.loanfacility[0].facility_gid;
                    }
                    else {
                        $scope.loan_list = resp.data.loanfacility;
                    }
                   
                }
                else {
                    $scope.loan = '-----Select Loan Facility Type----';
                }

            });
        }


        $scope.onselectedchangecustomer = function (customer) {
            var params = {
                customer_gid: $scope.cbocustomergid.customer_gid
            }
            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.mdlheadsofcustomer = resp.data;
                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmgmt_name = resp.data.creditmanager_gid;
                $scope.sanctiondtl = resp.data.sanctiondtl;
                $scope.loan_list = {};

            });
        }

        $scope.loanback = function () {
            $state.go('app.loanManagement');
        }

        $scope.loanUpdate = function (val) {
            //var customername = $('#customername :selected').text();
            var vertical_code = $('#vertical_code :selected').text();
            var loanTitle = $('#loanTitle :selected').text();
            var sanctionrefno = $('#sanctionrefno :selected').text();
            var zonal_name = $('#zonal_name :selected').text();
            var businesshead_name = $('#businesshead_name :selected').text();
            var cluster_manager_name = $('#cluster_manager_name :selected').text();
            var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
            var creditmgmt_name = $('#creditmanager_name :selected').text();
           

            var params = {
                loan_gid: $scope.loan_gid,
                customer_gid: $scope.customer_gid,
                customer_name: $scope.customer,
                vertical_gid: $scope.vertical,
                sanctionGid: $scope.cbosanctionGid,
                loanRefNoedit: $scope.loanRefNoedit,
                sanctionrefnoedit: sanctionrefno,
                sanctionDateedit: $scope.sanction_Date,
                loanmaster_gid: $scope.loanmaster_gid,
                loanTitleedit: loanTitle,
                vertical_code: vertical_code,
                zonalGid: $scope.zonalHead,
                businessHeadGid: $scope.businessHead,
                clustermanagerGid: $scope.clustermanager,
                relationshipMgmtGid: $scope.relationshipMgmt,
                creditmanager_name: creditmgmt_name,
                zonal_name: zonal_name,
                businesshead_name: businesshead_name,
                cluster_manager_name: cluster_manager_name,
                relationshipmgmt_name: relationshipmgmt_name,
                creditmanager_gid: $scope.creditmgmt_name,
            }
            console.log(params);

            var url = 'api/loan/loanUpdate';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                   
                    $state.go('app.loanManagement');
                    Notify.alert('Loan Updated Successfully..!!', 'success')
                }

                else {
                    Notify.alert(resp.data.message, 'warning')
                }

            });
        }

    }



})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('extensionApprovalcontroller', extensionApprovalcontroller);

    extensionApprovalcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$filter', 'ngTableParams', '$resource', '$timeout', 'DownloaddocumentService'];
    function extensionApprovalcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $filter, ngTableParams, $resource, $timeout, DownloaddocumentService) {
       // var vm = this;
        //   vm.title = 'extensionApprovalcontroller';

        activate();
        function activate() {
            var url = apiManage.apiList['extensionSummary'].api;

            SocketService.get(url).then(function (resp) {
                $scope.cadApproval = resp.data.deferralSummaryDtls;

                //$scope.usersTable = new ngTableParams({
                //    page: 1,
                //    count: 5

                //}, {
                //    total: $scope.cadApprovaldata.length,
                //    getData: function ($defer, params) {
                //        $scope.cadApproval = $scope.cadApprovaldata.slice((params.page() - 1) * params.count(), params.page() * params.count());
                //        $defer.resolve($scope.cadApproval);
                //    }
                //});
            });
        };


        /////////////////////////


        $scope.popupapproveExtension = function (deferral_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/extensionDetails.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.deferral_gid = deferral_gid;
                $scope.deferral_gid = localStorage.setItem('deferral_gid', deferral_gid);
                var params = {
                    deferral_gid: deferral_gid
                }

                var url = apiManage.apiList['GetDetails'].api;
            
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {                     
                        $scope.UploadDocumentname = resp.data;
                        $scope.deferral_gid = resp.data.deferral_gid;
                        $scope.deferralextension_gid = resp.data.deferralextension_gid;
                        $scope.loanref_no = resp.data.loanref_no;
                        $scope.loan_title = resp.data.loan_title;
                        $scope.record_id = resp.data.record_id;
                        $scope.deferral_name = resp.data.deferral_name;
                        $scope.filename_list = resp.data.filename_list;
                        $scope.extensionapplied_remarks = resp.data.extensionapplied_remarks;
                        //document.getElementById("tabledocuments").style.visibility = "visible";
                    }
                    else if (resp.data.status == false) {
                        //unlockUI();
                        //$scope.UploadDocumentname = resp.data;
                        //$scope.deferral_gid = resp.data.deferral_gid;
                        //$scope.deferralextension_gid = resp.data.deferralextension_gid;
                        //$scope.loanref_no = resp.data.loanref_no;
                        //$scope.loan_title = resp.data.loan_title;
                        //$scope.deferral_id = resp.data.deferral_id;
                        //$scope.deferral_name = resp.data.deferral_name;
                        //$scope.extensionapplied_remarks = resp.data.extensionapplied_remarks;
                        //document.getElementById("tabledocuments").style.visibility = "hidden";

                      
                      
                        Notify.alert('No Extension Request Raised!', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });
               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };

               
                $scope.downloads = function (val1, val2) {
                    //var phyPath = val1;
                    //var relPath = phyPath.split("StoryboardAPI");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = val2.split(".")
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();

                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
            }
        };


        ///////////////////////////
        // View Issue 
        //$scope.popupapproveExtension = function (deferral_gid) {
        //    var doc = document.getElementById('popupapproveExtension');

        //    doc.style.display = 'block';
        //    $scope.deferral_gid = deferral_gid;
        //    $scope.deferral_gid = localStorage.setItem('deferral_gid', deferral_gid);
        //    var params = {
        //        deferral_gid: deferral_gid
        //    }

        //    var url = apiManage.apiList['GetDetails'].api;
        //    lockUI();
        //    SocketService.getparams(url, params).then(function (resp) {
        //        if (resp.data.status == true) {
        //            unlockUI();
        //            $scope.UploadDocumentname = resp.data;
        //            $scope.deferral_gid = resp.data.deferral_gid;
        //            $scope.deferralextension_gid = resp.data.deferralextension_gid;
        //            $scope.loanref_no = resp.data.loanref_no;
        //            $scope.loan_title = resp.data.loan_title;
        //            $scope.deferral_id = resp.data.deferral_id;
        //            $scope.deferral_name = resp.data.deferral_name;
        //            $scope.filename_list = resp.data.filename_list;
        //            $scope.extensionapplied_remarks = resp.data.extensionapplied_remarks;
        //            document.getElementById("tabledocuments").style.visibility = "visible";
        //        }
        //        else if (resp.data.status == false) {
                   

        //            unlockUI();
        //            console.log(resp.data);
        //            doc.style.display = 'none';
        //            Notify.alert('No Extension Request Raised!', {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });

        //        }
        //    });
        //};

        $scope.approveExtension = function () {
            var params = {
                deferral_gid: $scope.deferral_gid,
                deferralextension_gid: $scope.deferralextension_gid,
                extensionapproval_remarks: $scope.extensionapproval_remarks,
                due_date: $scope.due_date
            }

            var url = apiManage.apiList['approveExtension'].api;
            lockUI();
            SocketService.post(apiManage.apiList['approveExtension'].api, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.close('popupapproveExtension');
                    location.reload();
                    Notify.alert('Extension Approved Successfully..!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert('Error While Approving Extension !', {
                        status: 'warning',
                        pos: 'top-right',
                        timeout: 3000
                    });

                }
            });
        };

        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = "http://"
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        };

        $scope.close = function (val) {
            document.getElementById("userformextension").reset();
            var doc = document.getElementById(val);
            doc.style.display = 'none';
        };

    };
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('landingcontroller', landingcontroller);

    landingcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function landingcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'landingcontroller';

        activate();


        function activate() {
            var url = apiManage.apiList['notification'].api;
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
               
                $scope.count_acknowledgement = resp.data.count_acknowledgement;
                $scope.count_myasset = resp.data.count_myasset;
                $scope.count_surrender = resp.data.count_surrender;
                $scope.count_temporaryhandover = resp.data.count_temporaryhandover;
                $scope.employee_id = resp.data.employee_id;
                $scope.count_response = resp.data.count_response;
                $scope.count_myapprovals = resp.data.count_myapprovals;
            });
        }
        $scope.view = function (val,count) {
                $scope.employee_id = val;
                ScopeValueService.store("ackCtrl", $scope);
                $state.go('app.viewasset');
        };
        $scope.acknowledge = function (val, count) {
            $scope.employee_id = val;
            ScopeValueService.store("ackCtrl", $scope);
            $state.go('app.acknowledgemyasset');
        };

        $scope.surrender = function (val, count) {
                $scope.employee_id = val;
                ScopeValueService.store("ackCtrl", $scope);
                $state.go('app.assetsurrender');
        };

        $scope.temporaryhandover = function (val, count) {
                $scope.employee_id = val;
                ScopeValueService.store("ackCtrl", $scope);
                $state.go('app.temporaryhandover');
        };

         $scope.newserviceticket = function (val) {
         $scope.employee_id = val;
         ScopeValueService.store("ackCtrl", $scope);
         $state.go('app.newservice_ticket');
        };
         $scope.newtaskrequest = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.newtaskrequest');
         };
        
         $scope.viewserviceticket = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.viewserviceticket');
         };
         $scope.myapprovals = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.myapprovals');
         };
         $scope.loanManagement = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.loanManagement');
         };
         $scope.rmManagement = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.rmManagement');
         };
         $scope.deferralManagement = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.DeferralManagement');
         };
         $scope.segment = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.segment');
         };
         $scope.deferral = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.deferral');
         };
         $scope.covenantType = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.covenantType');
         };
         $scope.transferRM = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.transferRM');
         };
         $scope.customerMaster = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.customerMaster');
         };
         $scope.cadApproval = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.cadApproval');
         };

         $scope.extensionApproval = function (val) {
             $scope.employee_id = val;
             ScopeValueService.store("ackCtrl", $scope);
             $state.go('app.extensionApproval');
         };
         $scope.adminlogin = function () {
                 var url = apiManage.apiList['SValues'].api;
                 lockUI();
                 SocketService.get(url).then(function (resp) {
                     unlockUI();
                     var host = window.location.host;
                     var prefix = "https://"
                     var win= window.open(prefix.concat(host,"/Framework/adlogin.aspx?userCode=",resp.data.user_code,"&?&userPassword=",resp.data.user_password,"&?&companyCode=",resp.data.company_code),'_blank');
                    win.focus();
                 })
             };
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('loan2deferralcontroller', loan2deferralcontroller);

    loan2deferralcontroller.$inject = ['$rootScope', '$modal', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function loan2deferralcontroller($rootScope, $modal, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'loan2deferralcontroller';
        var customer_gid;
        activate();
        var customer_remarks;

        function activate() {
            $scope.activeAdd = true;
            $scope.DivEdittab = false;
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            $scope.btnupdate = false;
            $scope.btnsubmit = true;
            $scope.loan_gid = localStorage.getItem('loan_gid');
            var url = 'api/loan/getLoandeferraldetails';
            var param = {
                loan_gid: $scope.loan_gid
            };

            SocketService.getparams(url, param).then(function (resp) {
                $scope.LoanDetails = resp.data;
                $scope.customerName = resp.data.customerName;
                $scope.loanRefNo = resp.data.loanRefNo;
                $scope.loanTitle = resp.data.loanTitle;
                $scope.sanctionRefno = resp.data.sanctionRefno;
                $scope.sanctionDate = resp.data.sanctionDate;
                customer_gid = resp.data.customer_gid;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.vertical_gid = resp.data.vertical_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.zonalHead = resp.data.zonal_gid;
                $scope.businessHead = resp.data.businesshead_gid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipmgmt_gid;
                $scope.creditmanager = resp.data.creditmanager_gid;
                $scope.zonal_name = resp.data.zonal_name,
                $scope.businesshead_name = resp.data.businesshead_name,
                $scope.rm_name = resp.data.rm_name,
                $scope.cluster_manager_name = resp.data.cluster_manager_name;
                $scope.creditmgmt_name = resp.data.creditmgmt_name;
                $scope.entityName = resp.data.entityName;
                $scope.entity_gid = resp.data.entity_gid;
                $scope.branchName = resp.data.branchName;
                $scope.branch_gid = resp.data.branch_gid;
            });



            var url = 'api/deferral/deferralSummary';
            var param = {
                loan_gid: $scope.loan_gid
            };
            SocketService.getparams(url, param).then(function (resp) {
                $scope.deferrals = resp.data.deferralSummaryDtls;
                //console.log(resp.data);
            });
            var url = 'api/deferral/deferral';
            SocketService.get(url).then(function (resp) {
                $scope.deferral_list = resp.data.deferral_list;
            });

            var url = 'api/entity/entity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
            });

            var url = 'api/branch/branch';
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branch_list;
            });

            var url = 'api/covenanttype/getcovenanttype';
            SocketService.get(url).then(function (resp) {
                //unlockUI();
                $scope.covenanttype_list = resp.data.covenanttype_list;
            });

            var params = {
                deferral_gid: ''
            }
            var url = 'api/deferral/Getcaddoc';

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.filename_list = resp.data.filename_list;
                }

            });
            //$scope.showdiv = true;
    }

      

        $scope.onselectedchangedeferral = function () {
            $scope.critical = true;
                $scope.MDLcriticallity = $scope.deferralname;
                $scope.criticallity = $scope.deferralname.criticallity;
                $scope.remarks = $scope.deferralname.comments;
                $scope.customerremarks=$scope.deferralname.comments;
           
            //activate();
        }

        $scope.onselectedchangecovenant = function () {
            $scope.critical = true;
                $scope.MDLcriticallity = $scope.covenanttype;
                $scope.criticallity = $scope.covenanttype.criticallity;
                $scope.remarks = $scope.covenanttype.comments;
                $scope.customerremarks=$scope.covenanttype.comments;
            //activate();
        }
        $scope.btncopy = function () {
            customer_remarks = $scope.remarks;
            $scope.customerremarks = customer_remarks;
         }

        $scope.isShowHide = function (param) {
            if (param == "show") {
                $scope.showval = true;
                $scope.hideval = false;
                $scope.showdiv = true;
            }
            else if (param == "hide") {
                $scope.showval = false;
                $scope.hideval = true;
                $scope.showdiv = true;
            }
            else {
                $scope.showval = false;
                $scope.hideval = false;
                $scope.showdiv = false;
            }
        }

        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('deferral_gid', $scope.deferral_gid);
            frm.append('loan_gid', $scope.loan_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            var url = 'api/deferral/UploadcadDocument';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.filename_list;

                $("#addupload").val('');
                $("#editupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')
                    var params = {
                        deferral_gid: $scope.deferral_gid
                    }
                    var url = 'api/deferral/Getcaddoc';

                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.filename_list = resp.data.filename_list;
                        }

                    });
                    //$scope.deferral_gid = "";
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')
                    
                }
                //activate();

            });

        }
        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            ////.log(str);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);

        }

        $scope.popupView = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.viewloan2deferralDetails');

        }
        $scope.deferralback = function (val) {
            $state.go('app.loanManagement');
        }

        $scope.isShowHideAdd=function(val){
            if(val=='Deferral'){
               $scope. showDeferralAdd=true;
               $scope. showCovenantAdd=false;
            }
            else{
               $scope. showDeferralAdd=false;
               $scope. showCovenantAdd=true;
            }
        }
        $scope.isShowHideEdit=function(val){
            if(val=='Deferral'){
               $scope. showDeferralEdit=true;
               $scope. showCovenantEdit=false;
            }
            else{
               $scope. showDeferralEdit=false;
               $scope. showCovenantEdit=true;
            }
        }

        $scope.deferralSubmit = function (val) {
            var deferral_name;
            var deferraltype_gid;
            var covenanttype_name;
            var covenanttype_gid;

            if ($scope.trackingtype == 'Deferral') {
                //console.log($scope.deferralname);
                if ($scope.deferralname == undefined || $scope.deferralname=="") {
                    Notify.alert('Kindly select the deferral','warning');
                    return;
                }
             deferral_name= $('#deferralname :selected').text();
             deferraltype_gid=$scope.deferralname.deferral_gid
             covenanttype_name = '';
             covenanttype_gid='';
            }
            else if ($scope.trackingtype == 'Covenant') {
                //console.log($scope.covenanttype);
                if ($scope.covenanttype == undefined || $scope.covenanttype == "") {
                    Notify.alert('Kindly select the covenant','warning');
                    return;
                }
                  covenanttype_name = $('#covenanttype_name :selected').text();
                covenanttype_gid = $scope.covenanttype.covenanttype_gid
                deferral_name='';
                deferraltype_gid='';
                
            }
            
           
            var params = {
                entity_gid: $scope.entity_gid,
                branch_gid: $scope.branch_gid,
                entity_name: $scope.entityName,
                branch_name: $scope.branchName,
                loan_gid: $scope.loan_gid,
                record_id: $scope.record_id,
                tracking_type: $scope.trackingtype,
                criticallity: $scope.criticallity,
                vertical_code: $scope.vertical_code,
                customer_gid: $scope.customer_gid,
                covenanttype_gid: covenanttype_gid,
                deferral_name: deferral_name,
                covenanttype_name: covenanttype_name,
                deferraltype_gid: deferraltype_gid,
                deferral_category: $scope.deferralcategory,
                due_date: $scope.due_date,
                remarks: $scope.remarks,
                customerremarks: $scope.customerremarks,
                zonal_name: $scope.zonal_name,
                businesshead_name: $scope.businesshead_name,
                relationshipmgmt_name: $scope.rm_name,
                cluster_manager_name: $scope.cluster_manager_name,
                creditmgmt_name: $scope.creditmgmt_name,
                zonalGid: $scope.zonalHead,
                businessHeadGid: $scope.businessHead,
                relationshipMgmtGid: $scope.relationshipMgmt,
                clustermanagerGid: $scope.clustermanager,
                creditmanager_gid: $scope.creditmanager,
                customer_name: $scope.customerName
               
            }
            //console.log(params);
            if ($scope.trackingtype == "Deferral") {
                    lockUI();
                    //$("#load").show();
                    var url = 'api/deferral/loan2Deferral';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            //$("#load").hide();
                            unlockUI();
                            Notify.alert('Deferral Created Successfully..!!', 'success')
                            $scope.entity = "";
                            $scope.branch = "";
                            $scope.trackingtype = "";
                            $scope.deferralname.deferral_gid = "";
                            $scope.deferralname = "";
                            //$scope.covenanttype.covenanttype_gid = "";
                            $scope.deferralcategory = "";
                            $scope.due_date = "";
                            $scope.critical = false;
                            $scope.showval = false;
                            $scope.hideval = false;
                            document.getElementById("deferralforms").reset();
                            //activate();
                        }
                        else {
                            //$("#load").hide();
                            unlockUI();
                            Notify.alert('Error Occurred While Creating Deferral!')
                            document.getElementById("deferralforms").reset();
                        }
                        activate();
                    });
                
            }
            else if ($scope.trackingtype == "Covenant") {
                    lockUI();
                    //$("#load").show();
                    var url = 'api/deferral/loan2Deferral';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            //$("#load").hide();
                            Notify.alert('Deferral Created Successfully..!!', 'success')
                            $scope.entity = "";
                            $scope.branch = "";
                            $scope.trackingtype = "";
                            //$scope.deferralname.deferral_gid = "";
                            $scope.covenanttype.covenanttype_gid = "";
                            $scope.covenanttype = "";
                            $scope.deferralcategory = "";
                            $scope.due_date = "";
                            $scope.critical = false;
                            $scope.showval = false;
                            $scope.hideval = false;
                            document.getElementById("deferralforms").reset();
                            //activate();
                        }
                        else {
                            unlockUI();
                            //$("#load").hide();
                            Notify.alert('Error Occurred While Creating Deferral!', 'warning')
                            document.getElementById("deferralforms").reset();
                        }
                        activate();

                    });
                
            }

        }

        $scope.popupEdit = function (val) {
            $scope.deferral_gid = val;
            $scope.activeAdd = false;
            $scope.DivEdittab = true;
            
            var params = {
                deferral_gid: localStorage.getItem('deferral_gid')
            }
            var url = 'api/deferral/checkerlist';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checker_list = resp.data.checker_list;
            });

            $scope.entity_list_edit= $scope.entity_list;
            $scope.branch_list_edit = $scope.branch_list;
            $scope.deferral_list_edit = $scope.deferral_list;
            $scope.covenanttype_list_edit = $scope.covenanttype_list;
            var param = {
                deferral_gid: val
            };
            var url = 'api/deferral/Getdeferraldetails';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.entityEdit = resp.data.entity_gid;
                $scope.branchEdit = resp.data.branch_gid;
                $scope.deferralcategoryEdit = resp.data.deferral_category;
                $scope.trackingtypeEdit = resp.data.tracking_type;
                $scope.deferralnameEdit = resp.data.deferraltype_gid;
                $scope.covenanttypeEdit = resp.data.covenanttype_gid;
                $scope.due_dateEdit = resp.data.due_date;
                $scope.due_dateEdit = Date.parse($scope.due_dateEdit);
                $scope.criticallity = resp.data.criticallity;
                $scope.remarksEdit = resp.data.remarks;
                $scope.customerremarksEdit = resp.data.customerremarks;
                $scope.checker_status = resp.data.checker_status;
                $scope.checker_remarks = resp.data.checker_remarks;
                if (resp.data.checker_status == "PushBack") {
                    $scope.showvalchecker_pushback = true;
                }
                else {
                    $scope.showvalchecker_pushback = false;
                }
                if($scope.trackingtypeEdit=='Deferral'){
                    $scope. showDeferralEdit=true;
                    $scope. showCovenantEdit=false;
                 }
                 else{
                    $scope. showDeferralEdit=false;
                    $scope. showCovenantEdit=true;
                 }
                
                 var url = 'api/deferral/Getcaddoc';
                SocketService.getparams(url, param).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.filename_list = resp.data.filename_list;
                    }
                });
            });
        };
        $scope.loan2deferralUpdateEdit = function () {
            //var date = new Date($scope.due_dateEdit);
            //date = date.getFullYear() + '-' + date.getMonth() + '-' + date.getDate() + ' ' + date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();
           
            var deferraltype_name_edit;
            var covenanttype_name_edit;

            //var entity_index = $scope.entity_list.map(function (e) { return e.entity_gid }).indexOf($scope.entityEdit);
            //if (entity_index == -1) { entity_name_edit = ''; } else { entity_name_edit = $scope.entity_list[entity_index].entity_name; };
            //var branch_index = $scope.branch_list.map(function (e) { return e.branch_gid }).indexOf($scope.branchEdit);
            //if (branch_index == -1) { branch_name_edit = ''; } else { branch_name_edit = $scope.branch_list[branch_index].branch_name; };
            var deferraltype_index = $scope.deferral_list.map(function (e) { return e.deferraltype_gid }).indexOf($scope.deferralnameEdit);
            if (deferraltype_index == -1) { deferraltype_name_edit = '' } else { deferraltype_name_edit = $scope.deferral_list[deferraltype_index].deferral_name; };
            var covenanttype_index = $scope.covenanttype_list.map(function (e) { return e.covenanttype_gid }).indexOf($scope.covenanttypeEdit);
            if (covenanttype_index == -1) { covenanttype_name_edit = '' } else { covenanttype_name_edit = $scope.covenanttype_list[covenanttype_index].covenanttype_name; };
            if ($scope.trackingtypeEdit == 'Covenant') { $scope.deferralname = ''; deferraltype_name_edit = ''; } else { $scope.covenanttypeEdit = ''; covenanttype_name_edit = ''; };

            var params = {
                deferral_gid: $scope.deferral_gid,
                entity_gid: $scope.entity_gid,
                entity_name: $scope.entityName,
                branch_gid: $scope.branch_gid,
                branch_name: $scope.branchName,
                customer_gid: $scope.customer_gid,
                customer_name: $scope.customerName,
                loan_gid: $scope.loan_gid,
                zonalhead_gid: $scope.zonalHead,
                zonalhead_name: $scope.zonal_name,
                businesshead_gid: $scope.businessHead,
                businesshead_name: $scope.businesshead_name,
                clustermgr_gid: $scope.clustermanager,
                clusterhead_name: $scope.cluster_manager_name,
                relationmgr_gid: $scope.relationshipMgmt,
                relationmgr_name: $scope.rm_name,

                creditmgr_gid: $scope.creditmanager,
                creditmgr_name: $scope.creditmgmt_name,
                category_gid: $scope.deferralcategoryEdit,
                tracking_type: $scope.trackingtypeEdit,
                deferraltype_gid: $scope.deferralnameEdit,
                deferraltype_name: deferraltype_name_edit,
                covenanttype_gid: $scope.covenanttypeEdit,
                covenanttype_name: covenanttype_name_edit,
                duedate: $scope.due_dateEdit,
                criticallity: $scope.criticallity,
                vertical_gid: $scope.vertical_gid,
                vertical_code: $scope.vertical_code,
                remarks: $scope.remarksEdit,
                customerremarks: $scope.customerremarksEdit,
                checker_status: $scope.checker_status
            }
            //console.log(params);
            var url = 'api/deferral/update' ;
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    activate();
                    document.getElementById("critical").style.visibility = "hidden";
                    $scope.activeAdd = true;
                    $scope.DivEdittab = false;
                    $scope.deferral_gid = "";
                    Notify.alert('Deferral Updated Successfully..!!', 'success')
                }

                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Updating Deferral !')
                }
            });
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('loanmanagement', loanmanagement);

    loanmanagement.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function loanmanagement($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title ='loanmanagement';

        activate();

        function activate() {

           $scope.totalDisplayed=100;
            var url = 'api/loan/loanSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.loan_data = resp.data.loanDetails;
                // new code start   
                if ($scope.loan_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.loan_data.length;
                                        if ($scope.loan_data.length < 100) {
                                            $scope.totalDisplayed = $scope.loan_data.length;
                                        }
                                    }
                    // new code end
               
                // if($scope.loan_data==null){
                //     $scope.total=0;
                // }
                // else{
                //     $scope.total=$scope.loan_data.length;
                // }

            });

            // document.getElementById('pagecount').onkeyup = function () {
           
            //     if($scope.pagecount==null){
            //      var el = document.getElementById('loadmore');
            //      el.style.backgroundColor = '#DCDCDC';  
            //     }
            //     else{
            //      var el = document.getElementById('loadmore');
            //      el.style.backgroundColor = '#ffa';
            //     }
            // };

            
  $scope.loadMore= function (pagecount) {
    if(pagecount==undefined){
        Notify.alert("Enter the Total Summary Count","warning");
        return;
    }
    lockUI();

    var Number = parseInt(pagecount);
    // new code start
        if ($scope.loan_data != null) {
       
                if (pagecount < $scope.loan_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.loan_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.loan_data.length;
                        Notify.alert(" Total Summary " + $scope.loan_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.loan_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
        
    // $scope.totalDisplayed += Number;
    // console.log(pagecount);
    unlockUI();
};

        var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
          }
        $scope.popuploan = function () {
            $state.go('app.createLoan');
        }
   
       $scope.edit = function (val) {
            $scope.loan_gid = val;
            $scope.loan_gid = localStorage.setItem('loan_gid', val);
            $state.go('app.editLoan');
       }

       $scope.PopupNewLoanRef = function (loan_gid,loanref_no, customername) {
           var modalInstance = $modal.open({
               templateUrl: '/updateLoanref.html',
               controller: ModalInstanceCtrl,
               size: 'md'
           });
           ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
           function ModalInstanceCtrl($scope, $modalInstance) {

               $scope.customername = customername;
               $scope.loanref_no = loanref_no;
              
               $scope.ok = function () {
                   $modalInstance.close('closed');
               };

               $scope.LoanRefUpdate = function () {

                   var params = {
                       loan_gid: loan_gid,
                       newloanref_no: $scope.txtnewloanrefno,
                       oldloanref_no: loanref_no
                   }

                   lockUI();
                   var url = "api/loan/LoanRefUpdate";
                   SocketService.post(url, params).then(function (resp) {
                       if (resp.data.status == true) {

                          
                           unlockUI();
                           $modalInstance.close('closed');
                           activate();
                           Notify.alert(resp.data.message, {
                               status: 'success',
                               pos: 'top-center',
                               timeout: 3000
                           });

                       }
                       else {
                           $modalInstance.close('closed');
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
       }

    $scope.createDeferral = function (val) {
                $scope.loan_gid = val;
                $scope.loan_gid= localStorage.setItem('loan_gid', val);
                $state.go('app.loan2deferral');
            }
            // View Issue 

           }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('loancontroller', loancontroller);

    loancontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function loancontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'loancontroller';
        //console.log($scope.segment_name);
        activate();


        function activate() {
            $scope.totalDisplayed=100;
            lockUI();
            var url = 'api/loan/getLoanmasterSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.vertical = resp.data.loanDetails;
                 // new code start   
                 if ($scope.vertical== null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.vertical.length;
                                        if ($scope.vertical.length < 100) {
                                            $scope.totalDisplayed = $scope.vertical.length;
                                        }
                                    }
                    // new code endd
                //$scope.total=$scope.vertical.length;
                //console.log(resp.data.loanDetails);
                
            });
        }
        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
        if ($scope.vertical!= null) {
       
                if (pagecount < $scope.vertical.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.vertical.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.vertical.length;
                        Notify.alert(" Total Summary " + $scope.vertical.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.vertical.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };

        $scope.delete = function (loanmaster_gid) {
            var params = {
                loanmaster_gid: loanmaster_gid
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
                    var url = 'api/loan/deleteloanmaster';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Loan!', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };

        $scope.popuploan = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.segmentSubmit = function () {
                    var params = {
                      
                        remarks: $scope.remarks
                    }
                    var url = 'api/loan/loanCreate';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Loan Added Successfully..!!', 'success')
                            activate();

                        }
                        else {
                            Notify.alert('Error Occurred While Adding Loan!', 'warning')
                            activate();
                        }
                    });
                    $state.go('app.loanMaster');
                }
            }
        }

        $scope.edit = function (loanmaster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    loanmaster_gid: loanmaster_gid
                }
                var url = 'api/loan/editloanmaster';
                SocketService.getparams(url, params).then(function (resp) {

              
                    $scope.descriptionedit = resp.data.loanTitleedit;
                    $scope.loanmaster_gid = resp.data.loanmaster_gid;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.segmentUpdate = function (loanmaster_gid) {

                    var params = {
                       
                        loanTitleedit: $scope.descriptionedit,
                        loanmaster_gid: $scope.loanmaster_gid
                    }
                    var url = 'api/loan/updateloanmaster';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            $modalInstance.close('closed');
                            Notify.alert('Loan Updated Successfully..!!', 'success')

                        }
                        else {
                            Notify.alert('Error Occurred While Updating Loan !', 'success')
                            activate();

                        }
                    });
                }
            }

        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('mailHistoryViewcontroller', mailHistoryViewcontroller);

    mailHistoryViewcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function mailHistoryViewcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'mailHistoryViewcontroller';
        activate();
        function activate() {



            $scope.customermail_gid = localStorage.getItem('customermail_gid');
            var params = {
                customermail_gid: $scope.customermail_gid
            };

            var url = 'api/customerAlertGenerate/mailHistoryView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.objCutomerAlert = resp.data;
                $scope.content = resp.data.content;
                document.getElementById('test').innerHTML += $scope.content;
                $scope.mail_data = resp.data.mailhistorydeferral_list;
                unlockUI();
            });



        }

       

        $scope.back = function () {

            $state.go('app.customerAlertHistory');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('mailManagementcontroller', mailManagementcontroller);

    mailManagementcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function mailManagementcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'mailManagementcontroller';

        activate();


        function activate() {

            $scope.totalDisplayed=100;
            var url = 'api/customerAlertGenerate/mailManagement';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.customer_data = resp.data.customermail_list;
              // new code start   
              if ($scope.customer_data == null) {
                                    $scope.total = 0;
                                    $scope.totalDisplayed = 0;
                                }
                                else {
                                    $scope.total = $scope.customer_data.length;
                                    if ($scope.customer_data.length < 100) {
                                        $scope.totalDisplayed = $scope.customer_data.length;
                                    }
                                }
                // new code end
                //$scope.total=$scope.customer_data.length;
            });


        }
        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
             // new code start
        if ($scope.customer_data != null) {
       
                if (pagecount < $scope.customer_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.customer_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.customer_data.length;
                        Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };
        $scope.send = function (val) {
            $scope.customeralert_gid = val;
            $scope.customeralert_gid = localStorage.setItem('customeralert_gid', val);
            $state.go('app.sendMailalert');
        }
        $scope.mailHistory = function (val) {
            $scope.customer_gid = val;
            $scope.customer_gid = localStorage.setItem('customer_gid', val);
            $scope.pageNavigation = localStorage.setItem('mailManagement', 'mailManagement');
            $state.go('app.customerAlertHistory');

        }


    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('manageDeferral', manageDeferral);

    manageDeferral.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function manageDeferral($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'manageDeferral';
       
        activate();
        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/deferral/manageDeferralSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.deferral_data = resp.data.managedeferralSummaryDtls;
                // new code start  
                unlockUI(); 
                if ($scope.deferral_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.deferral_data.length;
                                        if ($scope.deferral_data.length < 100) {
                                            $scope.totalDisplayed = $scope.deferral_data.length;
                                        }
                                    }
                    // new code end
                // $scope.total=$scope.deferral_data.length;
            });
         
         }
        //  document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
        if ($scope.deferral_data != null) {
       
                if (pagecount < $scope.deferral_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.deferral_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.deferral_data.length;
                        Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };

        $scope.edit = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.manageDeferraledit');
        }

        $scope.popupdeferral = function () {
            $state.go('app.manageDeferraladd');
        }

       
        $scope.popupView = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.manageDeferralview');

        }

         }
        })();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('manageDeferraladd', manageDeferraladd);

    manageDeferraladd.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function manageDeferraladd($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'manageDeferraladd';
        activate();
        var customer_remarks;


        function activate() {

            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats=['dd-MM-yyyy'];
            vm.format=vm.formats[0];

            //document.getElementById('load').style.visibility = "hidden";
            $('#loandata').multiselect({ includeSelectAllOption: true });
            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });

            var url = 'api/entity/entity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
            });

            var url = 'api/branch/branch';
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branch_list;
            });

            var url = 'api/deferral/deferral';
            SocketService.get(url).then(function (resp) {
                $scope.deferral_list = resp.data.deferral_list;
            });
            var url = 'api/covenanttype/getcovenanttype';
            SocketService.get(url).then(function (resp) {
                $scope.covenanttype_list = resp.data.covenanttype_list;
            });
            //var url = 'api/customer/customer';
            //SocketService.get(url).then(function (resp) {
            //    $scope.customer_list = resp.data.customer_list;
            //});
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var params = {
                deferral_gid: ''
            }
            var url = 'api/deferral/Getcaddoc';

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.filename_list = resp.data.filename_list;
                }

            });


            $scope.onselectedchangecustomer = function (customer) {
                var loandata = '';
                var params = {
                    customer_gid: $scope.customer_gid
                };
                var url = 'api/deferral/customer2loan';

                SocketService.getparams(url, params).then(function (resp) {
                   
                    $scope.loan = resp.data.loan;
                });

                $scope.showcustomer = true;
                $scope.showdetails = true;
               
                var url = 'api/loan/customer_getheads';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.mdlheadsofcustomer = resp.data;
                    $scope.zonalHead = resp.data.zonalGid;
                    $scope.businessHead = resp.data.businessHeadGid;
                    $scope.clustermanager = resp.data.clustermanagerGid;
                    $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                    $scope.creditmgmt_gid = resp.data.creditmanager_gid;
                    $scope.vertical_code = resp.data.vertical_code;
                    $scope.vertical = true;
                });

            }




            $scope.onselectedchangedeferral = function (val) {
                if($scope.deferralname==undefined){
                    $scope.MDLcriticallity = '';
                    $scope.criticallity = '';
                    $scope.remarks ='';
                    $scope.customerremarks='';
                    $scope.critical = false;
              
                }
                else{
                    $scope.MDLcriticallity = $scope.deferralname;
                    $scope.criticallity = $scope.deferralname.criticallity;
                    $scope.remarks =$scope.deferralname.comments;
                    $scope.customerremarks=$scope.deferralname.comments;
                    $scope.critical = true
              
                }
              
        }
        $scope.onselectedchangecovenant = function (covenanttype) {
            if($scope.covenanttype==undefined){
                $scope.MDLcriticallity = '';
                $scope.criticallity = '';
                $scope.remarks ='';
                $scope.customerremarks='';
                $scope.critical = false
          
            }
            else{
                $scope.MDLcriticallity = $scope.covenanttype;
                $scope.criticallity = $scope.covenanttype.criticallity;
                $scope.remarks = $scope.covenanttype.comments;
                $scope.customerremarks=$scope.covenanttype.comments;
                $scope.critical = true
          
            }
              
        }


        }


        $scope.complete = function (string) {

            if (string.length >= 3) {
                $scope.message = "";
                var url = 'api/customer/ExploreCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        $scope.message = "No Records";
                    }


                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "Type atleast three character";
            }
        }

        $scope.fillTextbox = function (customer_gid, customer_name) {
            //console.log('string', customer_name, customer_gid);
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;




            var params = {
                customer_gid: customer_gid
            }

            var loandata = '';

            var url = 'api/deferral/customer2loan';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.loan = resp.data.loan;

            });

            $scope.showcustomer = true;
            $scope.showdetails = true;

            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.mdlheadsofcustomer = resp.data;
                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmgmt_gid = resp.data.creditmanager_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.vertical = true;
            });
        }

        $scope.btncopy = function () {
            customer_remarks = $scope.remarks;
            $scope.customerremarks = customer_remarks;
        }

        $scope.isShowHide = function (param) {
            if (param == "show") {
                $scope.showval = true;
                $scope.hideval = false;
                $scope.showdiv = true;
            }
            else if (param == "hide") {
                $scope.showval = false;
                $scope.hideval = true;
                $scope.showdiv = true;
            }
            else {
                $scope.showval = false;
                $scope.hideval = false;
                $scope.showdiv = true;
            }
        }

        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('deferral_gid', $scope.deferral_gid);
            frm.append('loan_gid', $scope.loan_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            var url = 'api/deferral/UploadcadDocument';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.filename_list;

                $("#addupload").val('');
                $("#editupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')
                    var params = {
                        deferral_gid: $scope.deferral_gid
                    }
                    var url = 'api/deferral/Getcaddoc';

                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.filename_list = resp.data.filename_list;
                        }

                    });
                    //$scope.deferral_gid = "";
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')

                }
                //activate();

            });

        }
        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            ////console.log(str);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
       
        $scope.deferralback = function (val) {
            $state.go('app.manageDeferral');
        }

        $scope.deferralSubmit = function () {
            var deferral_name;
            var deferraltype_gid;
            var covenanttype_name;
            var covenanttype_gid;
            //var customer_name = $('#customer_name :selected').text();
            var deferral_name = $('#deferral_name :selected').text();
            var covenanttype_name = $('#covenanttype_name :selected').text();
            var zonal_name = $('#zonal_name :selected').text();
            var businesshead_name = $('#businesshead_name :selected').text();
            var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
            var cluster_manager_name = $('#cluster_manager_name :selected').text();
            var creditmgmt_name = $('#creditmgmt_name :selected').text();
            var entityname = $('#entity_name :selected').text();
            var branchname = $('#branch_name :selected').text();

            if($scope.trackingtype=='Deferral'){
                if($scope.deferralname==undefined){
                    Notify.alert('Kindly select the deferral','warning');
                    return;
                }
             deferral_name= $('#deferralname :selected').text();
             deferraltype_gid=$scope.deferralname.deferral_gid
             covenanttype_name = '';
             covenanttype_gid='';
            }
            else if($scope.trackingtype=='Covenant'){
                if($scope.covenanttype==undefined){
                    Notify.alert('Kindly select the covenant','warning');
                    return;
                }
                deferral_name='';
                deferraltype_gid='';
                 covenanttype_name = $('#covenanttype_name :selected').text();
                 covenanttype_gid=$scope.covenanttype.covenanttype_gid
            }
            var params = {
                entity_gid: $scope.entity.entity_gid,
                branch_gid: $scope.branch.branch_gid,
                entity_name: entityname,
                branch_name: branchname,
                customer_gid: $scope.customer_gid,
                covenanttype_gid: covenanttype_gid,
                criticallity: $scope.criticallity,
                deferraltype_gid: deferraltype_gid,
                loans: $scope.$parent.loan,
                record_id: $scope.record_id,
                tracking_type: $scope.trackingtype,
                vertical_code: $scope.vertical_code,
                deferral_name: deferral_name,
                covenanttype_name: covenanttype_name,
                deferral_gid: deferraltype_gid,
                deferral_category: $scope.deferralcategory,
                due_date: $scope.due_date,
                sanction_refno: $scope.sanctionrefno,
                sanction_date: $scope.sanctiondate,
                remarks: $scope.remarks,
                customerremarks: $scope.customerremarks,
                customer_name: $scope.customer,
                zonal_name: zonal_name,
                businesshead_name: businesshead_name,
                relationshipmgmt_name: relationshipmgmt_name,
                cluster_manager_name: cluster_manager_name,
                creditmgmt_name: creditmgmt_name,
                zonalGid: $scope.zonalHead,
                businessHeadGid: $scope.businessHead,
                relationshipMgmtGid: $scope.relationshipMgmt,
                clustermanagerGid: $scope.clustermanager,
                creditmanager_gid: $scope.creditmgmt_gid,
            }
            //console.log(params);
            lockUI();
            var url = 'api/deferral/createDeferral';

            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    //$("#load").hide();
                  

                    Notify.alert('Deferral Created Successfully..!!', 'success')
                    $state.go('app.manageDeferral');
                }
                else {
                    unlockUI();
                    //$("#load").hide();
                    Notify.alert('Select Atleast One Loan')
                    $state.go('app.manageDeferraladd');
                }

            });
       
          

        }

        $scope.deferralSave = function () {
            var deferral_name;
            var deferraltype_gid;
            var covenanttype_name;
            var covenanttype_gid;

            var loandata = $('#loandata :selected').text();
            //var customer_name = $('#customer_name :selected').text();
            var zonal_name = $('#zonal_name :selected').text();
            var businesshead_name = $('#businesshead_name :selected').text();
            var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
            var cluster_manager_name = $('#cluster_manager_name :selected').text();
            var creditmgmt_name = $('#creditmgmt_name :selected').text();
            var entityname = $('#entity_name :selected').text();
            var branchname = $('#branch_name :selected').text();
            if($scope.trackingtype=='Deferral'){
                if($scope.deferralname==undefined){
                    Notify.alert('Kindly select the deferral','warning');
                    return;
                }
             deferral_name= $('#deferralname :selected').text();
             deferraltype_gid=$scope.deferralname.deferral_gid
             covenanttype_name = '';
             covenanttype_gid='';
            }
            else if($scope.trackingtype=='Covenant'){
                if($scope.covenanttype==undefined){
                    Notify.alert('Kindly select the covenant','warning');
                    return;
                }
                deferral_name='';
                deferraltype_gid='';
                 covenanttype_name = $('#covenanttype_name :selected').text();
                 covenanttype_gid=$scope.covenanttype.covenanttype_gid
            }

            var params = {
                entity_gid: $scope.entity.entity_gid,
                branch_gid: $scope.branch.branch_gid,
                entity_name: entityname,
                branch_name: branchname,
                customer_gid: $scope.customer_gid,
                covenanttype_gid:covenanttype_gid,
                criticallity: $scope.criticallity,
                deferraltype_gid: deferraltype_gid,
                loans: $scope.$parent.loan,
                record_id: $scope.record_id,
                tracking_type: $scope.trackingtype,
                vertical_code: $scope.vertical_code,
                deferral_name: deferral_name,
                covenanttype_name: covenanttype_name,
                deferral_gid: deferraltype_gid,
                deferral_category: $scope.deferralcategory,
                due_date: $scope.due_date,
                sanction_refno: $scope.sanctionrefno,
                sanction_date: $scope.sanctiondate,
                remarks: $scope.remarks,
                customerremarks: $scope.customerremarks,
                customer_name: $scope.customer,
                zonal_name: zonal_name,
                businesshead_name: businesshead_name,
                relationshipmgmt_name: relationshipmgmt_name,
                cluster_manager_name: cluster_manager_name,
                creditmgmt_name: creditmgmt_name,
                zonalGid: $scope.zonalHead,
                businessHeadGid: $scope.businessHead,
                relationshipMgmtGid: $scope.relationshipMgmt,
                clustermanagerGid: $scope.clustermanager,
                creditmanager_gid: $scope.creditmgmt_gid,
                //loandata: loandata
            }
            //console.log(params);
            lockUI();
            var url = 'api/deferral/createDeferral';

            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                

                    Notify.alert('Deferral Created Successfully..!!', 'success')
                    $scope.deferralcategory = "";
                    $scope.due_date = "";
                    $scope.deferralname = "";
                    $scope.covenanttype = "";
                    $scope.trackingtype = "";
                    $scope.selectedData = "";
                    $scope.remarks = "";
                    $scope.loan = [];
                    var params = {
                        customer_gid: $scope.customer_gid
                    };
                    var url = 'api/deferral/customer2loan';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.loan = resp.data.loan;

                    });
                    $scope.critical = false;
                    $scope.showval = false;
                    $scope.hideval = false;
                    document.getElementById("trackingtype").checked = false;
                    var params = {
                        deferral_gid: ''
                    }
                    var url = 'api/deferral/Getcaddoc';

                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.filename_list = resp.data.filename_list;
                        }

                    });
                }
                else {
                    unlockUI();

                    Notify.alert('Select Atleast One Loan')
                    $state.go('app.createDeferral');
                }

            });
        
                   }

    }
})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('manageDeferraledit', manageDeferraledit);

    manageDeferraledit.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function manageDeferraledit($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'manageDeferraledit';
        activate();
        var customer_remarks;
        function activate() {
            $('#loandata').multiselect({ includeSelectAllOption: true });


            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats=['dd-MM-yyyy'];
            vm.format=vm.formats[0];

            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });

            var url = 'api/entity/entity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
            });

            var url = 'api/branch/branch';
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branch_list;
            });

            var url = 'api/deferral/deferral';
            SocketService.get(url).then(function (resp) {
                $scope.deferral_list = resp.data.deferral_list;
            });
            var url = 'api/covenanttype/getcovenanttype';
            SocketService.get(url).then(function (resp) {
                $scope.covenanttype_list = resp.data.covenanttype_list;
            });
            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.clusterlist = resp.data.employee_list;
                $scope.businesshead_list = resp.data.employee_list;
                $scope.relationshiplist = resp.data.employee_list;
                $scope.zonallist = resp.data.employee_list;
                $scope.creditlist = resp.data.employee_list;
            });

            var params = {
                deferral_gid: localStorage.getItem('deferral_gid')
            }
            var url = 'api/deferral/checkerlist';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checker_list = resp.data.checker_list;
            });

            var param = {
                deferral_gid: localStorage.getItem('deferral_gid')
            };

            var url = 'api/deferral/Getdeferraldetails';
            SocketService.getparams(url, param).then(function (resp) {
                //console.log(resp.data)
                $scope.Getdeferral = resp.data;
                $scope.branchEdit = resp.data.branch_gid;
                $scope.entityEdit = resp.data.entity_gid;
                $scope.customer = resp.data.customer_gid;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.cluster_manager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmgmt_name = resp.data.creditmanager_gid;
                $scope.tracking_type = resp.data.tracking_type;
                $scope.loanRefNoedit = resp.data.loanGID;
                $scope.due_dateedit = resp.data.due_date;
                $scope.due_dateedit = Date.parse($scope.due_dateedit);
                if (resp.data.tracking_type == "Covenant") {
                    $scope.showval = true;
                    $scope.hideval = false;
                }
                else {
                    $scope.showval = false;
                    $scope.hideval = true;
                }
                $scope.deferralname = resp.data.deferraltype_gid;
                $scope.covenanttype = resp.data.covenanttype_gid;
                $scope.deferralcategoryedit = resp.data.deferral_category;
                $scope.vertical = resp.data.vertical_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.criticallity = resp.data.criticallity;
                $scope.remarks = resp.data.remarks;
                $scope.customerremarks = resp.data.customerremarks;
                $scope.checker_status = resp.data.checker_status;
                $scope.checker_remarks = resp.data.checker_remarks;
                if (resp.data.checker_status == "PushBack") {
                    $scope.showvalchecker_pushback = true;
                }
                else {
                    $scope.showvalchecker_pushback = false;
                }

            });


            $scope.onselectedchangedeferral = function (val) {
                var params = {
                    deferral: val
                };
                var url = 'api/loan/getdeferralcriticallity';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.MDLcriticallity = resp.data;
                    $scope.criticallity = resp.data.criticallity;
                    $scope.remarks = resp.data.comments;
                    $scope.customerremarks=resp.data.comments;
                    $scope.critical = true
                });
            }

            $scope.onselectedchangecovenant = function (covenanttype) {
                var params = {
                    covenanttype: covenanttype
                };
                var url = 'api/loan/getcovenanttypecriticallity';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.MDLcriticallity = resp.data;
                    $scope.criticallity = resp.data.criticallity;
                    $scope.remarks = resp.data.comments;
                    $scope.customerremarks=resp.data.comments;
                    $scope.critical = true
                });
            }

            $scope.onselectedchangecustomer = function (customer) {
                var params = {
                    customer_gid: customer
                }
                var url = 'api/loan/customer_getheads';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.vertical_code = resp.data.vertical_code;
                    $scope.zonalHead = resp.data.zonalGid;
                    $scope.businessHead = resp.data.businessHeadGid;
                    $scope.clustermanager = resp.data.clustermanagerGid;
                    $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                    $scope.creditmgmt_name = resp.data.creditmanager_gid;
                });
            }
            var url = 'api/deferral/Getcaddoc';
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.filename_list = resp.data.filename_list;
                }
            });
        }
        $scope.btncopy = function () {
            customer_remarks = $scope.remarks;
            $scope.customerremarks = customer_remarks;
        }
        $scope.isShowHide = function (param) {
            if (param == "show") {
                $scope.showval = true;
                $scope.hideval = false;
            }
            else if (param == "hide") {
                $scope.showval = false;
                $scope.hideval = true;
            }
            else {
                $scope.showval = false;
                $scope.hideval = false;
            }
        }
        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('deferral_gid', $scope.deferral_gid);
            frm.append('loan_gid', $scope.loan_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            document.getElementById('load').style.visibility = "visible";
            var url = 'api/deferral/UploadcadDocument';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                unlockUI();
                $scope.filename_list = resp.data.filename_list;

                $("#addupload").val('');

                if (resp.data.status == true) {
                    activate();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')
                }
                activate();
            });
        }
        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            ////console.log(str);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.deferralback = function () {
            $state.go('app.manageDeferral');
        }
        $scope.deferralUpdateEdit = function () {
            //var date = new Date($scope.due_dateedit);
            //date = date.getFullYear() + '-' + date.getMonth() + '-' + date.getDate() + ' ' + date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();

            var entity_name_edit;
            var branch_name_edit;
            var deferraltype_name_edit;
            var covenanttype_name_edit;
            var customername_edit;
            var zonalhead_name_edit;
            var businesshead_name_edit;
            var clusterhead_name_edit;
            var relationalmgr_name_edit;
            var creditmanager_name_edit;

            var vertical_code = $('#vertical_code :selected').text();
            var entity_index = $scope.entity_list.map(function (e) { return e.entity_gid }).indexOf($scope.entityEdit);
            if (entity_index == -1) { entity_name_edit = ''; } else { entity_name_edit = $scope.entity_list[entity_index].entity_name; };
            var branch_index = $scope.branch_list.map(function (e) { return e.branch_gid }).indexOf($scope.branchEdit);
            if (branch_index == -1) { branch_name_edit = ''; } else { branch_name_edit = $scope.branch_list[branch_index].branch_name; };
            var customer_index = $scope.customer_list.map(function (e) { return e.customer_gid }).indexOf($scope.customer);
            if (customer_index == -1) { customername_edit = ''; } else { customername_edit = $scope.customer_list[customer_index].customername; };
            var zonalhead_index = $scope.zonallist.map(function (e) { return e.employee_gid }).indexOf($scope.zonalHead);
            if (zonalhead_index == -1) { zonalhead_name_edit = ''; } else { zonalhead_name_edit = $scope.zonallist[zonalhead_index].employee_name; }
            var businesshead_index = $scope.businesshead_list.map(function (e) { return e.employee_gid }).indexOf($scope.businessHead);
            if (businesshead_index == -1) { businesshead_name_edit = ''; } else { businesshead_name_edit = $scope.businesshead_list[businesshead_index].employee_name; }
            var clusterhead_index = $scope.clusterlist.map(function (e) { return e.employee_gid }).indexOf($scope.cluster_manager);
            if (clusterhead_index == -1) { clusterhead_name_edit = ''; } else { clusterhead_name_edit = $scope.clusterlist[clusterhead_index].employee_name; }
            var reletinalmgr_index = $scope.relationshiplist.map(function (e) { return e.employee_gid }).indexOf($scope.relationshipMgmt);
            if (reletinalmgr_index == -1) { relationalmgr_name_edit = ''; } else { relationalmgr_name_edit = $scope.relationshiplist[reletinalmgr_index].employee_name; }
            var creditmanager_index = $scope.creditlist.map(function (e) { return e.employee_gid }).indexOf($scope.creditmgmt_name);
            if (creditmanager_index == -1) { creditmanager_name_edit = ''; } else { creditmanager_name_edit = $scope.creditlist[creditmanager_index].employee_name; }
            var deferraltype_index = $scope.deferral_list.map(function (e) { return e.deferraltype_gid }).indexOf($scope.deferralname);
            if (deferraltype_index == -1) { deferraltype_name_edit = '' } else { deferraltype_name_edit = $scope.deferral_list[deferraltype_index].deferral_name; };
            var covenanttype_index = $scope.covenanttype_list.map(function (e) { return e.covenanttype_gid }).indexOf($scope.covenanttype);
            if (covenanttype_index == -1) { covenanttype_name_edit = '' } else { covenanttype_name_edit = $scope.covenanttype_list[covenanttype_index].covenanttype_name; };
            if ($scope.tracking_type == 'Covenant') { $scope.deferralname = ''; deferraltype_name_edit = ''; } else { $scope.covenanttype = ''; covenanttype_name_edit = ''; };

            var params = {
                deferral_gid: localStorage.getItem('deferral_gid'),
                entity_gid: $scope.entityEdit,
                entity_name: entity_name_edit,
                branch_gid: $scope.branchEdit,
                branch_name: branch_name_edit,
                customer_gid: $scope.customer,
                customer_name: customername_edit,
                loan_gid: $scope.loanRefNoedit,
                zonalhead_gid: $scope.zonalHead,
                zonalhead_name: zonalhead_name_edit,
                businesshead_gid: $scope.businessHead,
                businesshead_name: businesshead_name_edit,
                clustermgr_gid: $scope.cluster_manager,
                clusterhead_name: clusterhead_name_edit,
                relationmgr_gid: $scope.relationshipMgmt,
                relationmgr_name: relationalmgr_name_edit,
                creditmgr_gid: $scope.creditmgmt_name,
                creditmgr_name: creditmanager_name_edit,
                category_gid: $scope.deferralcategoryedit,
                tracking_type: $scope.tracking_type,
                deferraltype_gid: $scope.deferralname,
                deferraltype_name: deferraltype_name_edit,
                covenanttype_gid: $scope.covenanttype,
                covenanttype_name: covenanttype_name_edit,
                duedate: $scope.due_dateedit,
                //duedate: date,
                criticallity: $scope.criticallity,
                vertical_code: vertical_code,
                vertical_gid: $scope.vertical,
                remarks: $scope.remarks,
                customerremarks: $scope.customerremarks,
                checker_status: $scope.checker_status
            }
            //console.log(params);
            var url = 'api/deferral/update' ;
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    $state.go('app.manageDeferral');
                    Notify.alert('Deferral Updated Successfully..!!', 'success')
                }

                else {
                    Notify.alert('Error Occurred While Updating Deferral !')
                }
            });
        }
    }
})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('manageDeferralview', manageDeferralview);

    manageDeferralview.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService'];

    function manageDeferralview($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'manageDeferralview';

        activate();
        function activate() {
            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/Getcaddoc';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadDocumentname = resp.data;
                $scope.deferrals = resp.data.deferralSummaryDtls;
                $scope.deferral_gid = resp.data.deferral_gid;
                $scope.loanref_no = resp.data.loanref_no;
                $scope.loan_title = resp.data.loan_title;
                $scope.record_id = resp.data.record_id;
                $scope.deferral_name = resp.data.deferral_name;
                $scope.approval_remarks = resp.data.approval_remarks;
                $scope.approval_status = resp.data.approval_status;
                $scope.filename_list = resp.data.filename_list;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.businesshead_name = resp.data.businesshead_name;
                $scope.rm_name = resp.data.rm_name;
                $scope.customer_code = resp.data.customer_code;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.entity_name = resp.data.entity_name;
                $scope.branch_name = resp.data.branch_name;
                $scope.cluster_manager_name = resp.data.cluster_manager_name;
            });

            // Close Modals

            $scope.close = function (val) {
                document.getElementById("userform").reset();
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }

            //$scope.upload = function (val, val1, name) {
            //    var item = {
            //        name: val[0].name,
            //        file: val[0]
            //    };
            //    var frm = new FormData();
            //    frm.append('fileupload', item.file);
            //    frm.append('file_name', item.name);
            //    frm.append('document_name', $scope.documentname);
            //    frm.append('deferral_gid', $scope.deferral_gid);
            //    frm.append('loan_gid', $scope.loan_gid);
            //    frm.append('project_flag', "Default");
            //    $scope.uploadfrm = frm;
            //    var url = 'api/deferral/UploadcadDocument';
            //    lockUI();
            //    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
            //        unlockUI();
            //        $scope.filename_list = resp.data.filename_list;

            //        $("#addupload").val('');

            //        if (resp.data.status == true) {
            //            activate();
            //            Notify.alert('Document Uploaded Successfully..!!', 'success')
            //            var modalInstance = $modal.open({
            //                templateUrl: '/UploadDocument.html',
            //                controller: ModalInstanceCtrl,
            //                size: 'md'
            //            });
            //        }
            //        else {
            //            unlockUI();
            //            Notify.alert('File Format Not Supported!')
            //        }
            //        activate();
            //    });

            //}
            $scope.downloads = function (val1, val2) {
                //var phyPath = val1;
                //var relPath = phyPath.split("StoryboardAPI");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                ////console.log(str);
                //var link = document.createElement("a");
                //var name = val2.split('.');
                //link.download = val2;
                //var uri = str;
                //link.href = uri;
                //link.click();
                DownloaddocumentService.Downloaddocument(val1, val2);
            }

        }

        $scope.deferralback = function (val) {
            $state.go('app.manageDeferral');
        }



    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('penalityAlertcontroller', penalityAlertcontroller);

    penalityAlertcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'Notify', 'SocketService', '$location', '$route', '$filter', 'ngTableParams', '$modal', '$resource'];

    function penalityAlertcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, Notify, SocketService, $location, $route, $filter, ngTableParams, $modal, $resource) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'penalityAlertcontroller';

        activate();

        function activate() {

            // open dialog window
            $scope.totalDisplayed=100;
            var url = 'api/penalityAlert/penalityManagement';
            lockUI();
            SocketService.get(url).then(function (resp) {
               unlockUI();
                $scope.customer_data = resp.data.customermail_list;
                // new code start   
                if ($scope.customer_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.customer_data.length;
                                        if ($scope.customer_data.length < 100) {
                                            $scope.totalDisplayed = $scope.customer_data.length;
                                        }
                                    }
                    // new code end
                //$scope.total=$scope.customer_data.length;
            });
        }
        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
             // new code start
        if ($scope.customer_data != null) {
       
                if (pagecount < $scope.customer_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.customer_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.customer_data.length;
                        Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };
        $scope.viewpenalityalert = function (customeralert_gid) {
            localStorage.setItem('penalityalert_gid', customeralert_gid);
            $state.go('app.penalityAlertView')
        }

        $scope.startpenalityalert = function (customeralert_gid) {
            var params = {
                customeralert_gid: customeralert_gid
            };
            console.log(params);
            var modalInstance = $modal.open({
                templateUrl: '/penalityAlertMailContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = "api/penalityAlert/Getcustomerpenalitydetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customerdetails = resp.data;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.sendStartPenalityAlert = function () {
                    var url = "api/penalityAlert/startpenalityalert";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }

                    });
                }
            }
        }

        $scope.endpenalityalert = function (customeralert_gid) {
            var params = {
                customeralert_gid: customeralert_gid
            };

            var url = "api/penalityAlert/Getpenalityrecorddetails"
            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp);
                if (resp.data.status == false) {
                     
                    alert(resp.data.message);
                    return resp.data.message;
                }
                else {

                    var modalInstance = $modal.open({
                        templateUrl: '/endpenalityAlertMailContent.html',
                        controller: ModalInstanceCtrl,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {

                        $scope.customerdetails = resp.data;

                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };

                        $scope.sendendPenalityAlert = function () {
                            lockUI();
                            var url = "api/penalityAlert/endpenalityalert";
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status == true) {

                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $modalInstance.close('closed');
                                    activate();
                                    unlockUI();
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'Warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    unlockUI();
                                }
                            });
                        }
                    }
                }
            });

        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('penalityAlertViewcontroller', penalityAlertViewcontroller);

    penalityAlertViewcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'Notify', 'SocketService', '$location', '$route', '$filter', 'ngTableParams', '$resource'];

    function penalityAlertViewcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, Notify, SocketService, $location, $route, $filter, ngTableParams, $resource) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'penalityAlertViewcontroller';

        activate();

        function activate() {
            lockUI();
            var params = {
                customeralert_gid: localStorage.getItem('penalityalert_gid')
            }
            var url = "api/penalityAlert/Getcustomerpenalitydetails";
            SocketService.getparams(url, params).then(function (resp) {
               
                $scope.customerdetails = resp.data;
                $scope.deferral_data = resp.data.mailalert_list;
                $scope.penalityalert = resp.data.penalityalert_list;
                $scope.penalityhistory = resp.data.mailhistorydeferral_list;
            });

            unlockUI();
        }

        $scope.back = function () {
            $state.go('app.penalityAlert');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('registerCustomer', registerCustomer);

    registerCustomer.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function registerCustomer($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'registerCustomer';
        activate();
        function activate() {
            $scope.Warning = false;
            var url = 'api/customer/cMmail';
            SocketService.get(url).then(function (resp) {
                $scope.txtccmail = resp.data.ccmail;
            });

            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });


            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });
            var url = 'api/customer/Getconstitution';
            SocketService.get(url).then(function (resp) {
                $scope.constitution_list = resp.data.constitution_list;
                console.log(resp.data.constitution_list);
            });
            $scope.txtcountry = "India";
        }


        $scope.customerback = function (val) {
            $state.go('app.registerCustomersummary');
        }
        $scope.urnvalidation = function () {
            var params =
                {
                    urn: $scope.txtcustomerURN,
                }
            var url = 'api/MstCustomerAdd/GetURNInfo';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.Warning = true;
                }
                else {
                    $scope.Warning = false;
                }
            });
        }
        $scope.complete = function (string) {
            if (string.length >= 3) {
                var url = 'api/customer/CommonCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        var message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        if (resp.data.message == null) {
                            $scope.customer_list = null;
                            $scope.message = "";
                        }
                        else {
                            $scope.customer_list = null;
                            $scope.message = resp.data.message;
                        }
                    }
                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "";
            }
        }
        $scope.fillTextbox = function (customer_gid, customer_name) {
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;

            var url = 'api/customer/CommonCustomer';
            var params = {
                customername: customer_name
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    $scope.message = "";

                }
                else {
                    if (resp.data.message == null) {
                        $scope.customer_list = null;
                        $scope.message = "";
                    }
                    else {
                        $scope.customer_list = null;
                        $scope.message = resp.data.message;
                    }
                }
            });
        }
        $scope.customerSubmit = function () {
            if ($scope.customer == undefined || $scope.customer == null || $scope.customer == "") {
                Notify.alert("Kindly check the customer", 'warning')

            }
            else {
                if ($scope.message == "You can't add this Customer. Tag the customer from master.")
                {
                    Notify.alert("You can't add this Customer. Tag the customer from master.", 'warning')
                }
                else{
            var params =
           {
               urn: $scope.txtcustomerURN,
           }
            var url = 'api/MstCustomerAdd/GetURNInfo';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert('Already this URN is in Imported Customer', 'warning');
                }
                else {
                    var vertical_code = $('#vertical_code :selected').text();
                    var zonalHead_name = $('#zonalHead_name :selected').text();
                    var businessHead_name = $('#businessHead_name :selected').text();
                    var regionalHead_name = $('#regionalHead_name :selected').text();
                    var cluster_manager_name = $('#cluster_manager_name :selected').text();
                    var relationshipMgmt_name = $('#relationshipMgmt_name :selected').text();
                    var creditmanager_name = $('#creditmanager_name :selected').text();
                    var state_name = $('#statename :selected').text();
                    var zonlRM_name = $('#zonlRM_name :selected').text();
                    var riskmanager_name = $('#riskmanager_name :selected').text();
                    var RiskMonitoring_Name = $('#RiskMonitoring_Name :selected').text();

                    var params = {
                        vertical_gid: $scope.vertical,
                        vertical_code: vertical_code,
                        customercode: $scope.txtcustomercode,
                        //  customername: $scope.txtcustomername,
                        customername: $scope.customer,
                        contactperson: $scope.txtcontactperson,
                        contactnumber: $scope.txtcontactno,
                        mobileno: $scope.txtmobileno,
                        email: $scope.txtemail,
                        address1: $scope.txtaddress1,
                        //address2: $scope.txtaddress2,
                        region: $scope.txtregion,
                        address2: $scope.txtaddress2,
                        state_gid: $scope.state_gid,
                        state: state_name,
                        postalcode: $scope.txtpostalcode,
                        country: $scope.txtcountry,
                        tomail: $scope.txttomail,
                        ccmail: $scope.txtccmail,
                        zonalGid: $scope.zonalHead,
                        businessHeadGid: $scope.businessHead,
                        regionalHeadGid: $scope.regionalHead,
                        relationshipMgmtGid: $scope.relationshipMgmt,
                        clustermanagerGid: $scope.clustermanager,
                        creditmanagerGid: $scope.creditmanager,
                        zonal_name: zonalHead_name,
                        businesshead_name: businessHead_name,
                        regionalhead_name: regionalHead_name,
                        cluster_manager_name: cluster_manager_name,
                        relationshipmgmt_name: relationshipMgmt_name,
                        creditmanager_name: creditmanager_name,
                        customer_urn: $scope.txtcustomerURN,
                        pan_number: $scope.pan_number,
                        gst_number: $scope.gst_number,
                        constitution_name: $scope.cboconstitution.constitution_name,
                        constitution_gid: $scope.cboconstitution.constitution_gid,
                        major_corporate: $scope.txtmajor_corporate,
                        zonal_riskmanagerGID: $scope.zonalRM_GID,
                        zonal_riskmanagerName: zonlRM_name,
                        risk_managerGID: $scope.riskmanager_GID,
                        riskmanager_name: riskmanager_name,
                        riskMonitoring_GID: $scope.RiskMonitoring_GID,
                        riskMonitoring_Name: RiskMonitoring_Name
                    }
                    //console.log(params);
                    var url = 'api/customer/customerSubmit';
                    lockUI()
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            activate();
                            $state.go('app.registerCustomersummary');
                            Notify.alert('Customer Created Successfully..!!', 'success')
                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message)
                        }
                        activate();
                    });
                }
            });
                }
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('registercustomerEdit', registercustomerEdit);

    registercustomerEdit.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function registercustomerEdit($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'registercustomerEdit';




        activate();
        function activate() {
            $scope.Warning = false;
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.clusterlist = resp.data.employee_list;
                $scope.businesshead_list = resp.data.employee_list;
                $scope.regionalhead_list = resp.data.employee_list;
                $scope.relationshiplist = resp.data.employee_list;
                $scope.zonallist = resp.data.employee_list;
                $scope.creditlist = resp.data.employee_list;
                $scope.zonalrm_list = resp.data.employee_list;
                $scope.rm_list = resp.data.employee_list;
                $scope.riskmonitorning_list = resp.data.employee_list;
            });
            var url = 'api/customer/Getconstitution';
            SocketService.get(url).then(function (resp) {
                $scope.constitution_list = resp.data.constitution_list;

            });
            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });

            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            $scope.customer_gid = localStorage.getItem('customer_gid');
            var url = 'api/customer/Getcustomerupdatedetails';
            var param = {
                customer_gid: $scope.customer_gid
            };

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {

                $scope.customerCodeedit = resp.data.customerCodeedit;
                $scope.customerNameedit = resp.data.customerNameedit;
                $scope.contactPersonedit = resp.data.contactPersonedit;
                $scope.mobileNoedit = resp.data.mobileNo_edit;
                $scope.contactnoedit = resp.data.contactno_edit;
                $scope.emailedit = resp.data.emailedit;
                $scope.txtaddress1 = resp.data.addressline1edit;
                $scope.txtaddress2 = resp.data.addressline2edit;
                $scope.regionedit = resp.data.regionedit;
                $scope.countryedit = resp.data.countryedit;
                $scope.vertical = resp.data.vertical_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.statename = resp.data.state;
                $scope.state_gid = resp.data.state_gid;

                $scope.postalcodeedit = resp.data.postalcode_edit;
                $scope.tomailedit = resp.data.tomailedit;
                $scope.ccmailedit = resp.data.ccmailedit;

                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.regionalHead = resp.data.regionalHeadGid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmanager = resp.data.creditmanagerGid;
                $scope.customerURNedit = resp.data.customer_urnedit;
                $scope.pan_number = resp.data.pan_number;
                $scope.gst_number = resp.data.gst_number;
                $scope.txtmajor_corporateedit = resp.data.major_corporateedit;
                $scope.cboconstitutionedit = resp.data.constitution_gidedit;
                $scope.ZonalRM = resp.data.zonal_riskmanagerGID;
                $scope.riskmanager = resp.data.risk_managerGID;
                $scope.RiskMonitoringName = resp.data.riskMonitoring_GID;
                
                unlockUI();
              
            });
       
        }



        $scope.customereditback = function () {
            $state.go('app.registerCustomersummary');
        }

        $scope.urnvalidation = function () {
            var params =
                {
                    urn: $scope.customerURNedit,
                }
            var url = 'api/MstCustomerAdd/GetURNInfo';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.Warning = true;
                }
                else {
                    $scope.Warning = false;
                }
            });
        }
        $scope.customerUpdate = function () {
            var params =
           {
               urn: $scope.customerURNedit,
           }
            var url = 'api/MstCustomerAdd/GetURNInfo';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert('Already this URN is in Imported Customer', 'warning');
                }
                else {
                    var zonalHead_name = $('#zonalHead_name :selected').text();
                    var businessHead_name = $('#businessHead_name :selected').text();
                    var regionalHead_name = $('#regionalHead_name :selected').text();
                    var vertical_code = $('#vertical_code :selected').text();
                    var cluster_manager_name = $('#cluster_manager_name :selected').text();
                    var relationshipMgmt_name = $('#relationshipMgmt_name :selected').text();
                    var creditmanager_name = $('#creditmanager_name :selected').text();
                    var state_name = $('#statename :selected').text();
                    var constitutionname = $('#constitutionname :selected').text();
                    var zonlRM_name = $('#zonlRM_name :selected').text();
                    var riskmanager_name = $('#riskmanager_name :selected').text();
                    var RiskMonitoring_Name = $('#RiskMonitoring_Name :selected').text();
                    var params = {
                        customer_gid: $scope.customer_gid,
                        customerCodeedit: $scope.customerCodeedit,
                        customerNameedit: $scope.customerNameedit,
                        contactPersonedit: $scope.contactPersonedit,
                        mobileNoedit: $scope.mobileNoedit,
                        contactnoedit: $scope.contactnoedit,
                        emailedit: $scope.emailedit,
                        addressline1edit: $scope.txtaddress1,
                        regionedit: $scope.regionedit,
                        addressline2edit: $scope.txtaddress2,
                        countryedit: $scope.countryedit,
                        vertical_gid: $scope.vertical,
                        vertical_code: vertical_code,
                        state_gid: $scope.state_gid,
                        state: state_name,
                        tomailedit: $scope.tomailedit,
                        ccmailedit: $scope.ccmailedit,
                        postalcodeedit: $scope.postalcodeedit,
                        zonalGid: $scope.zonalHead,
                        businessHeadGid: $scope.businessHead,
                        regionalHeadGid: $scope.regionalHead,
                        clustermanagerGid: $scope.clustermanager,
                        creditmanagerGid: $scope.creditmanager,
                        relationshipMgmtGid: $scope.relationshipMgmt,
                        zonal_name: zonalHead_name,
                        businesshead_name: businessHead_name,
                        regionalhead_name: regionalHead_name,
                        cluster_manager_name: cluster_manager_name,
                        creditmanager_name: creditmanager_name,
                        relationshipmgmt_name: relationshipMgmt_name,
                        customer_urnedit: $scope.customerURNedit,
                        gst_number: $scope.gst_number,
                        pan_number: $scope.pan_number,
                        major_corporateedit: $scope.txtmajor_corporateedit,
                        constitution_nameedit: constitutionname,
                        constitution_gidedit: $scope.cboconstitutionedit,
                        zonal_riskmanagerGID: $scope.ZonalRM,
                        zonal_riskmanagerName: zonlRM_name,
                        risk_managerGID: $scope.riskmanager,
                        risk_managerName: riskmanager_name,
                        riskMonitoring_GID: $scope.RiskMonitoringName,
                        riskMonitoring_Name: RiskMonitoring_Name
                    }
                    var url = 'api/customer/customerUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            $state.go('app.registerCustomersummary');
                            Notify.alert('Customer Updated Successfully..!!', 'success')
                        }

                        else {
                            Notify.alert(resp.data.message)
                        }
                        activate();
                    });
                }
            });
            }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('registerCustomersummary', registerCustomersummary);

    registerCustomersummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', 'DownloaddocumentService'];

    function registerCustomersummary($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'registerCustomersummary';

        activate();


        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/customer/customerdetail';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.customer_data = resp.data.customer_list;
                 // new code start   
                 if ($scope.customer_data== null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.customer_data.length;
                                        if ($scope.customer_data.length < 100) {
                                            $scope.totalDisplayed = $scope.customer_data.length;
                                        }
                                    }
                    // new code end
                // $scope.total=$scope.customer_data.length;
            });

            // var url = 'api/employee/employee';
            // SocketService.get(url).then(function (resp) {
            //     $scope.employee_list = resp.data.employee_list;
            // });
        }
        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
             // new code start
        if ($scope.customer_data!= null) {
       
                if (pagecount < $scope.customer_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.customer_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.customer_data.length;
                        Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };

        $scope.popupcustomer = function () {

            $state.go('app.registerCustomer');
        }


        $scope.btntag2legal = function (customer_gid, customername, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/tagtoLegal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.customername = customername;

                if (customer_urn != "") {
                    $scope.customer_urn = customer_urn;
                }
                else {
                    $scope.customer_urn = "-";
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.btnconfirm = function () {

                    var params = {
                        customer_gid: customer_gid,
                        customer_name: customername,
                        tag_remarks: $scope.txttag_remarks,
                        currentcustomer_urn: customer_urn
                    }

                    lockUI();
                    var url = "api/customer/TagtoLegal";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
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
        }

        $scope.edit = function (val) {
            $scope.customer_gid = val;
            $scope.customer_gid = localStorage.setItem('customer_gid', val);
            $state.go('app.registercustomerEdit');
        }

        $scope.updatecustomerURN = function (customer_gid, customername, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/updateURN.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.customername = customername;

                if (customer_urn != "") {
                    $scope.customer_urn = customer_urn;
                }
                else {
                    $scope.customer_urn = "-";
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.UrnUpdate = function () {

                    var params = {
                        customer_gid: customer_gid,
                        newcustomer_urn: $scope.txtnewcustomerURN,
                        currentcustomer_urn: customer_urn
                    }

                    lockUI();
                    var url = "api/customer/GetNewCustomerURN";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

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
        }

        $scope.btntag2npa = function (customer_gid, customername, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/tagtoNPA.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.customername = customername;

                if (customer_urn != "") {
                    $scope.customer_urn = customer_urn;
                }
                else {
                    $scope.customer_urn = "-";
                }


                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/Customer/TaggedNPAHistoryList';
                SocketService.getparams(url, params).then(function (resp) {
                    //console.log(resp.data.customertag_list, url);
                    $scope.customertagnpa_list = resp.data.customertagnpa_list;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };



                $scope.btnconfirmnpa = function () {

                    var params = {
                        customer_gid: customer_gid,
                        customer_name: customername,
                        tag_remarks: $scope.txttag_remarks,
                        currentcustomer_urn: customer_urn
                    }

                    lockUI();
                    var url = "api/customer/TagtoNPA";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
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
        }

        $scope.exportcustomerdata = function () {
            lockUI();
            var url = 'api/customer/ExportCustomer';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname);
                    unlockUI();

                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')
                    activate();
                }

            });
        }

        $scope.delete = function (customer_gid) {
            var params = {
                customer_gid: customer_gid
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
                    var url = 'api/customer/customerDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };


    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('reopen', reopen);

    reopen.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function reopen($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'reopen';

        activate();
        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/deferral/reopensummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                // $scope.deferralSummary = resp.data;
                $scope.cadApproval_data = resp.data.rmdeferralSummaryDtls;
                 // new code start   
                 if ($scope.cadApproval_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.cadApproval_data.length;
                                        if ($scope.cadApproval_data.length < 100) {
                                            $scope.totalDisplayed = $scope.cadApproval_data.length;
                                        }
                                    }
                    // new code end
                // $scope.total=$scope.cadApproval_data.length;

            });


        }
        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
             // new code start
        if ($scope.cadApproval_data != null) {
       
                if (pagecount < $scope.cadApproval_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.cadApproval_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.cadApproval_data.length;
                        Notify.alert(" Total Summary " + $scope.cadApproval_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.cadApproval_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };

        $scope.isShowHide = function (param) {
            if (param == "show") {
                $scope.showval = true;
                $scope.hideval = true;
            }
            else if (param == "hide") {
                $scope.showval = false;
                $scope.hideval = false;
            }
            else {
                $scope.showval = false;
                $scope.hideval = false;
            }
        }

        $scope.popupApprove = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            //console.log(val);
            $state.go('app.reopenclosed');

        }



        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split(".")
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }
        // Close Modals

        $scope.close = function (val) {
            document.getElementById("userform").reset();
            var doc = document.getElementById(val);
            doc.style.display = 'none';
        }

    }
})();

(function () {
    'use strict';


    angular
        .module('angle')
        .controller('reopenclosed', reopenclosed);

    reopenclosed.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function reopenclosed($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'reopenclosed';

        activate();
        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/getReopen';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadDocumentname = resp.data;
                $scope.customer_remarks = resp.data.customer_remarks;
                $scope.approvalremarks = resp.data.remarks;
                $scope.deferrals = resp.data.deferralSummaryDtls;
                $scope.deferral_gid = resp.data.deferral_gid;
                $scope.loanref_no = resp.data.loanref_no;
                $scope.loan_title = resp.data.loan_title;
                $scope.record_id = resp.data.record_id;
                $scope.deferral_name = resp.data.deferral_name;
                $scope.approval_remarks = resp.data.approval_remarks;
                $scope.approval_status = resp.data.approval_status;
                $scope.def_status = resp.data.def_status;
                $scope.filename_list = resp.data.filename_list;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.businesshead_name = resp.data.businesshead_name;
                $scope.rm_name = resp.data.rm_name;
                $scope.credit_manager = resp.data.credit_manager;
                $scope.customer_code = resp.data.customer_code;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.entity_name = resp.data.entity_name;
                $scope.branch_name = resp.data.branch_name;
                $scope.deferral_approver_name = resp.data.cluster_manager_name;
                //console.log($scope.UploadDocumentname);
                if (resp.data.approval_status == "Approval Pending" || resp.data.approval_status == "Extension Approval Pending") {
                    $scope.uploaddoc = true;
                }
                else {
                    $scope.uploaddoc = false;
                }

            });

            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/getdeferralstages';

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.stage_list = resp.data.stage_list;
                }
                else {
                    document.getElementById("stages").style.display = "none";
                }
            });


            var params = {
                deferral_gid: localStorage.getItem('deferral_gid')
            }
            var url = 'api/deferral/checkerlist';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checker_list = resp.data.checker_list;
                if ($scope.checker_list == null) {
                    $scope.approval_history = true;
                }
                else {
                    $scope.approval_history = false;
                }
            });

            $scope.upload = function (val, val1, name) {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('deferral_gid', $scope.deferral_gid);
                frm.append('loan_gid', $scope.loan_gid);
                frm.append('by', "cad");
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;

                var url = 'api/deferral/uploaddeferraldocumentbycad';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.filename_list = resp.data.filename_list;

                    $("#addupload").val('');

                    if (resp.data.status == true) {
                        Notify.alert('Document Uploaded Successfully..!!', 'success')

                    }
                    else {
                        Notify.alert('File Format Not Supported!')
                    }
                });
            }

            $scope.downloads = function (val1, val2) {
                //var phyPath = val1;
                //var relPath = phyPath.split("StoryboardAPI");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                ////console.log(str);
                //var link = document.createElement("a");
                //var name = val2.split('.');
                //link.download = val2;
                //var uri = str;
                //link.href = uri;
                //link.click();
                DownloaddocumentService.Downloaddocument(val1, val2);

            }
            // Close Modals

            $scope.close = function (val) {
                document.getElementById("userform").reset();
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }
            $scope.Approve = function () {
                var params = {
                    def_gid: $scope.deferral_gid,
                    deferral_status: $scope.deferral_status,
                    due_date: $scope.extened_date,
                    approval_remarks: $scope.approvalremarks,
                    customer_remarks: $scope.customer_remarks
                }
                console.log(params);
                var url = 'api/deferral/deferralApprove';

                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $state.go('app.reopen');
                        Notify.alert('Submitted Successfully..!!', 'success')
                    }
                    else {
                        Notify.alert('Error Occurred While Submitting!')
                    }
                });
            }


        }

        $scope.deferralback = function (val) {
            $state.go('app.reopen');
        }


        $scope.onselectedchange = function (deferral_status) {
            if (deferral_status == "Extend") {
                $scope.showval = true;
            }
            else {
                $scope.showval = false;
            }
        }


    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('report', report);

    report.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function report($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        //vm.title = 'deferralReport';

        activate();
        function activate() {
            $scope.page = localStorage.getItem('page');

            $scope.totalDisplayed=100;
            var url = 'api/deferral/reportView';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.deferral_data = resp.data.deferralSummaryDtls;
                // new code start   
                if ($scope.deferral_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.deferral_data.length;
                                        if ($scope.deferral_data.length < 100) {
                                            $scope.totalDisplayed = $scope.deferral_data.length;
                                        }
                                    }
                    // new code end
                // $scope.total=$scope.deferral_data.length;

            });
            // var url = 'api/customer/customer';
            // SocketService.get(url).then(function (resp) {
            //     $scope.customer_list = resp.data.customer_list;
            // });

             var url = 'api/vertical/vertical';
             SocketService.get(url).then(function (resp) {
                 $scope.vertical_list = resp.data.vertical_list;
             });
             var url = 'api/branch/branch';
             SocketService.get(url).then(function (resp) {
                 $scope.branch_list = resp.data.branch_list;

             });

             var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            // var url = 'api/employee/employee';
            // SocketService.get(url).then(function (resp) {
            //     $scope.employee_list = resp.data.employee_list;

            // });

             var url = 'api/entity/entity';
             SocketService.get(url).then(function (resp) {
                 $scope.entity_list = resp.data.entity_list;
             });

        }


        $scope.complete = function (string) {

            if (string.length >= 3) {
                $scope.message = "";
                var url = 'api/customer/ExploreCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        $scope.message = "No Records";
                    }
                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "Type atleast three character";
            }
        }

        $scope.fillTextbox = function (customer_gid, customer_name) {
            //console.log('string', customer_name, customer_gid);
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;
        }
        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
        if ($scope.deferral_data != null) {
       
                if (pagecount < $scope.deferral_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.deferral_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.deferral_data.length;
                        Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };
        $scope.all = function () {
            $scope.entity_gid = "";
            $scope.branch = "";
            $scope.customer_gid = "";
            $scope.customer = "";
            $scope.vertical = "";
            $scope.cbostate = "";
            //document.getElementById(userform7).reset();
            activate();
        }

        $scope.search = function () {
            var params = {
                customer_gid: $scope.customer_gid,
                vertical_gid: $scope.vertical,
                entity_gid: $scope.entity_gid,
                branch_gid: $scope.branch,
                relationshipMgmt: $scope.relationshipMgmt,
                zonalHead: $scope.zonalHead,
                businessHead: $scope.businessHead,
                deferralApprover: $scope.deferralApprover,
                state_gid:$scope.cbostate
            }
            lockUI();
            var url = 'api/deferral/reportsummaryview';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                $scope.deferral_data = resp.data.deferralSummaryDtls;
            });
        }

        $scope.popupView = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            localStorage.setItem('page', 'UserReport');
            $state.go('app.reportpagedetails');

        }

        $scope.export = function () {
            if($scope.cbostate == undefined)
            {
                $scope.cbostate = "";
            }
            var params = {
               
                customer_gid: $scope.customer_gid,
                vertical_gid: $scope.vertical,
                branch_gid: $scope.branch,
                entity_gid: $scope.entity_gid,
                relationshipMgmt: $scope.relationshipMgmt,
                zonalHead: $scope.zonalHead,
                businessHead: $scope.businessHead,
                deferralApprover: $scope.deferralApprover,
                state_gid:$scope.cbostate
            }
            //console.log(params);
            lockUI();
            var url = 'api/deferral/excelexport';
           
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname);
                    unlockUI();
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // //console.log(str);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                   
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
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
        .controller('ReportDetails', ReportDetails);

    ReportDetails.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService'];

    function ReportDetails($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewDeferral';

        activate();
        function activate() {
            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/Getcaddoc';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadDocumentname = resp.data;
                $scope.deferrals = resp.data.deferralSummaryDtls;
                $scope.deferral_gid = resp.data.deferral_gid;
                $scope.loanref_no = resp.data.loanref_no;
                $scope.loan_title = resp.data.loan_title;
                $scope.record_id = resp.data.record_id;
                $scope.deferral_name = resp.data.deferral_name;
                $scope.approval_remarks = resp.data.approval_remarks;
                $scope.approval_status = resp.data.approval_status;
                $scope.filename_list = resp.data.filename_list;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.businesshead_name = resp.data.businesshead_name;
                $scope.rm_name = resp.data.rm_name;
                $scope.credit_manager = resp.data.credit_manager;
                $scope.customer_code = resp.data.customer_code;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.entity_name = resp.data.entity_name;
                $scope.branch_name = resp.data.branch_name;
                $scope.cluster_manager_name = resp.data.cluster_manager_name;
            });

            var url = 'api/deferral/getdeferralstages';

            SocketService.getparams(url, params).then(function (resp) {

                $scope.stage_list = resp.data.stage_list;

            });

            // Close Modals

            $scope.close = function (val) {
                document.getElementById("userform").reset();
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }
            $scope.downloads = function (val1, val2) {
                //var phyPath = val1;
                //var relPath = phyPath.split("StoryboardAPI");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                ////console.log(str);
                //var link = document.createElement("a");
                //var name = val2.split('.');
                //link.download = val2;
                //var uri = str;
                //link.href = uri;
                //link.click();
                DownloaddocumentService.Downloaddocument(val1, val2);

            }

        }

        $scope.deferralback = function (val) {
            $state.go('app.deferralReport');
        }



    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('reportpagedetails', reportpagedetails);

    reportpagedetails.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService'];

    function reportpagedetails($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewDeferral';

        activate();
        function activate() {
            $scope.page = localStorage.getItem('page');

            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/Getcaddoc';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadDocumentname = resp.data;
                $scope.deferrals = resp.data.deferralSummaryDtls;
                $scope.deferral_gid = resp.data.deferral_gid;
                $scope.loanref_no = resp.data.loanref_no;
                $scope.loan_title = resp.data.loan_title;
                $scope.record_id = resp.data.record_id;
                $scope.deferral_name = resp.data.deferral_name;
                $scope.approval_remarks = resp.data.approval_remarks;
                $scope.approval_status = resp.data.approval_status;
                $scope.filename_list = resp.data.filename_list;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.businesshead_name = resp.data.businesshead_name;
                $scope.rm_name = resp.data.rm_name;
                $scope.credit_manager = resp.data.credit_manager;
                $scope.customer_code = resp.data.customer_code;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.entity_name = resp.data.entity_name;
                $scope.branch_name = resp.data.branch_name;
                $scope.cluster_manager_name = resp.data.cluster_manager_name;
            });

              var url = 'api/deferral/getdeferralstages';

            SocketService.getparams(url, params).then(function (resp) {
               
                $scope.stage_list = resp.data.stage_list;
               
            });
            // Close Modals

            $scope.close = function (val) {
                document.getElementById("userform").reset();
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }
            $scope.downloads = function (val1, val2) {
                //var phyPath = val1;
                //var relPath = phyPath.split("StoryboardAPI");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                ////console.log(str);
                //var link = document.createElement("a");
                //var name = val2.split('.');
                //link.download = val2;
                //var uri = str;
                //link.href = uri;
                //link.click();
                DownloaddocumentService.Downloaddocument(val1, val2);

            }

        }

        $scope.deferralback = function () {
            if ($scope.page == 'UserReport2')
            {
                $state.go('app.DtsRptUserReport2');
            }
            else {
                $state.go('app.userReport');
            }
        }



    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('reportView', reportView);

    reportView.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function reportView($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        //vm.title = 'deferralReport';

        activate();
        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/deferral/directDeferralSummaryview';
            lockUI();
            SocketService.get(url).then(function (resp) {
               unlockUI();
                $scope.deferral_data = resp.data.deferralSummaryDtls;
                // new code start   
                if ($scope.deferral_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.deferral_data.length;
                                        if ($scope.deferral_data.length < 100) {
                                            $scope.totalDisplayed = $scope.deferral_data.length;
                                        }
                                    }
                    // new code end
                // $scope.total=$scope.deferral_data.length;

            });
            // var url = 'api/customer/customer';
            // SocketService.get(url).then(function (resp) {
            //     $scope.customer_list = resp.data.customer_list;
            // });

            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });
            
            var url = 'api/branch/branch';
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branch_list;
            });
        
            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            var url = 'api/entity/entity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
            });

        }
        



        $scope.complete = function (string) {

            if (string.length >= 3) {
                $scope.message = "";
                var url = 'api/customer/ExploreCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        $scope.message = "No Records";
                    }
                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "Type atleast three character";
            }
        }

        $scope.fillTextbox = function (customer_gid, customer_name) {
            //console.log('string', customer_name, customer_gid);
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;
        }

         $scope.all = function () {
             $scope.entity_gid = "";
             $scope.branch = "";
             $scope.customer_gid = "";
             $scope.customer = "";
             $scope.vertical = "";
             $scope.cbostate = "";
             //document.getElementById(userform7).reset();
             activate();
         }
        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
        if ($scope.deferral_data != null) {
       
                if (pagecount < $scope.deferral_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.deferral_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.deferral_data.length;
                        Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };
           $scope.search = function () {
            var params = {
                customer_gid: $scope.customer_gid,
                vertical_gid: $scope.vertical,
                entity_gid: $scope.entity_gid,
                branch_gid: $scope.branch,
                relationshipMgmt: $scope.relationshipMgmt,
                zonalHead: $scope.zonalHead,
                businessHead: $scope.businessHead,
                deferralApprover: $scope.deferralApprover,
                state_gid:$scope.cbostate
            }
            lockUI();
            var url = 'api/deferral/directDeferralSummaryreportview';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                $scope.deferral_data = resp.data.deferralSummaryDtls;
            });
        }

        $scope.popupView = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.reportViewdetails');

        }

        $scope.export = function () {
            var params = {
                customer_gid: $scope.customer_gid,
                vertical_gid: $scope.vertical,
                branch_gid: $scope.branch,
                entity_gid: $scope.entity_gid,
                relationshipMgmt: $scope.relationshipMgmt,
                zonalHead: $scope.zonalHead,
                businessHead: $scope.businessHead,
                deferralApprover: $scope.deferralApprover,
                state_gid:$scope.cbostate
            }
            //console.log(params);
            lockUI();
            var url = 'api/deferral/excel';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname);
                    unlockUI();
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // //console.log(str);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
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
        .controller('reportViewdetails', reportViewdetails);

    reportViewdetails.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService'];

    function reportViewdetails($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewDeferral';

        activate();
        function activate() {
            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/Getcaddoc';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadDocumentname = resp.data;
                $scope.deferrals = resp.data.deferralSummaryDtls;
                $scope.deferral_gid = resp.data.deferral_gid;
                $scope.loanref_no = resp.data.loanref_no;
                $scope.loan_title = resp.data.loan_title;
                $scope.record_id = resp.data.record_id;
                $scope.deferral_name = resp.data.deferral_name;
                $scope.approval_remarks = resp.data.approval_remarks;
                $scope.approval_status = resp.data.approval_status;
                $scope.filename_list = resp.data.filename_list;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.businesshead_name = resp.data.businesshead_name;
                $scope.rm_name = resp.data.rm_name;
                $scope.credit_manager = resp.data.credit_manager;
                $scope.customer_code = resp.data.customer_code;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.entity_name = resp.data.entity_name;
                $scope.branch_name = resp.data.branch_name;
                $scope.cluster_manager_name = resp.data.cluster_manager_name;
            });

            var url = 'api/deferral/getdeferralstages';

            SocketService.getparams(url, params).then(function (resp) {
               
                    $scope.stage_list = resp.data.stage_list;
               
            });

            // Close Modals

            $scope.close = function (val) {
                document.getElementById("userform").reset();
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }
            $scope.downloads = function (val1, val2) {
                //var phyPath = val1;
                //var relPath = phyPath.split("StoryboardAPI");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                ////console.log(str);
                //var link = document.createElement("a");
                //var name = val2.split('.');
                //link.download = val2;
                //var uri = str;
                //link.href = uri;
                //link.click();
                DownloaddocumentService.Downloaddocument(val1, val2);

            }

        }

        $scope.deferralback = function (val) {
            $state.go('app.cadReport');
        }



    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('RMDetails', RMDetails);

    RMDetails.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function RMDetails($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'RMDetails';
        
        activate();
        function activate() {


            $scope.relationshipmgmt_gid = localStorage.getItem('relationshipmgmt_gid');
            var params = {
                relationshipmgmt_gid: $scope.relationshipmgmt_gid
            };
         
            var url = 'api/deferral/rmdeferraldetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.deferral_data = resp.data.deferralSummaryDtls;
                unlockUI();
            });
           
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });


        }

        $scope.checkall = function (selected) {
            //console.log(selected);
            angular.forEach($scope.deferral_data, function (val) {
                val.checked = selected;
            });
        }

        $scope.back = function (val) {
            $state.go('app.transferRM');
        }

       

        $scope.transfer = function () {
            var deferralGidList = [];

            angular.forEach($scope.deferral_data, function (val) {

                if (val.checked == true) {
                    var deferral_gid = val.deferral_gid;
                    deferralGidList.push(deferral_gid);
                    
                }
            });
          
           
            var params = {
                deferral_gid: deferralGidList,
                employee_gid: $scope.employee_gid
            }
            //console.log(params);
            var url = 'api/deferral/deferralTransfer';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert('Transferred Successfully!', 'success');
                    $state.go('app.transferRM');
                }
                else {
                    Notify.alert('Select Atleast One!')                
                }
                
            });

        }

    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmManagementcontroller', rmManagementcontroller);

    rmManagementcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function rmManagementcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmManagementcontroller';

        activate();
        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/deferral/getrmSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.rmDeferral_data = resp.data.rmdeferralSummaryDtls;
                // new code start   
                unlockUI();
                if ($scope.rmDeferral_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.rmDeferral_data.length;
                                        if ($scope.rmDeferral_data.length < 100) {
                                            $scope.totalDisplayed = $scope.rmDeferral_data.length;
                                        }
                                    }
                    // new code end
                // if( $scope.rmDeferral_data==null){
                //     $scope.total=0;
                // }
                // else{
                //     $scope.total=$scope.rmDeferral_data.length;
                // }
             
           });
         
        }

        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
        if ($scope.rmDeferral_data != null) {
       
                if (pagecount < $scope.rmDeferral_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.rmDeferral_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.rmDeferral_data.length;
                        Notify.alert(" Total Summary " + $scope.rmDeferral_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.rmDeferral_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };



        $scope.popupUpload = function (val_deferral, val_loan) {
            $scope.deferral_gid = val_deferral;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val_deferral);
          
            $scope.loan_gid = val_loan;
            $scope.loan_gid = localStorage.setItem('loan_gid', val_loan);

            $state.go('app.rmManagementRequest');

        }

       }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmManagementRequest', rmManagementRequest);

    rmManagementRequest.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function rmManagementRequest($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmManagementRequest';

        activate();
        function activate() {

            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/getDeferralDocument';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadDocumentname = resp.data;
                $scope.deferrals = resp.data.deferralSummaryDtls;
                $scope.deferral_gid = resp.data.deferral_gid;
                $scope.loanref_no = resp.data.loanref_no;
                $scope.loan_title = resp.data.loan_title;
                $scope.record_id = resp.data.record_id;
                $scope.deferral_name = resp.data.deferral_name;
                $scope.approval_remarks = resp.data.approval_remarks;
                $scope.approval_status = resp.data.approval_status;
                $scope.filename_list = resp.data.filename_list;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.businesshead_name = resp.data.businesshead_name;
                $scope.rm_name = resp.data.rm_name;
                $scope.customer_code = resp.data.customer_code;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.entity_name = resp.data.entity_name;
                $scope.credit_manager = resp.data.credit_manager;
                $scope.branch_name = resp.data.branch_name;
                $scope.cluster_manager_name = resp.data.cluster_manager_name;

                if (resp.data.approval_status == "Pending" || resp.data.approval_status == "PushBack" || resp.data.approval_status == "ReOpen") {
                    $scope.uploaddoc = true;
                }

                else {
                    $scope.uploaddoc = false;
                }
              
            });

            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/getdeferralstages';

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.stage_list = resp.data.stage_list;
                }
                else {
                    document.getElementById("stages").style.display = "none";
                }
            });

            $scope.upload = function (val, val1, name) {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('deferral_gid', $scope.deferral_gid);
                frm.append('loan_gid', $scope.loan_gid);
                frm.append('by', "cad");
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;

                var url = 'api/deferral/UploadDocument' ;
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.filename_list = resp.data.filename_list;

                    $("#addupload").val('');

                    if (resp.data.status == true) {
                        Notify.alert('Document Uploaded Successfully..!!', 'success')

                    }
                    else {
                        Notify.alert('File Format Not Supported!')
                    }
                });
            }

            $scope.downloads = function (val1, val2) {
                //var phyPath = val1;
                //var relPath = phyPath.split("StoryboardAPI");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                ////console.log(str);
                //var link = document.createElement("a");
                //var name = val2.split('.');
                //link.download = val2;
                //var uri = str;
                //link.href = uri;
                //link.click();
                DownloaddocumentService.Downloaddocument(val1, val2);

            }


            $scope.Approve = function () {
                var params = {
                    def_gid: $scope.deferral_gid,
                    deferral_status: $scope.deferral_status,
                    due_date: $scope.extened_date,
                    applied_remarks: $scope.applied_remarks
                }

                var url = 'api/deferral/getApprove';

                SocketService.post('api/deferral/getApprove', params).then(function (resp) {
                    if (resp.data.status == true) {
                        $state.go('app.rmManagement');
                        Notify.alert('Request Raised Successfully..!!','success')
                    }
                    else {
                        Notify.alert('Error Occurred While Requesting!')
                    }
                });
            }

            
        }

        $scope.deferralback = function (val) {
            $state.go('app.rmManagement');
        }

       
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('securityTypecontroller', securityTypecontroller);

    securityTypecontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function securityTypecontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'securityTypecontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            $scope.totoalDisplayed=100;
            var url = 'api/security/getSecuritytype';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.security_data = resp.data.securitytype_list;
                // new code start   
                unlockUI();
                if ($scope.security_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.security_data.length;
                                        if ($scope.security_data.length < 100) {
                                            $scope.totalDisplayed = $scope.security_data.length;
                                        }
                                    }
                    // new code end
                // $scope.total=$scope.security_data.length;
            });
        }
     
  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
             if ($scope.security_data != null) {
       
                        if (pagecount < $scope.security_data.length) {
                            $scope.totalDisplayed += Number;
                            if($scope.security_data.length<$scope.totalDisplayed){
                                $scope.totalDisplayed =$scope.security_data.length;
                                Notify.alert(" Total Summary " + $scope.security_data.length + " Records Only", "warning");
                            }
                            unlockUI();
                        }
                        else {
                            unlockUI();
                            Notify.alert(" Total Summary " + $scope.security_data.length + " Records Only", "warning");
                            return;
                        }
                    }
                    // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
  };
  $scope.popupSecurity = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                
                $scope.SecuritySubmit = function () {
                    var params = {
                        security_type: $scope.txtsecurity_type,
                        bureau_code: $scope.txtbureau_code,
                        lms_code: $scope.txtlms_code,
                    }
                    //console.log(params);
                    var url = 'api/security/createSecurityType';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Security Type Added Successfully..!!', 'success')
                            activate();

                        }
                        else {
                            Notify.alert('Error Occurred While Adding Security Type!', 'warning')
                            
                        }
                    });
                    $modalInstance.close('closed');
                    activate();
                }
          

            }
        }

        $scope.edit = function (securitytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/securityedit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            //var doc = document.getElementById('edit');
            //doc.style.display = 'block';
            $scope.securitytype_gid = securitytype_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                //$scope.customer_gid = customer_gid;
                $scope.securitytype_gid = localStorage.setItem('securitytype_gid', securitytype_gid);
                var params = {
                    securitytype_gid: securitytype_gid
                }
                var url = 'api/security/GetSecurityTypeEdit';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditsecurity_type = resp.data.security_type;
                    $scope.securityTypegid = resp.data.securitytype_gid;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };

                $scope.securityUpdate = function () {

                    var params = {
                        security_type: $scope.txteditsecurity_type,
                        securitytype_gid: securitytype_gid,
                        bureau_code: $scope.txteditbureau_code,
                        lms_code: $scope.txteditlms_code,
                    }
                    var url = 'api/security/securityTypeUpdate';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            //$scope.close('edit');
                            $modalInstance.close('closed');
                            //SweetAlert.swal('Success!', 'Covenant Type Updated!', 'success');
                            Notify.alert('Security Type Updated Successfully..!!', {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            
                            Notify.alert('Error Occurred ', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            }

        }
        $scope.delete = function (securitytype_gid) {
            var params = {
                securitytype_gid: securitytype_gid
            }
            var url = 'api/security/securityTypeDelete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    SweetAlert.swal({
                        title: 'Are you sure?',
                        text: 'Do You Want To Delete the Record ?',
                        showCancelButton: true,
                        confirmButtonColor: '#DD6B55',
                        confirmButtonText: 'Yes, delete it!',
                        closeOnConfirm: false
                    }, function (isConfirm) {
                        if (isConfirm) {
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                            activate();
                        }

                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    activate();
                }
            });
        };
        $scope.Status_update = function (securitytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCompanyDocument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    securitytype_gid: securitytype_gid
                }
                var url = 'api/security/GetSecurityTypeEdit';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.securityTypenameedit = resp.data.security_type;
                    $scope.securityTypegid = resp.data.securitytype_gid;
                    $scope.txtbureau_codeedit = resp.data.bureau_code;
                    $scope.txtlms_codeedit = resp.data.lms_code;
                    $scope.rbo_status = resp.data.status_log;
                });
                var url = 'api/security/GetActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.securitytype_list = resp.data.securitytype_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        remarks: $scope.txtremarks,
                        status_log: $scope.rbo_status,
                        securitytype_gid: securitytype_gid
                    }
                    var url = 'api/security/securityTypeStatusUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');

                }
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('segmentcontroller', segmentcontroller);

    segmentcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function segmentcontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'segmentcontroller';
        //console.log($scope.segment_name);
        activate();


        function activate() {

            var url = 'api/segment/segment';
            SocketService.get(url).then(function (resp) {
                $scope.segment = resp.data.segment_list;
                //console.log($scope.segment);
            });
        }        
              
              $scope.delete = function (segment_gid) {
                var params = {
                    segment_gid: segment_gid
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
                        var url = 'api/segment/segmentDelete';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                activate();
                            }
                            else {
                                Notify.alert('Error Occurred While Deleting Customer!', {
                                    status: 'warning',
                                    pos: 'top-right',
                                    timeout: 3000
                                });
                                activate();
                            }
                        });
                        SweetAlert.swal('Deleted Successfully!');
                    }

                });
            };
        
        $scope.popupsegment = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {             
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.segmentSubmit = function () {
                    var params = {
                        segment_code: $scope.segment_code,
                        segment_name: $scope.segment_name
                    }                    
                    var url = 'api/segment/createSegment';
                   
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Vertical Added Successfully..!!', 'success')
                            activate();
                        
                          }
                        else {
                            Notify.alert('Error Occurred While Adding Vertical!', 'warning')
                            activate();
                        }
                    });
                    $state.go('app.segment');
                }
            }
        }

        $scope.edit = function (segment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    segment_gid: segment_gid
                }
                var url = 'api/segment/Getsegmentupdate';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.verticalNameedit = resp.data.verticalNameedit;
                    $scope.descriptionedit = resp.data.descriptionedit;
                    $scope.segment_gid = resp.data.segment_gid;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.segmentUpdate = function () {

                    var params = {
                        verticalNameedit: $scope.verticalNameedit,
                        descriptionedit: $scope.descriptionedit,
                        segment_gid: $scope.segment_gid
                    }
                    var url = 'api/segment/segmentUpdate';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            $modalInstance.close('closed');
                            Notify.alert('Vertical Updated Successfully..!!', 'success')

                        }
                        else {
                            Notify.alert('Error Occurred While Updating Vertical !', 'success')
                            activate();

                        }
                    });
                }
            }

        }
       }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sendMailalert', sendMailalert);

    sendMailalert.$inject = ['$rootScope','$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function sendMailalert($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sendMailalert';

        activate();

        function activate() {
            $scope.customeralert_gid = localStorage.getItem('customeralert_gid');
            var url = 'api/customerAlertGenerate/Getcustomerdetails';
            var params = {
                customeralert_gid: $scope.customeralert_gid
            };
           
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customermail_list = resp.data;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.customeralert_gid = resp.data.customeralert_gid;
                $scope.customer_code = resp.data.customercode;
                $scope.customer_name = resp.data.customername;
                $scope.content = resp.data.content;

                document.getElementById('test').innerHTML += $scope.content;
              
                $scope.mailalert_list = resp.data.mailalert_list;

            });
            
           

        }

       

        $scope.onselectedchangeTemplate = function (template) {
            var params = {
                template_gid: template
            }
            var url = 'api/template/Content';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.content = resp.data.template_content;

            });

        }

        $scope.sendmail = function (customeralert_gid) {
          
            var params = {
                customeralert_gid: $scope.customeralert_gid,
                customer_gid: $scope.customer_gid,
                content: $scope.content,
                customercode: $scope.customer_code,
                customername: $scope.customer_name
            }
           
            var url = 'api/customerAlertGenerate/sendMail';
            lockUI();
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Mail Sent to the Customer Successfully!', 'success');
                    $state.go('app.mailManagement');
                }
                else {
                    unlockUI();
                    Notify.alert('Oops!Problem While Sent Mail.Kindly Check Mail ID')
                }

            });

        }


        $scope.sendback = function () {
            $state.go('app.mailManagement');
        }

      
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sidebarcontroller', sidebarcontroller);

    sidebarcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function sidebarcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sidebarcontroller';

        activate();

        function activate() {
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('taggedCustomerListController', taggedCustomerListController);

    taggedCustomerListController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function taggedCustomerListController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        var relpath1;
        vm.title = 'taggedCustomerListController';

        activate();


        function activate() {

            var url = window.location.href;
            var relPath = url.split("lspage=");
            $scope.relpath1 = relPath[1];
            //console.log(relpath1);

            $scope.totalDisplayed = 100;
            var url = 'api/Customer/TaggedCustomerList';
            SocketService.get(url).then(function (resp) {
                $scope.customertag_list = resp.data.customertag_list;
                unlockUI();
                // new code start   
                if ($scope.customertag_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.customertag_list.length;
                    if ($scope.customertag_list.length < 100) {
                        $scope.totalDisplayed = $scope.customertag_list.length;
                    }
                }
                // new code endd
                //$scope.total=$scope.covenant_data.length;

            });
        }


        $scope.btnback = function () {
            //console.log($scope.relpath1);
            if ($scope.relpath1 == "regcustomer") {
                //console.log('sub');
                $state.go('app.registerCustomersummary');
              
            }
            else {
                //console.log('main');
                $state.go('app.customerMaster');
            }      
        }

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.covenant_data != null) {

                if (pagecount < $scope.covenant_data.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.covenant_data.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.covenant_data.length;
                        Notify.alert(" Total Summary " + $scope.covenant_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.covenant_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };
   

        $scope.untag = function (customer_gid, customer_name, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/customeruntag.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            //var doc = document.getElementById('edit');
            //doc.style.display = 'block';
            $scope.customer_gid = customer_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;

                if (customer_urn != "") {
                    $scope.customer_urn = customer_urn;
                }
                else {
                    $scope.customer_urn = "-";
                }
                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/Customer/TaggedHistoryList';
                SocketService.getparams(url, params).then(function (resp) {
                    //console.log(resp.data.customertag_list, url);
                    $scope.customertag_list = resp.data.customertag_list;
                    unlockUI();                  
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };
                $scope.btnuntagcustomer = function () {

                    var params = {
                        customer_gid: customer_gid,
                        customer_name: customer_name,
                        currentcustomer_urn: customer_urn,
                        untag_remarks: $scope.untagremarks
                    }
                    var url = 'api/Customer/UnTagtoLegal';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            $modalInstance.close('closed');
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

        }

       



    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('taggedNPACustomerListController', taggedNPACustomerListController);

    taggedNPACustomerListController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function taggedNPACustomerListController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        var relpath1;
        vm.title = 'taggedNPACustomerListController';

        activate();


        function activate() {

            var url = window.location.href;
            var relPath = url.split("lspage=");
            $scope.relpath1 = relPath[1];
            //console.log(relpath1);

            $scope.totalDisplayed = 100;
            var url = 'api/Customer/TaggedNPACustomerList';
            SocketService.get(url).then(function (resp) {
                $scope.customertagnpa_list = resp.data.customertagnpa_list;
                unlockUI();
                // new code start   
                if ($scope.customertagnpa_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.customertag_list.length;
                    if ($scope.customertagnpa_list.length < 100) {
                        $scope.totalDisplayed = $scope.customertag_list.length;
                    }
                }
                // new code endd
                //$scope.total=$scope.covenant_data.length;

            });
        }


        $scope.btnback = function () {
            //console.log($scope.relpath1);
            if ($scope.relpath1 == "regcustomer") {
                //console.log('sub');
                $state.go('app.registerCustomersummary');

            }
            else {
                //console.log('main');
                $state.go('app.customerMaster');
            }
        }

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.covenant_data != null) {

                if (pagecount < $scope.covenant_data.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.covenant_data.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.covenant_data.length;
                        Notify.alert(" Total Summary " + $scope.covenant_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.covenant_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };


        $scope.untagnpa = function (customer_gid, customer_name, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/customeruntagnpa.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            //var doc = document.getElementById('edit');
            //doc.style.display = 'block';
            $scope.customer_gid = customer_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;

                if (customer_urn != "") {
                    $scope.customer_urn = customer_urn;
                }
                else {
                    $scope.customer_urn = "-";
                }
                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/Customer/TaggedNPAHistoryList';
                SocketService.getparams(url, params).then(function (resp) {
                    //console.log(resp.data.customertag_list, url);
                    $scope.customertagnpa_list = resp.data.customertagnpa_list;
                    unlockUI();
                }); 

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };
                $scope.btnuntagnpacustomer = function () {

                    var params = {
                        customer_gid: customer_gid,
                        customer_name: customer_name,
                        currentcustomer_urn: customer_urn,
                        untag_remarks: $scope.untagremarks
                    }
                    var url = 'api/Customer/UnTagtoNPA';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            $modalInstance.close('closed');
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

        }





    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('topnavbarcontroller', topnavbarcontroller);

    topnavbarcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function topnavbarcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'topnavbarcontroller';

        activate();

        function activate() {
            $scope.user_name = localStorage.getItem('user_name');  
        }

        if (!$rootScope.$$listenerCount['downloadEvent']) {
            $rootScope.$on('downloadEvent', function (event, resp) {
                if (resp.data.status == true) {
                    if (resp.data.format == 'pdf') {
                        contentType = 'application/pdf';
                    }
                    else if (resp.data.format == 'xls') {
                        var contentType = 'application/vnd.ms-excel';
                    }
                    else if (resp.data.format == 'xlsx') {
                        var contentType = 'application/vnd.ms-excel';
                    }
                    var b64Data = resp.data.file;
                    var blob = b64toBlob(b64Data, contentType);
                    var blobUrl = URL.createObjectURL(blob);
                    var img = document.getElementById('btnpdf');
                    img.download = resp.data.name;
                    img.href = blobUrl;
                    img.click();
                } else {
                    Notify.alert('Error in downloading. please contact sysadmin',
                    {
                        status: 'warning',
                        pos: 'top-right',
                        timeout: 3000
                    });
                }
            });
        }

        if (!$rootScope.$$listenerCount['DocumentViewerListener']) {
            $rootScope.$on('DocumentViewerListener', function (event, resp) {
                if (resp.data.status == true) {
                    if (resp.data.format.toLowerCase() == 'pdf') {
                        contentType = 'application/pdf';
                    }  
                    else if (resp.data.format.toLowerCase() == 'png') {
                        var contentType = 'image/png';
                    }
                    else if (resp.data.format.toLowerCase() == 'jpg') {
                        var contentType = 'image/jpg';
                    }
                    else if (resp.data.format.toLowerCase() == 'jpeg') {
                        var contentType = 'image/jpeg';
                    }
                    else if (resp.data.format.toLowerCase() == 'txt') {
                        var contentType = 'text/plain';
                    }
                    else if (resp.data.format.toLowerCase() == 'html') {
                        var contentType = 'text/html';
                    }
                    var b64Data = resp.data.file;
                    var blob = b64toBlob(b64Data, contentType);
                    var blobUrl = URL.createObjectURL(blob);
                    const pdfWindow = window.open(""); 
                    pdfWindow.document.write("<iframe width='100%' height='100%'  src='" + blobUrl + "#toolbar=0" + "'></iframe>"); 
                } else {
                    Notify.alert('Error Occured. please contact sysadmin',
                    {
                        status: 'warning',
                        pos: 'top-right',
                        timeout: 3000
                    });
                }
            });
        } 

        function b64toBlob(b64Data, contentType, sliceSize) {
            contentType = contentType || '';
            sliceSize = sliceSize || 512; var byteCharacters = atob(b64Data);
            var byteArrays = [];
            for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
                var slice = byteCharacters.slice(offset, offset + sliceSize);
                var byteNumbers = new Array(slice.length);
                for (var i = 0; i < slice.length; i++) {
                    byteNumbers[i] = slice.charCodeAt(i);
                }
                var byteArray = new Uint8Array(byteNumbers);
                byteArrays.push(byteArray);
            }
            var blob = new Blob(byteArrays, { type: contentType }); return blob;
        }

        $scope.user_profile = function () {
            $location.url('app/MstUserProfile');
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('transferRMcontroller', transferRMcontroller);

    transferRMcontroller.$inject = ['$rootScope', '$scope','$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function transferRMcontroller($rootScope, $scope,$modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'transferRMcontroller';
       
        activate();

        function activate() {
            $scope.totalDisplayed=100;
            lockUI();
            var url = 'api/deferral/rm';
            SocketService.get(url).then(function (resp) {
                $scope.transferRM_data = resp.data.deferralSummaryDtls;
                // new code start 
                unlockUI();  
                if ($scope.transferRM_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.transferRM_data.length;
                                        if ($scope.transferRM_data.length < 100) {
                                            $scope.totalDisplayed = $scope.transferRM_data.length;
                                        }
                                    }
                    // new code end
                // if ( $scope.transferRM_data==null){
                //     $scope.total=0;
                // }
                // else{
                //     $scope.total= $scope.transferRM_data.length;
                // }
                //console.log(resp.data.deferralSummaryDtls);
            });
        }
           
            // document.getElementById('pagecount').onkeyup = function () {
           
            //     if($scope.pagecount==null){
            //      var el = document.getElementById('loadmore');
            //      el.style.backgroundColor = '#DCDCDC';  
            //     }
            //     else{
            //      var el = document.getElementById('loadmore');
            //      el.style.backgroundColor = '#ffa';
            //     }
            // };
    
      $scope.loadMore= function (pagecount) {
                if(pagecount==undefined){
                    Notify.alert("Enter the Total Summary Count","warning");
                    return;
                }
                lockUI();
    
                var Number = parseInt(pagecount);
                // new code start
        if ($scope.transferRM_data != null) {
       
                if (pagecount < $scope.transferRM_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.transferRM_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.transferRM_data.length;
                        Notify.alert(" Total Summary " + $scope.transferRM_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.transferRM_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
                // $scope.totalDisplayed += Number;
                // console.log(pagecount);
                unlockUI();
            };
    
           

            $scope.popuptransfer = function (val) {
                $scope.relationshipmgmt_gid = val;
                $scope.relationshipmgmt_gid = localStorage.setItem('relationshipmgmt_gid', val);
                $state.go('app.RMDetails');
            }

           


            $scope.close = function (val) {
                document.getElementById("userform").reset();
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }
        }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('user_logincontroller', user_logincontroller);

    user_logincontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies'];

    function user_logincontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'user_logincontroller';

        activate();

        function activate() {
        }

        $scope.submitclick = function () {
            var params = {
                user_code: $scope.user_code,
                user_password: $scope.user_password
            }
            var url = apiManage.apiList['user_login'].api;
            lockUI();
            SocketService.postlogin(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    $cookies.putObject('token', resp.data.token);
                    localStorage.setItem('user_name', resp.data.username)
                    $state.go('app.landingpage');
                }
            })
        };
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('verticalcontroller', verticalcontroller);

    verticalcontroller.$inject = ['$rootScope', '$scope','$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function verticalcontroller($rootScope, $scope,$modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'verticalcontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/vertical/vertical';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.vertical = resp.data.vertical_list;
                unlockUI();               
            });
     
        }

        $scope.editRule = function (vertical_gid) {
            $location.url('app/MstVerticalApplicantTypeRule?lsvertical_gid=' + vertical_gid);
        }
       
       $scope.delete = function (vertical_gid) {
                var params = {
                    vertical_gid: vertical_gid
                }
                var url = 'api/vertical/verticalDelete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    SweetAlert.swal({
                        title: 'Are you sure?',
                        text: 'Do You Want To Delete the Record ?',
                        showCancelButton: true,
                        confirmButtonColor: '#DD6B55',
                        confirmButtonText: 'Yes, delete it!',
                        closeOnConfirm: false
                    }, function (isConfirm) {
                        if (isConfirm) {
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                            activate();
                        }

                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    activate();
                }
            });
            };
        
        $scope.popupvertical = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {             
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var url = 'api/entity/Entity';
                SocketService.get(url).then(function (resp) {
                    $scope.entity_list = resp.data.entity_list;
                });
                $scope.verticalSubmit = function () {
                    var params = {
                        bureau_code: $scope.txtbureau_code,
                        vertical_name: $scope.txtvertical_name,
                        vertical_code:$scope.txtvertical_code,
                        lms_code: $scope.txtlms_code,
                        entity_gid: $scope.cboentity_type.entity_gid,
                        entity_name: $scope.cboentity_type.entity_name,
                        vertical_refno: $scope.txtvertical_refno,
                    }
                  
                    var url = 'api/vertical/createVertical';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')
                             activate();
                        
                          }
                        else {
                            Notify.alert(resp.data.message, 'warning')
                            
                        }
                        activate();
                    });
                    $modalInstance.close('closed');
                  
                }
                
            }
        }

       

        $scope.edit = function (vertical_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
                var params = {
                    vertical_gid: vertical_gid
                }
                var url = 'api/vertical/Getverticalupdate';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.bureau_codeedit = resp.data.bureau_codeedit;
                    $scope.lms_codeedit = resp.data.lms_codeedit;
                    $scope.vertical_codeedit = resp.data.verticalCodeedit;
                    $scope.verticalNameedit = resp.data.verticalNameedit;
                    $scope.vertical_gid = resp.data.vertical_gid;
                    $scope.cboentity_type = resp.data.entity_gid;
                    $scope.txtvertical_refno = resp.data.vertical_refno;
                     });
                var url = 'api/entity/Entity';
                SocketService.get(url).then(function (resp) {
                    $scope.entity_list = resp.data.entity_list;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.verticalUpdate = function () {
                    var entity_name = $('#entity_name :selected').text();
                    var params = {
                        bureau_codeedit: $scope.bureau_codeedit,
                        lms_codeedit: $scope.lms_codeedit,
                        verticalCodeedit:$scope.vertical_codeedit,
                        verticalNameedit: $scope.verticalNameedit,
                        vertical_gid: $scope.vertical_gid,
                        entity_gid: $scope.cboentity_type,
                        entity_name: entity_name,
                        vertical_refno: $scope.txtvertical_refno,
                    }
                    var url = 'api/vertical/verticalUpdate';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')

                        }
                        else {
                            Notify.alert(resp.data.message, 'success')
                            activate();

                        }
                    });
                }
            }

        }

        $scope.Status_update = function (vertical_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCompanyDocument.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    vertical_gid: vertical_gid
                }
            var url = 'api/vertical/Getverticalupdate';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.bureau_codeedit = resp.data.bureau_codeedit;
                $scope.lms_codeedit = resp.data.lms_codeedit;
                $scope.vertical_codeedit = resp.data.verticalCodeedit;
                $scope.lblvertical_name = resp.data.verticalNameedit;
                $scope.rbo_status = resp.data.status_log;
                $scope.txtvertical_refno = resp.data.vertical_refno;
            });
            var params = {
                vertical_gid: vertical_gid
            }
             var url = 'api/vertical/GetActiveLog';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        vertical_name: $scope.lblvertical_name,
                        remarks: $scope.txtremarks,
                        status_log: $scope.rbo_status,
                        vertical_gid: vertical_gid
                    }
                    var url = 'api/vertical/VerticalStatusUpdate';
                      lockUI();
                     SocketService.post(url, params).then(function (resp) {
                         unlockUI();
                         if (resp.data.status == true) {
 
                             Notify.alert(resp.data.message, {
                                 status: 'success',
                                 pos: 'top-center',
                                 timeout: 3000
                             });
                             activate();
                         }
                         else {
                             Notify.alert(resp.data.message, {
                                 status: 'info',
                                 pos: 'top-center',
                                 timeout: 3000
                             });
                         }
                     }); 

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.editTemplate = function (vertical_gid) {
            $location.url('app/MstTemplateSummary?lsvertical_gid=' + vertical_gid);
        }

        $scope.editSanctionTemplate = function (vertical_gid) {
            $location.url('app/MstSanctionTemplateSummary?lsvertical_gid=' + vertical_gid);
        }

        $scope.document_deferral = function (vertical_gid) {
            $location.url('app/MstDisbursementDeferralDocument?vertical_gid=' + vertical_gid);
        }
        $scope.disbursement_document = function (vertical_gid) {
            $location.url('app/MstVerticalDisbursementDocument?vertical_gid=' + vertical_gid);
        }
        $scope.disbursement_bankaccount = function (vertical_gid) {
            $location.url('app/MstDisbursementBankAccount?vertical_gid=' + vertical_gid);
        }
        $scope.disbursement_odbelow30 = function (vertical_gid) {
            $location.url('app/MstDisbursementODBelow30?vertical_gid=' + vertical_gid);
        }
        $scope.disbursement_odbelow90 = function (vertical_gid) {
            $location.url('app/MstDisbursementODBelow90?vertical_gid=' + vertical_gid);
        }
        $scope.penal_waiver = function (vertical_gid) {
            $location.url('app/MstPenalWaiver?vertical_gid=' + vertical_gid);
        }

        $scope.editContrateTemplate = function (vertical_gid) {
            $location.url('app/AgrMstContrateTemplateSummary?lsvertical_gid=' + vertical_gid);
        }
       }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewDeferral', viewDeferral);

    viewDeferral.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService'];

    function viewDeferral($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewDeferral';

        activate();
        function activate() {
            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/Getcaddoc';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadDocumentname = resp.data;
                $scope.deferrals = resp.data.deferralSummaryDtls;
                $scope.deferral_gid = resp.data.deferral_gid;
                $scope.loanref_no = resp.data.loanref_no;
                $scope.loan_title = resp.data.loan_title;
                $scope.record_id = resp.data.record_id;
                $scope.deferral_name = resp.data.deferral_name;
                $scope.approval_remarks = resp.data.approval_remarks;
                $scope.approval_status = resp.data.approval_status;
                $scope.filename_list = resp.data.filename_list;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.businesshead_name = resp.data.businesshead_name;
                $scope.rm_name = resp.data.rm_name;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.entity_name = resp.data.entity_name;
                $scope.branch_name = resp.data.branch_name;
                $scope.customer_code = resp.data.customer_code;
                $scope.cluster_manager_name = resp.data.cluster_manager_name;
            });
            // Close Modals


            $scope.close = function (val) {
                document.getElementById("userform").reset();
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }

            //$scope.upload = function (val, val1, name) {
            //    var item = {
            //        name: val[0].name,
            //        file: val[0]
            //    };
            //    var frm = new FormData();
            //    frm.append('fileupload', item.file);
            //    frm.append('file_name', item.name);
            //    frm.append('document_name', $scope.documentname);
            //    frm.append('deferral_gid', $scope.deferral_gid);
            //    frm.append('loan_gid', $scope.loan_gid);
            //    frm.append('project_flag', "Default");
            //    $scope.uploadfrm = frm;
            //    var url = 'api/deferral/UploadcadDocument';
            //    lockUI();
            //    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
            //        unlockUI();
            //        $scope.filename_list = resp.data.filename_list;

            //        $("#addupload").val('');

            //        if (resp.data.status == true) {
            //            activate();
            //            Notify.alert('Document Uploaded Successfully..!!', 'success')
            //            var modalInstance = $modal.open({
            //                templateUrl: '/UploadDocument.html',
            //                controller: ModalInstanceCtrl,
            //                size: 'md'
            //            });
            //        }
            //        else {
            //            unlockUI();
            //            Notify.alert('File Format Not Supported!')
            //        }
            //        activate();
            //    });

            //}
            $scope.downloads = function (val1, val2) {
                //var phyPath = val1;
                //var relPath = phyPath.split("StoryboardAPI");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                ////console.log(str);
                //var link = document.createElement("a");
                //var name = val2.split('.');
                //link.download = val2;
                //var uri = str;
                //link.href = uri;
                //link.click();
                DownloaddocumentService.Downloaddocument(val1, val2);

            }

        }

        $scope.deferralback = function (val) {
            $state.go('app.DeferralManagement');
        }
         


    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewloan2deferralDetails', viewloan2deferralDetails);

    viewloan2deferralDetails.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function viewloan2deferralDetails($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewloan2deferralDetails';

        activate();
        function activate() {
            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            var params = {
                deferral_gid: $scope.deferral_gid
            }

            var url = 'api/deferral/Getcaddoc';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadDocumentname = resp.data;
                $scope.deferrals = resp.data.deferralSummaryDtls;
                $scope.deferral_gid = resp.data.deferral_gid;
                $scope.loanref_no = resp.data.loanref_no;
                $scope.loan_title = resp.data.loan_title;
                $scope.record_id = resp.data.record_id;
                $scope.deferral_name = resp.data.deferral_name;
                $scope.approval_remarks = resp.data.approval_remarks;
                $scope.approval_status = resp.data.approval_status;
                $scope.filename_list = resp.data.filename_list;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.businesshead_name = resp.data.businesshead_name;
                $scope.rm_name = resp.data.rm_name;
                $scope.credit_manager = resp.data.credit_manager;
                $scope.customer_code = resp.data.customer_code;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.entity_name = resp.data.entity_name;
                $scope.branch_name = resp.data.branch_name;
                $scope.cluster_manager_name = resp.data.cluster_manager_name;
                //console.log(resp.data.cluster_manager_name);
            });


            // Close Modals
               $scope.close = function (val) {
                document.getElementById("userform").reset();
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }

            $scope.upload = function (val, val1, name) {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('deferral_gid', $scope.deferral_gid);
                frm.append('loan_gid', $scope.loan_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/deferral/UploadcadDocument';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.filename_list = resp.data.filename_list;

                    $("#addupload").val('');

                    if (resp.data.status == true) {
                        activate();
                        Notify.alert('Document Uploaded Successfully..!!', 'success')
                        var modalInstance = $modal.open({
                            templateUrl: '/UploadDocument.html',
                            controller: ModalInstanceCtrl,
                            size: 'md'
                        });
                    }
                    else {
                        Notify.alert('File Format Not Supported!')
                    }
                    activate();
                });

            }
            $scope.downloads = function (val1, val2) {
                //var phyPath = val1;
                //var relPath = phyPath.split("StoryboardAPI");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                ////console.log(str);
                //var link = document.createElement("a");
                //var name = val2.split('.');
                //link.download = val2;
                //var uri = str;
                //link.href = uri;
                //link.click();
                DownloaddocumentService.Downloaddocument(val1, val2);

            }

        }

        $scope.deferralback = function (val) {
            //$scope.loan_gid = val;
            //$scope.loan_gid = localStorage.setItem('loan_gid', val);
            $state.go('app.loan2deferral');
        }



    }
})();
