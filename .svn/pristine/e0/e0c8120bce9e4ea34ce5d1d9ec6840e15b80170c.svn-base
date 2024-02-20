(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanEditRequestController360', MstHRLoanEditRequestController360);

    MstHRLoanEditRequestController360.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstHRLoanEditRequestController360($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanEditRequestController360';

        var lsFunctionalhead_gid, lsdepartment_gid, lsreportingmgr_gid, lsentity_gid;
        $scope.request_gid = cmnfunctionService.decryptURL($location.search().hash).request_gid;
        var request_gid = $scope.request_gid;
        var hrreqdocument_gid = $scope.hrreqdocument_gid;

        activate();
        function activate() {
            // var url = 'api/MstHRLoanRequest/tempdelete';
            // SocketService.get(url).then(function (resp) {
            // });
            // var url = 'api/MstHRLoanRequest/GetHRloanDetails';
            // lockUI();
            // SocketService.get(url).then(function (resp) {
            //     $scope.hrloanrequestSummary_list = resp.data.hrloanrequest;               
            //     unlockUI();
            // });

            var url = 'api/MstHRLoanRequest/GetFinType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.fintype_list = resp.data.fintype_list;
                unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetSeverity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.severity_list = resp.data.severity_list;
                unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetPurpose';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.purpose_list = resp.data.purpose_list;
                unlockUI();
            });

            var url = 'api/MstHRLoanRequest/GetEmployeeDetails';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.txtemp = resp.data.employee_name;
                $scope.txtrole = resp.data.role;
                $scope.txtdept = resp.data.department;

                $scope.txtofficialmail = resp.data.official_mailid;
                $scope.txtofficialmobile = resp.data.official_mobileno;
                $scope.txtpersonalmail = resp.data.pers_mailid;
                $scope.txtpersonalmobile = resp.data.pers_mobileno;
                $scope.txtentityname = resp.data.entity_name;
                lsentity_gid = resp.data.entity_gid;

                $scope.txtreporting_manager = resp.data.reporting_manager;
                $scope.txtfunctional_head = resp.data.functional_head;
                lsFunctionalhead_gid = resp.data.functionalhead_gid;
                lsdepartment_gid = resp.data.department_gid;
                lsreportingmgr_gid = resp.data.reportingmgr_gid;


                unlockUI();
            });
            var params = {
                request_gid: request_gid
            }
            var url = 'api/MstHRLoanDrmApproval/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.Approvalsummary = resp.data.Approvalsummary;
            });
            var params = {
                request_gid: request_gid
            }
            var url = 'api/MstHRLoanApprovalsApproved/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.ApprovedApprovalsummary;
            });
            var params = {
                request_gid: request_gid
            }
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.RejectedApprovalsummary;
            });
            var params = {
                request_gid: request_gid
            }
            var url = 'api/MstHRLoanRequest/EditHRLoanRequest';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.fintype = resp.data.fintype_name;
                $scope.amount = resp.data.amount;
                $scope.purpose = resp.data.purpose_name;
                $scope.severity = resp.data.severity_name;
                $scope.tenure = resp.data.tenure;
                $scope.reason = resp.data.request_reason;
                $scope.txtinterest = resp.data.interest;
                document.getElementById('selectNote').innerHTML = resp.data.purpose_note;

                unlockUI();
            });
            var url = 'api/TrnHRLoanHRVerifications/HRLoanGetApprovedInterest';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.approvedinterest = resp.data.approved_interest;
                $scope.approvedtenure = resp.data.approved_tenure;
                $scope.approvedtenure_startdate = resp.data.approvedtenure_startdate;
                $scope.approvedtenure_enddate = resp.data.approvedtenure_enddate;

            });
            var params =
            {
                request_gid: request_gid
            }
            var url = 'api/TrnHRLoanHRVerifications/termsandcondtnview';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.termsandcont_list = resp.data.hrtermsandconditions_list;
            });
            var url = 'api/MstHRLoanRequest/GetUploadDocumentsList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.upload_list = resp.data.upload_list;
            });

            var url = 'api/MstHRLoanRequest/GetHRLoanStatusRequest';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.request_status = resp.data.request_status;
                $scope.raisequery_status = resp.data.raisequery_status;
            });

            var url = 'api/MstHRLoanDrmApproval/GetDRMRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.drmraisequery_list = resp.data.drmraisequery_list;
            });

            var url = 'api/MstHRLoanDrmApproval/GetFHRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.fhraisequery_list = resp.data.fhraisequery_list;
            });

            var url = 'api/MstHRLoanDrmApproval/GetHRRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.hrraisequery_list = resp.data.hrraisequery_list;
            });
            var url = 'api/TrnHRLoanHRVerifications/GetManagerRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mangraisequery_list = resp.data.mangraisequery_list;
            });
            var url = 'api/MstHRLoanDrmApproval/GetUploadDocumentsList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.hrReuploadDocument_list = resp.data.HrReuploadDocument_list;
            });
            var url = 'api/MstHRLoanDrmApproval/GetDropDown';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.applicationadddoc_list = resp.data.hrdocument_list;
            });
            var param =
            {
                request_gid: request_gid
            }
            var url = 'api/TrnHRLoanHRVerifications/GetUploadList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.HRDocument_list = resp.data.HRDocument_list;
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
     

        //  Document Multiple Add
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

        //$scope.download = function (val1, val2) {
        //          DownloaddocumentService.Downloaddocument(val1, val2);
        //      } 
        $scope.download = function () {
            var filename = "Salary Advance form.pdf";
            //var phyPath = resp.data.file_path;
            var phyPath = "F:\\Web\\EMS\\templates\\Salary Advance form.pdf";
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = "http://"
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = filename.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }
        $scope.download1 = function () {
            var filename = "Employee Loan Application Form.pdf";
            //var phyPath = resp.data.file_path;
            var phyPath = "F:\\Web\\EMS\\templates\\Employee Loan Application Form.pdf";
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = "http://"
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = filename.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        //$scope.download1 = function (val1, val2) {
        //          DownloaddocumentService.Downloaddocument(val1, val2);
        //      } 

        $scope.download_allenquiry = function () {
            for (var i = 0; i < $scope.hrReuploadDocument_list.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument($scope.hrReuploadDocument_list[i].document_path, $scope.hrReuploadDocument_list[i].document_name);
            }
        }

        $scope.download_all = function () {
            for (var i = 0; i < $scope.PaymentDocument_list.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument($scope.PaymentDocument_list[i].document_path, $scope.PaymentDocument_list[i].document_name);
            }
        }

        $scope.UploadHrDocument = function (val, val1, name) {
            if (($scope.cbodocument_id == null) || ($scope.cbodocument_id == '') || ($scope.cbodocument_id == undefined) || ($scope.cbodocumentname == null) || ($scope.cbodocumentname == '') || ($scope.cbodocumentname == undefined)) {
                $("#hrloanfile").val('');
                Notify.alert('Kindly Enter the Document Title/ID', 'warning');
            }
            else {

                var frm = new FormData();
                for (var i = 0; i < val.length; i++) {
                var item = {
                name: val[i].name,
                file: val[i]
            };
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
        }
                // var item = {
                //     name: val[0].name,
                //     file: val[0]
                // };
                // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                // if (IsValidExtension == false) {
                //     Notify.alert("File format is not supported..!", {
                //         status: 'danger',
                //         pos: 'top-center',
                //         timeout: 3000
                //     });
                //     return false;
                // }
                // var frm = new FormData();


                // $scope.uploadfrm = frm;
                // var frm = new FormData();
                // frm.append('fileupload', item.file);
                // frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cbodocumentname.hrdocumentlist_name);
                frm.append('hrdocument_name', $scope.cbodocumentname.hrdocumentlist_gid);
                frm.append('document_id', $scope.cbodocument_id);
                frm.append('request_gid', $scope.request_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstHRLoanDrmApproval/HRLoanReUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.hrReuploadDocument_list = resp.data.HrReuploadDocument_list;
                        unlockUI();
                        $("#hrloanfile").val('');
                        $scope.cbodocumentname = "";
                        $scope.cbodocument_id = "";
                        $scope.uploadfrm = undefined;
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            var params =
                            {
                                request_gid: request_gid
                            }
                            var url = 'api/MstHRLoanDrmApproval/GetUploadDocumentsList';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.hrReuploadDocument_list = resp.data.HrReuploadDocument_list;
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
        $scope.delete_hrdocument = function (hrreuploaddocument_gid) {
            lockUI();
            var params = {
                hrreuploaddocument_gid: hrreuploaddocument_gid
            }
            var url = 'api/MstHRLoanDrmApproval/UploadDocumentsDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.hrReuploadDocument_list = resp.data.HrReuploadDocument_list;
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var params =
                {
                    request_gid: request_gid
                }
                var url = 'api/MstHRLoanDrmApproval/GetUploadDocumentsList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrReuploadDocument_list = resp.data.HrReuploadDocument_list;
                });


                unlockUI();
            });
        }
        $scope.allChecked = function () {
            return $scope.termsandcont_list.filter(function (obj) { return obj.checked; }).length === $scope.termsandcont_list.length;
         //return $scope.termsandcont_list.filter(function (obj) { return obj.checked; }).length === $scope.termsandcont_list.length;
        }
        
        $scope.checkAll = function () {
            angular.forEach($scope.termsandcont_list, function (val) {
                val.checked = $scope.check ;

            });
        };
        $scope.tremsandcondtn_acknwlg = function () {
            if ($scope.hrReuploadDocument_list == null) {
                Notify.alert('Kindly reupload the document!', 'warning')
            }
            else {
                if ($scope.termsandcont_list.filter(function (obj) { return obj.checked; }).length === $scope.termsandcont_list.length == 0) {
                    Notify.alert('Please select all the terms and conditions!', 'warning');
                }
                else {

                    var params = {
                        request_gid: request_gid,
                    }
                    var url = 'api/MstHRLoanDrmApproval/PostHrLoantremsandconditionacknwlg';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $state.go("app.MstHRLoanRaiseRequest");
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

            }
        }

       

        //$scope.checkall = function (selected) {
        //    angular.forEach($scope.termsandcont_list, function (val) {
        //        val.checked = selected;
        //    });
        //}

        //$scope.tremsandcondtn_acknwlg = function () {
        //    if ($scope.HRDocument_list != null && $scope.hrReuploadDocument_list == null) {
        //        Notify.alert('Kindly Reupload the Document!', 'warning')
        //    }
        //    else {
        //        var hrloantermsandconditions_gid;
        //        var termsandconditionslistGId = [];
        //        var check_flag = 0;
        //        angular.forEach($scope.termsandcont_list, function (val) {

        //            if (val.checked == true) {
        //                var termsandconditionslist_gid = val.hrloantermsandconditions_gid;
        //                hrloantermsandconditions_gid = val.hrloantermsandconditions_gid;
        //                termsandconditionslistGId.push(termsandconditionslist_gid);
        //                var params = {

        //                    request_gid: request_gid,
        //                    termsandconditionslist_gid: termsandconditionslistGId
        //                }
        //                if (hrloantermsandconditions_gid != undefined) {
        //                    var url = 'api/MstHRLoanDrmApproval/PostHrLoantremsandconditionacknwlg';
        //                    lockUI();
        //                    SocketService.post(url, params).then(function (resp) {
        //                        unlockUI();
        //                        if (resp.data.status == true) {
        //                            Notify.alert(resp.data.message, {
        //                                status: 'success',
        //                                pos: 'top-center',
        //                                timeout: 3000
        //                            });
        //                            $state.go("app.MstHRLoanRaiseRequest");
        //                        }
        //                        else {
        //                            Notify.alert(resp.data.message, {
        //                                status: 'warning',
        //                                pos: 'top-center',
        //                                timeout: 3000
        //                            });
        //                        }

        //                    });
        //                }
        //            }
        //            else {
        //                check_flag = 1;

        //            }

        //        });
        //        if (check_flag != 0) {
        //            Notify.alert('Select All the Terms and Conditions!', 'warning')
        //        }
        //    }

        //}



        $scope.back = function () {
            $state.go('app.MstHRLoanRaiseRequest');
        }

        $scope.view_drmquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryview.html',
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

        $scope.view_fhquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/fhqueryview.html',
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

        $scope.view_hrquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/hrqueryview.html',
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

        $scope.view_mangquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/mangqueryview.html',
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

        $scope.drmquery_close = function (drmraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        drmraisequery_gid: drmraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        request_gid: cmnfunctionService.decryptURL($location.search().hash).request_gid
                    }
                    var url = 'api/MstHRLoanDrmApproval/PostDRMresponsequery';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $modalInstance.close('closed');
                }

            }
        }
        $scope.fhquery_close = function (fhraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/fhqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        fhraisequery_gid: fhraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        request_gid: cmnfunctionService.decryptURL($location.search().hash).request_gid
                    }
                    var url = 'api/MstHRLoanDrmApproval/PostFHresponsequery';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $modalInstance.close('closed');
                }

            }
        }
        $scope.hrquery_close = function (hrraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/hrqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        hrraisequery_gid: hrraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        request_gid: cmnfunctionService.decryptURL($location.search().hash).request_gid
                    }
                    var url = 'api/MstHRLoanDrmApproval/PostHRresponsequery';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $modalInstance.close('closed');
                }

            }
        }
        $scope.mangquery_close = function (mangraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/mangqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        mangraisequery_gid: mangraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        request_gid: cmnfunctionService.decryptURL($location.search().hash).request_gid
                    }
                    var url = 'api/TrnHRLoanHRVerifications/PostManagerresponsequery';
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
                                status: 'warning',
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