(function () {
    'use strict';

    angular
        .module('angle')
        .controller('legalSR360controller', legalSR360controller);

    legalSR360controller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function legalSR360controller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'legalSR360controller';

        activate();

        function activate() {
            $scope.uploaddclickdiv = true;
            $scope.mailclickdiv = true;
            
            $scope.legalsr_gid = localStorage.getItem('legalsr_gid');
            var params = {
                customer_gid: localStorage.getItem('customer_gid'),
                legalsr_gid :localStorage.getItem('legalsr_gid')
            }
            var url = "api/raiseLegalSR/Getcustomerdetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerdetails = resp.data;
                //console.log(resp.data);
            });
            var url = 'api/raiseLegalSR/GetDemandNoticedtl';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.demandnotice_list = resp.data.demandnotice_list;
                $scope.demand_status = resp.data.demand_status;

            });
            var url = "api/raiseLegalSR/getsamgdetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_name = resp.data.customer_name;
                $scope.constitution = resp.data.constitution;
                $scope.financed_by = resp.data.financed_by;
                $scope.deal_year = resp.data.deal_year;
                $scope.address = resp.data.address;
                $scope.business_activity = resp.data.business_activity;
                $scope.email_id = resp.data.email_id;
                $scope.primary_securities = resp.data.primary_securities;
                $scope.collateral_securities = resp.data.collateral_securities;
                $scope.details_UDC_PDC = resp.data.details_UDC_PDC;
                $scope.unit_working_status = resp.data.unit_working_status;
                $scope.other_banker_exposures = resp.data.other_banker_exposures;
                $scope.cibil_data = resp.data.cibil_data;
                $scope.restructuring_data = resp.data.restructuring_data;
                $scope.status_current_overdue = resp.data.status_current_overdue;
                $scope.other_group_companies = resp.data.other_group_companies;
                $scope.meeting_details = resp.data.meeting_details;
                $scope.cycles_sanctiondated = resp.data.cycles_sanctiondated;
                $scope.limit_sanction = resp.data.limit_sanction;
                $scope.churing_account = resp.data.churing_account;
                $scope.created_date = resp.data.created_date;
                $scope.statuslegal_action = resp.data.statuslegal_action;
                $scope.instances_PTP = resp.data.instances_PTP;
                $scope.demandnotice_details = resp.data.demandnotice_details;
                $scope.other_banker_borrower = resp.data.other_banker_borrower;
                $scope.auth_remarks_list = resp.data.auth_remarks_list;
            });
            var url = "api/raiseLegalSR/getcustomerPromoter";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerPromotorlist = resp.data.customerPromoter;
            });

            var url = 'api/raiseLegalSR/getcustomerGuarantors';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.guarantors_list = resp.data.customerGuarantors;
                $scope.remarks = resp.data.remarks;
            });

            var url = "api/CustomerDashboard/Getcustomerloandetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.loandetails = resp.data.loandtl;
            });
            var param1 = {
                raiselegalSR_gid: $scope.legalsr_gid
            }

            var url = 'api/raiseLegalSR/Getcontactdtl';

            SocketService.getparams(url, param1).then(function (resp) {
                $scope.contactdetailsRM = resp.data.contactdetailsRM;
            });
            var url = "api/CustomerDashboard/Getcustomerdocumentdetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.filename_list = resp.data.filename_list;
            });

            var url = "api/CustomerDashboard/Getcustomermaildetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.composemail_list = resp.data.composemail;
            });

            var url = "api/requestCompliance/lawyerList";
            SocketService.get(url).then(function (resp) {
                $scope.assignlawyerlist = resp.data.assignlawyer;
            });

            var url = "api/CustomerDashboard/GetLawyerPayment";
            SocketService.get(url).then(function (resp) {
                $scope.invoicelist = resp.data.invoicedtl;
            });

            var param = {
                legalSR_gid: localStorage.getItem('legalsr_gid')
            }
            var url = "api/raiseLegalSR/getlegalSRapprovals";
            SocketService.getparams(url, param).then(function (resp) {

                $scope.approvals = resp.data.approvallist;
               
            });
       
            var url = "api/CustomerDashboard/GetlawyerSRassign"
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assign_lawyername = resp.data.assign_lawyername;
                $scope.assignlawyer = resp.data.assign_lawyergid;
                $scope.assignedlawyer_by = resp.data.assignedlawyer_by;
                $scope.assigned_date = resp.data.assigned_date;
                $scope.SLN_remarks = resp.data.SLN_remarks;
            });

            var url = "api/raiseLegalSR/getSLNdocument";
            SocketService.getparams(url, param).then(function (resp) {
                $scope.seeklawyerdocument=resp.data.uploadseek_list;
            });
           
            var url = "api/raiseLegalSR/getcollateralinfo";
            SocketService.getparams(url, param).then(function (resp) {

                $scope.customerCollateral = resp.data.customerCollateral;
            });
            var url = "api/raiseLegalSR/getrequesteddtl";
            SocketService.getparams(url, param).then(function (resp) {

                $scope.requesteddetails = resp.data;
            });
        }

        $scope.uploadclick = function () {
            $scope.uploadddiv = true;
            $scope.uploaddclickdiv = false;
        }

        $scope.cancelupload = function () {
            $scope.uploadddiv = false;
            $scope.uploaddclickdiv = true;
            $("#addupload").val('');
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
            frm.append('customer_gid', localStorage.getItem('customer_gid'));
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;

        }

        $scope.handleFile = function () {
            var url = 'api/CustomerDashboard/UploadDocument';

            SocketService.postFile(url, $scope.uploadfrm).then(function (resp){
                $scope.filename_list = resp.data.filename_list;
                $("#addupload").val('');

              
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.document_type='';
                    Notify.alert('Document Uploaded Successfully', 'success')

                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported','danger')


                }
            });

        
        }
                    
                     

                
        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            //console.log(val1);
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            //console.log(str);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.createmail = function () {

            var modalInstance = $modal.open({
                templateUrl: '/CustomerCreateMailContent.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.close = function () {
                    $modalInstance.close('closed');
                }

                $scope.sendcomposemail = function () {
                    var params = {
                        to_mail: $scope.tomail,
                        cc_mail: $scope.ccmail,
                        bcc_mail: $scope.bccmail,
                        subject_mail: $scope.mailsubject,
                        content_mail: $scope.mailcontent,
                        customer_gid: localStorage.getItem('customer_gid')
                    }

                    var url = "api/CustomerDashboard/sendcomposemail";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            activate();
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

        $scope.viewMailContent = function (composemail_gid) {
    
            var params = {
                composemail_gid: composemail_gid
            }

            var modalInstance = $modal.open({
                templateUrl: '/CustomerViewMailContent.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.close = function () {
                    $modalInstance.close('closed');
                }
                var url = "api/CustomerDashboard/Getcustomermail";
                SocketService.getparams(url, params).then(function (resp) {
                    //console.log(resp);
                    $scope.frommail_view = resp.data.from_mail;
                    $scope.tomail_view = resp.data.to_mail;
                    $scope.ccmail_view = resp.data.cc_mail;
                    $scope.bccmail_view = resp.data.bcc_mail;
                    $scope.mailsubject_view = resp.data.subject_mail;
                    $scope.mailcontent_view = resp.data.content_mail;
                    $scope.created_by = resp.data.created_by;
                    $scope.created_date = resp.data.created_date;
                });
            }
            
        }

        $scope.legarSRapprove= function ()
        {
          
            var params = {
                legalsr_gid:localStorage.getItem('legalsr_gid'),
                approval_remarks:$scope.txtremarks
            }
           
            var url = 'api/raiseLegalSR/legalSRApproval';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                 
                    Notify.alert(' Successfully..!!', 'success')
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
                activate();
            });
        }

        $scope.legalSRreject = function () {
            var params = {
                legalsr_gid: $scope.legalsr_gid
            }

            var url = 'api/raiseLegalSR/legalSRreject';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    Notify.alert('Legal SR Hold', 'success')


                }
                else {
                    unlockUI();
                    Notify.alert('Error Occured While Updating the Status', 'warning')

                }

            });
        }

        $scope.assignLawyerSubmit = function () {
            var lawyeruser_gid = $scope.cboassignlawyer;
            var lawyeruser_name = $('#assignlawyer :selected').text();


            var params = {
                legalSR_gid: localStorage.getItem('legalsr_gid'),
                lawyeruser_gid: $scope.cboassignlawyer
              
            }
            //console.log(params);
            var url = "api/raiseLegalSR/tmpSLNdocumentclear";
            SocketService.get(url).then(function (resp) {

            });

            var modalInstance = $modal.open({
                templateUrl: '/lawyeruploadContent.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.lawyeruser_name = lawyeruser_name;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.close = function () {
                    $modalInstance.close('closed');
                }

                $scope.uploadseek = function (val, val1, name) {
                    var item = {
                        name: val[0].name,
                        file: val[0]
                    };
                    var frm = new FormData();
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    frm.append('document_name', $scope.documentname);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;

                    var url = 'api/raiseLegalSR/SLNUpload';

                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.uploadseek_list = resp.data.uploadseek_list;
                        $("#addSLNupload").val('');

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert('File Format Not Supported!', {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });

                }

                $scope.uploadcancel = function (tmpSLN_documentgid) {
                    var seekupload = {
                        tmpSLN_documentgid: tmpSLN_documentgid
                    }
                    //console.log(seekupload);
                    var url = 'api/raiseLegalSR/SLNuploadcancel';
                    SocketService.getparams(url, seekupload).then(function (resp) {
                        $scope.uploadseek_list = resp.data.uploadseek_list;
                    });
                }

                $scope.assignconfirm = function () {
                    var params = {
                        legalSR_gid: localStorage.getItem('legalsr_gid'),
                        lawyeruser_gid: lawyeruser_gid,
                        SLN_remarks: $scope.SLNlawyerremarks
                    }
                    //console.log(params);
                    lockUI();
                    var url = "api/CustomerDashboard/assignSRLawyer";
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }

            }


        }

        $scope.cancelassignLawyer = function () {
            $scope.cboassignlawyer = "";
          }


        $scope.back = function () {
            $state.go('app.legalSLNmgmt');
           
        }
       
    }
})();
