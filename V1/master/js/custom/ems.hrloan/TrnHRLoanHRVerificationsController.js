(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRVerificationsController', TrnHRLoanHRVerificationsController);

    TrnHRLoanHRVerificationsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function TrnHRLoanHRVerificationsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRVerificationsController';       
        $scope.request_gid = cmnfunctionService.decryptURL($location.search().hash).request_gid;
        var request_gid = $scope.request_gid;
      
       
        lockUI();
        activate();

        function activate() { 
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };

           

            // Calender Popup... //                   
            vm.formats = ['MM-yyyy'];
            vm.format = 'MMM - yyyy';
            vm.dateOptions = {
                //formatYear: 'yy',
                //startingDay: 1
               
                minMode: 'month'
              

            };

            $scope.checkErr = function (approvedtenure_startdate, approvedtenure_enddate) {
                $scope.errMessage = '';
                var curDate = new Date();

                if (new Date(approvedtenure_startdate) > new Date(approvedtenure_enddate)) {
                    $scope.errMessage = 'End month should be greater than start month';
                    $scope.approvedtenure_startdate = '';
                    $scope.approvedtenure_enddate = ''
                    return false;
                }
                //if (new Date(startDate) < curDate) {
                //    $scope.errMessage = 'Start date should not be before today.';
                //    return false;
                //}
            };
                   
            lockUI();
            gettcflag();
            var params = {
                request_gid: $scope.request_gid,
            }

            var url = 'api/MstHRLoanDrmApproval/DaGetHRloanRequestviewDetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lblapplref_no = resp.data.request_refno;
                $scope.lblEmployee_name = resp.data.employee_name;
                $scope.lblRole = resp.data.employee_role;
                $scope.lblDepartment = resp.data.department_name;
                $scope.lblDRM = resp.data.reporting_mgr;
                $scope.lblFH = resp.data.functional_head;
                $scope.lblHR = resp.data.hr_head;
                $scope.cbofintype = resp.data.fintype_name;
                $scope.cbopurpose = resp.data.purpose_name;
                $scope.cboseverity = resp.data.severity_name;
                $scope.cbotenure = resp.data.tenure;
                $scope.txtamount = resp.data.amount;
                $scope.request_status = resp.data.request_status; 
                $scope.txtinterest = resp.data.interest;
                $scope.txtentityname = resp.data.entity_name;

                if(resp.data.request_status== "HRVerify Pending"){
                    $scope.txtapprovedinterest = resp.data.interest;
                    $scope.cboapprovedtenure = resp.data.tenure; 
                }
                else if(resp.data.request_status== "HR Approved"){
                    $scope.txtapprovedinterest = resp.data.interest;
                    $scope.cboapprovedtenure = resp.data.tenure;                       
                }
                else{
                    $scope.txtapprovedinterest = resp.data.approved_interest;
                    $scope.cboapprovedtenure = resp.data.approved_tenure;
                    $scope.approvedtenure_startdate = resp.data.approvedtenure_startdate;
                    $scope.approvedtenure_enddate = resp.data.approvedtenure_enddate;
                }
              
                unlockUI();
            });
            
            var url = 'api/TrnHRLoanHRVerifications/GetHRLoanDropDown';
            SocketService.get(url).then(function (resp) {                
                $scope.lshrtermsandconditions_list = resp.data.hrtermsandconditions_list;
                $scope.hrdocumentname_list = resp.data.hrdocumentname_list;

            });
            var url = 'api/TrnHRLoanHRVerifications/TempDocumentsList';
            SocketService.get(url).then(function (resp) {
            });
            var param = {
                request_gid: request_gid,
            }
            var url = 'api/TrnHRLoanHRVerifications/GetUploadList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.HRDocument_list = resp.data.HRDocument_list;
                });
            var url = 'api/MstHRLoanDrmApproval/GetUploadDocumentsList';
            SocketService.getparams(url, params).then(function (resp) {
               $scope.hrReuploadDocument_list = resp.data.HrReuploadDocument_list;
                });           

            var url = 'api/TrnHRLoanHRVerifications/GetManagerRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mangraisequery_list = resp.data.mangraisequery_list;
            });
            
            lockUI();
            var url = 'api/MstHRLoanHRMappingApprovals/GetManagerName';
            SocketService.get(url).then(function (resp) {
                unlockUI();                  
                $scope.lblemployee_name = resp.data.employee_name;                
            });
            var params =
                {
                    request_gid: $scope.request_gid
                }
            var url = 'api/TrnHRLoanHRVerifications/HRLoanPaymentDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.PaymentDocument_list = resp.data.PaymentDocument_list;
                });
                var url = 'api/MstHRLoanDrmApproval/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.Approvalsummary;
            });      
            
        }

        $scope.checkDate = function (startDate, endDate) {
            $scope.errMessage = '';          

            if (new Date(startDate) > new Date(endDate)) {
                $scope.errMessage = 'End Date should be greater than start date';
                $scope.approvedtenure_startdate = '';
                $scope.approvedtenure_enddate= ''
                return false;
            }
           
        };        
        function gettcflag(){
            var param= {
                request_gid: $scope.request_gid
            }
            var url = 'api/TrnHRLoanHRVerifications/GetTCflag';
            lockUI();
            SocketService.getparams(url,param).then(function (resp) {
                $scope.tc_flag = resp.data.tc_flag;
                unlockUI();
            });
        }

        // $scope.Back = function () {
        //     if (lspage == 'HRVerifications') {
        //         $state.go('app.TrnHRLoanHRVerificationsSummary');
        //     }
        //     else if (lspage == 'HRApproved') {
        //         $state.go('app.TrnHRLoanHRVerificationsApprovedSummmary');
        //     } 
        //     else if (lspage == 'HRReject') {
        //         $state.go('app.TrnHRLoanHRVerificationsRejectedSummary');
        //     }           
        //     else {
               
        //     } 
        // }

        //Document Multiple Add
       
        $scope.doc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
            unlockUI();
            return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

        $scope.UploadhrDocument = function (val, val1, name) {
            if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.cbodocumentname == null) || ($scope.cbodocumentname == '') || ($scope.cbodocumentname == undefined)) {
                $("#hrfile").val('');
                Notify.alert('Kindly Enter the Document Title/ID', 'warning');
            }
            else {
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
                frm.append('document_title', $scope.cbodocumentname.hrdocument_name);
                frm.append('hrdocument_name', $scope.cbodocumentname.hrdocument_gid);
                frm.append('document_id', $scope.txtdocument_id);
                frm.append('request_gid',$scope.request_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/TrnHRLoanHRVerifications/HRLoanDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.HRDocument_list = resp.data.HRDocument_list;
                        unlockUI();
                        $("#hrfile").val('');
                        $scope.cbodocumentname = "";
                        $scope.txtdocument_id = "";
                        $scope.uploadfrm = undefined;
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            var params =
                            {
                                request_gid: $scope.request_gid
                            }
                            var url = 'api/TrnHRLoanHRVerifications/HRLoanDocumentList';
                                    SocketService.getparams(url, params).then(function (resp) {
                                        $scope.HRDocument_list = resp.data.HRDocument_list;
                                    });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
            }
        }
        $scope.delete_hrspcldocument = function (hrspecialdocument_gid) {
            lockUI();
            var params = {
                hrspecialdocument_gid: hrspecialdocument_gid
            }
            var url = 'api/TrnHRLoanHRVerifications/UploadDocumentsDelete';
            SocketService.getparams(url, params).then(function (resp) {
              $scope.HRDocument_list = resp.data.HRDocument_list;
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var param =
                         {
                            request_gid: request_gid
                         }
                    var url = 'api/TrnHRLoanHRVerifications/GetUploadList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.HRDocument_list = resp.data.HRDocument_list;
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                 
                }
                unlockUI();
            });
        }

        $scope.allChecked = function () {
            return $scope.termsandcont_list.filter(function (obj) { return obj.checked; }).length === $scope.termsandcont_list.length;
        //    return $scope.lshrtermsandconditions_list.filter(obj => obj.checked).length == $scope.lshrtermsandconditions_list.length;
        }
      
        $scope.checkAll = function (selected) {
            angular.forEach($scope.lshrtermsandconditions_list, function (val) {
                val.checked = $scope.selectAll;

            });
        };

        $scope.checkall = function (selected) {
            angular.forEach($scope.lshrtermsandconditions_list, function (val) {
                val.checked = selected;
            });
        }
                     
        $scope.tremsandcondtn = function () {
          
            var hrloantermsandconditions_gid;
            var termsandconditionslistGId = [];
           
            angular.forEach($scope.lshrtermsandconditions_list, function (val){

                if (val.checked == true) {
                    
                    var termsandconditionslist_gid = val.hrloantermsandconditions_gid;
                    hrloantermsandconditions_gid = val.hrloantermsandconditions_gid;
                    termsandconditionslistGId.push(termsandconditionslist_gid);
                  
                }

            });
            if (($scope.cbofintype != 'Salary Advance' && $scope.PaymentDocument_list ==null)) {
                Notify.alert('Please Select Import Excel Document!  ', 'warning')
            }
            else {


                var params = {
                    request_gid: $scope.request_gid,
                    termsandconditionslist_gid: termsandconditionslistGId,
                    approved_interest: $scope.txtapprovedinterest,
                    approved_tenure: $scope.cboapprovedtenure,
                    approvedtenure_startdate: $scope.approvedtenure_startdate,
                    approvedtenure_enddate: $scope.approvedtenure_enddate

                }

                if (hrloantermsandconditions_gid != undefined) {
                    var url = 'api/TrnHRLoanHRVerifications/PostHrLoantermsandcondtn';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $state.go('app.TrnHRLoanHRVerificationsSummary');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });
                }

                else {
                    Notify.alert('Select Atleast One Terms and Conditions!', 'warning')
                }

            }
        }
       
        $scope.hrverify_approve = function () {
       
            var params = {

                hrverify_remarks: $scope.txtverify_remarks,
                request_gid: $scope.request_gid,
                                
            }
            var url = 'api/TrnHRLoanHRVerifications/PostHrLoanHRVerifyApprovalUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.TrnHRLoanHRVerificationsSummary');
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });

     }

     $scope.hrverify_reject = function () {

         var params = {

            hrverify_remarks: $scope.txtverify_remarks,
            request_gid: $scope.request_gid,
         }
         var url = 'api/TrnHRLoanHRVerifications/PostHrLoanHRVerifyRejectUpdate';
         lockUI();
         SocketService.post(url, params).then(function (resp) {
             unlockUI();
             if (resp.data.status == true) {

                 Notify.alert(resp.data.message, {
                     status: 'success',
                     pos: 'top-center',
                     timeout: 3000
                 });
                 $state.go('app.TrnHRLoanHRVerificationsSummary');
                 activate();
             }
             else {
                 Notify.alert(resp.data.message, {
                     status: 'warning',
                     pos: 'top-center',
                     timeout: 3000
                 });
             }

         });

     }
     $scope.verify = function () {
       
        var params = {

            hrdocverify_remarks: $scope.txtverify_remarks,
            request_gid: $scope.request_gid,                 
        }
        var url = 'api/TrnHRLoanHRVerifications/PostHrLoanVerifyApprovalUpdate';
        lockUI();
        SocketService.post(url, params).then(function (resp) {
            unlockUI();
            if (resp.data.status == true) {

                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtverify_remarks = '';
                $state.go('app.TrnHRLoanHRVerifications');
                activate();
            }
            else {
                Notify.alert(resp.data.message, {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }

        });
     }

     $scope.create_raisequery = function () {
        var modalInstance = $modal.open({
            templateUrl: '/raisequery.html',
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
            
            $scope.raisequery_add = function () {                  
               var params = {
                    query_title: $scope.txtquery_title,
                    query_description: $scope.txtquery_desc,                                          
                    request_gid: request_gid,                       
                    
                }
                var url = 'api/TrnHRLoanHRVerifications/PostManagerRaiseQuery';               
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

     $scope.view_mangquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
        var modalInstance = $modal.open({
            templateUrl: '/mangqueryDescriptionView.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'lg'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
            $scope.lblquery_desc = query_description;
            $scope.lblqueryresponse_remarks = queryresponse_remarks;
            $scope.lblquery_responseby = query_responseby;
            $scope.ok = function () {
                $modalInstance.close('closed');
            };
        }
    } 

    $scope.UploadDocument = function (val, val1, name) {      
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
            frm.append('request_gid',$scope.request_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/TrnHRLoanHRVerifications/HRLoanPaymentDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.PaymentDocument_list = resp.data.PaymentDocument_list;
                    unlockUI();
                    $("#Paymentfile").val('');                  
                    $scope.uploadfrm = undefined;
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var params =
                        {
                            request_gid: $scope.request_gid
                        }
                        var url = 'api/TrnHRLoanHRVerifications/HRLoanPaymentDocumentList';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.PaymentDocument_list = resp.data.PaymentDocument_list;
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    unlockUI();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }
      
    }
    $scope.delete_document = function (hrrepaymentdocument_gid) {
        lockUI();
        var params = {
            hrrepaymentdocument_gid: hrrepaymentdocument_gid
        }
        var url = 'api/TrnHRLoanHRVerifications/UploadPaymentDocumentsDelete';
        SocketService.getparams(url, params).then(function (resp) {
          $scope.PaymentDocument_list = resp.data.PaymentDocument_list;
            if (resp.data.status == true) {

                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });
                var param =
                     {
                        request_gid: request_gid
                     }
                var url = 'api/TrnHRLoanHRVerifications/HRLoanPaymentDocumentList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.PaymentDocument_list = resp.data.PaymentDocument_list;
                });
            }
            else {
                Notify.alert(resp.data.message, {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
             
            }
            unlockUI();
        });
    }
        $scope.Back = function () {
            $state.go('app.TrnHRLoanHRVerificationsSummary');
        }
       

    }
})();





