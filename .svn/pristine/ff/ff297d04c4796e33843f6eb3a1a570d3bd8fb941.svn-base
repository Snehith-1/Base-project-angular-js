(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrCadConsolidatedOriginalCopyVettingReportController', AgrCadConsolidatedOriginalCopyVettingReportController);

    AgrCadConsolidatedOriginalCopyVettingReportController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrCadConsolidatedOriginalCopyVettingReportController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrCadConsolidatedOriginalCopyVettingReportController';

        activate();

        function activate() {
            lockUI();
            $scope.total=0;
            $scope.totalDisplayed=100;
            if ( $scope.page ==undefined)
            {
                localStorage.setItem('page','workitemsummary')
            }
            $scope.page = localStorage.getItem('page');
            var url = 'api/AgrMstApplicationReport/CADConsolidatedReportCount';
            SocketService.get(url).then(function (resp) {
                $scope.SanctionCount = resp.data.SanctionCount;
            /*    $scope.LSACount = resp.data.LSACount;*/
                $scope.DocumentCheckListCount = resp.data.DocumentCheckListCount;
                $scope.SoftcopyVettingCount = resp.data.SoftcopyVettingCount;
                $scope.OriginalCopyVettingCount = resp.data.OriginalCopyVettingCount;


            });
         
            var url = 'api/AgrMstApplicationReport/GetConsolidatedOriginalCopyVettingReport';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.assignment_list = resp.data.assignment_list;
                //if ($scope.assignment_list == null) {
                //    $scope.total = 0;
                //    $scope.totalDisplayed = 0;

                //}
                //else {
                //    $scope.total = $scope.assignment_list.length;
                //    if ($scope.assignment_list.length < 100) {
                //        $scope.totalDisplayed = $scope.assignment_list.length;

                //    }
                //}
            });
        }


        $scope.Sanction = function () {
            $state.go('app.AgrCadConsolidatedSanctionReport');
        }
        $scope.LSA = function () {
            $state.go('app.AgrCadConsolidatedLSAReport');
        }
        $scope.DocumentChecklist = function () {
            $state.go('app.AgrCadConsolidatedDocumentChecklistReport');
        }
        $scope.SoftcopyVetting = function () {
            $state.go('app.AgrCadConsolidatedSoftcopyVettingReport');
        }
        $scope.OriginalCopyVetting = function () {
            $state.go('app.AgrCadConsolidatedOriginalCopyVettingReport');
        }
       
            
        $scope.EmployeeProfile = function (emp_gid) {
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params = {
                employee_gid: emp_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.user_code = resp.data.user_code;
                    $scope.user_name = resp.data.user_name;
                    $scope.user_photo = resp.data.user_photo;
                    $scope.user_designation = resp.data.user_designation;
                    $scope.user_department = resp.data.user_department;
                    $scope.user_mobileno = resp.data.user_mobileno;
                }
                else {
                    $scope.user_code = "-";
                    $scope.user_name = "-";
                    $scope.user_photo = "N";
                    $scope.user_designation = "-";
                    $scope.user_department = "-";
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
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };
        // Action Work Item 360
        $scope.WorkItem360=function(val){
            localStorage.setItem('email_gid',val)
            var params={
                email_gid:val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url,params).then(function (resp) {
            });
            if ($scope.page ==undefined)
            {
                localStorage.setItem('page','workitemsummarypage')
            }
            else{
                localStorage.setItem('page',$scope.page)
            }
           
            $state.go("app.iasnTrnWorkItem360");
        }

        // Action Work Item Allotted 360
        $scope.WorkItemAllotted360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', $scope.page)
            }

            $state.go("app.iasnTrnWorkItemAllotted360");
        }

        $scope.Allotted=function(){
            var url = 'api/IasnTrnWorkItem/WorkItemSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemAllotted_List = resp.data.MdlWorkItem;
              
                if ($scope.WorkItemAllotted_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemAllotted_List.length;
                    if ($scope.WorkItemAllotted_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemAllotted_List.length;
                    }
                }
            });

        }
        
        $scope.WorkItem=function(){
            var url = 'api/IasnTrnWorkItem/WorkItemPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });
        }

        $scope.Pushback=function(){
            var url = 'api/IasnTrnWorkItem/WorkItemPushbackSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPushback_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPushback_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPushback_List.length;
                    if ($scope.WorkItemPushback_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPushback_List.length;
                    }
                }

            });
        }

        $scope.Forward=function(){
            var url = 'api/IasnTrnWorkItem/WorkItemForwardSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemForward_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemForward_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemForward_List.length;
                    if ($scope.WorkItemForward_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemForward_List.length;
                    }
                }

            });
        }

        $scope.CloseTab=function(){
            var url = 'api/IasnTrnWorkItem/WorkItemCloseSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemClose_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemClose_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemClose_List.length;
                    if ($scope.WorkItemClose_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemClose_List.length;
                    }
                }

            });
        }

       
        

        $scope.archivalWI = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/archivalContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {

                $scope.onclickspecific = function () {
                    $scope.PnlSpecific = true;
                    $scope.customer = "";
                    $scope.cbosanctionrefno = "";
                }
                $scope.onclickcustomer = function () {
                    $scope.PnlSpecific = false;
                    $scope.customer = "";

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
                                $scope.customer = "";
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

                    $scope.customer = customer_name;
                    $scope.customer_gid = customer_gid;
                    $scope.customer_list = null;

                    var params = {
                        customer_gid: customer_gid
                    }


                    var url = 'api/loan/customer_getheads';

                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.sanctiondtl = resp.data.sanctiondtl;

                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.ArchivalSubmit = function () {
                    var sanctionref_no = '';
                    var sanction_gid = '';

                    if ($scope.archival.types_of_archival == 'Specific') {
                        if ($scope.cbosanctionrefno == undefined) {
                            modalInstance.close('closed');
                            Notify.alert('Select the Sanction Ref No.', 'warning');
                            return;
                        }
                        else {
                            sanctionref_no = $('#sanction :selected').text();
                            sanction_gid = $scope.cbosanctionrefno.sanction_Gid;
                        }
                    }


                    var params = {
                        composemail_gid: val,
                        archival_type: $scope.archival.types_of_archival,
                        remarks: $scope.archival.Remarks,
                        customer_gid: $scope.customer_gid,
                        customer_name: $scope.customer,
                        sanctionref_no: sanctionref_no,
                        sanction_gid: sanction_gid,
                        status: "Archival"
                    }
                    var url = 'api/IasnTrnWorkItem/ComposeMailDecision';
                    lockUI()
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')

                        }
                        else {

                            modalInstance.close('closed');
                            Notify.alert(resp.data.message)
                        }
                        activate();
                        $state.go("app.iasnWomWorkOrderSummary");
                    });

                }
            }
        }

        $scope.ComposeMail360 = function (val) {
            localStorage.setItem('composemail_gid', val)
            $state.go('app.iasnTrnComposeMail360');
        }

        $scope.forward = function (val, val1, val2, val3, val4) {
            $scope.ccMail = $scope.cc_mail;
            localStorage.setItem('composemail_gid', val);
            localStorage.setItem('toMail', val2);
            localStorage.setItem('ccMail', val3);
            localStorage.setItem('bccMail', val4);
            localStorage.setItem('email_subject', val1);
            localStorage.setItem('decision', 'Forward');
            localStorage.setItem('lspage', 'composemailsummary');
            $state.go('app.iasnTrnForwardMail');
        }
    }
})();
