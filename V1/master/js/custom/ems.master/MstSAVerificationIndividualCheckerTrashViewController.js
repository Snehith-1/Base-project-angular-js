
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAVerificationIndividualCheckerTrashViewController', MstSAVerificationIndividualCheckerTrashViewController);

    MstSAVerificationIndividualCheckerTrashViewController.$inject = ['DownloaddocumentService', '$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAVerificationIndividualCheckerTrashViewController(DownloaddocumentService, $rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAVerificationIndividualCheckerTrashViewController';

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.sacontact_gid = searchObject.lssacontact_gid;
        var sacontact_gid = $scope.sacontact_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;

        activate();
        function activate() {
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open2 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open3 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open4 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/MstSAOnboardingInstitution/GetDropDown';

            SocketService.get(url).then(function (resp) {
                $scope.saassessmentagencylist = resp.data.saassessmentagencylist;
            });

            SocketService.get(url).then(function (resp) {
                $scope.assessmentagencyratinglist = resp.data.assessmentagencyratinglist;
            });

            var url = 'api/MstSAOnboardingIndividual/TempBureauDocuments';
            SocketService.get(url).then(function (resp) {

            });

            var url = 'api/MstSAOnboardingIndividual/TempSAMailDocument';
            SocketService.get(url).then(function (resp) {

            });

            var url = 'api/MstSAOnboardingIndividual/TempSaVerifyDocument';
            SocketService.get(url).then(function (resp) {

            });

            var url = 'api/MstApplicationAdd/PANAbsenceReasonList';
            SocketService.get(url).then(function (resp) {
                $scope.panabsencereason_list = resp.data.panabsencereason_list;
            });

            var url = 'api/AssociateMaster/GetAssociateMasterASC';
            SocketService.get(url).then(function (resp) {
                $scope.associatemaster_list = resp.data.associatemaster_list;
            });

            var url = 'api/MstApplication360/BureauNameList';
            SocketService.get(url).then(function (resp) {
                $scope.bureau_list = resp.data.bureauname_list;
            });
            var param = {
                sacontact_gid: sacontact_gid
            }
            var url = 'api/MstSAOnboardingIndividual/GetMobileNoEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.individualmobileno_list = resp.data.Sacontactmobileno_list;
            });


            var url = 'api/MstSAOnboardingIndividual/GetEmailAddressEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.saOnboardemailaddress_list = resp.data.saOnboardemailaddress_list;
            });
            var url = 'api/MstSAOnboardingIndividual/GetProspectsEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.individualprospects_list = resp.data.saOnboardProspects_list;
            });
            var url = 'api/MstSAOnboardingIndividual/GetAddressEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactindividualaddress_list = resp.data.saOnboardaddress_list;
            });
            var url = 'api/MstSAOnboardingIndividual/GetDocumentEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.individualupload_list = resp.data.saOnboardDocument_list;
            });



            var url = 'api/MstSAOnboardingIndividual/GetSABureauInstitutionList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactbureau_list = resp.data.sainstitutebureau_list;

            });
            var url = 'api/MstSAOnboardingIndividual/SAMailDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mailuploaddoc_list = resp.data.sauploaddoc_list;
            });

            var url = 'api/MstSAOnboardingIndividual/SaVerifyDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;
                $scope.instverificationdocument_list = resp.data.sauploaddoc_list;
            });
            var url = 'api/MstSAOnboardingBussDevtVerification/GetBDIndividualRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.bdindividualraisequery_list = resp.data.bdindividualraisequery_list;
            });
            var url = 'api/MstSAOnboardingIndividual/GetMakerIndividualRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.makerindividualraisequery_list = resp.data.makerindividualraisequery_list;
            });
            var url = 'api/MstSAOnboardingIndividual/GetApproverIndividualRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.approverindividualraisequery_list = resp.data.approverindividualraisequery_list;
            });

            var url = 'api/MstSAOnboardingIndividual/GetCheckerIndividualRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.checkerindividualraisequery_list = resp.data.checkerindividualraisequery_list;
            });
            var url = 'api/MstSAOnboardingIndividual/ApprovalInitatedDetail';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cbopanstatus = resp.data.pan_status;
                if ($scope.cbopanstatus == 'Customer Submitting PAN') {
                    $scope.havepan = true;
                    $scope.havenotpan = false;
                }
                else if ($scope.cbopanstatus == 'Customer Submitting Form 60') {
                    $scope.havenotpan = true;
                    $scope.havepan = false;
                }
                $scope.lblrecdsce = resp.data.recordsource;
                $scope.lblsatype = resp.data.satype_name;
                $scope.lblsaentitytype = resp.data.saentitytype_name;
                $scope.lblsafirst_name = resp.data.sa_firstname;
                $scope.lblsamiddle_name = resp.data.sa_middlename;
                $scope.lblsalast_name = resp.data.sa_lastname;
                $scope.lblpan_no = resp.data.sa_pannumber;
                $scope.txtcredited_date = resp.data.sa_appcrediteddate;
                $scope.lblindividual_aadharnumber = resp.data.sa_aadharnumber;
                var aadhar = $scope.lblindividual_aadharnumber;
                var mask = aadhar.slice(-4);
                var maskaadhar = 'XXXX-XXXX-' + mask;
                $scope.individual_aadharnumber = maskaadhar;
                $scope.txtutr_number = resp.data.sa_apputr;
                $scope.txtcredited_amount = resp.data.sa_appcreditedamount;
                $scope.lblifsc_code = resp.data.saifsc_code;
                $scope.lblbankaccount_number = resp.data.saaccount_number;
                $scope.lblconfirmbankacct_no = resp.data.confirmbankaccountnumber;
                $scope.lblaccountholder_name = resp.data.saaccountholder_name;
                $scope.lblcancelledcheque_number = resp.data.sacanccheque_number;
                $scope.lblbranch_name = resp.data.sabranch_name;
                $scope.lblbank_name = resp.data.sabank_name;
                $scope.lblmicr = resp.data.micr;
                $scope.lblcity = resp.data.city;
                $scope.lblstate = resp.data.state;
                $scope.lbldistrict = resp.data.district;
                $scope.lblbranchaddress = resp.data.branch_address;
                $scope.lblsaupdateddate = resp.data.sa_updated_date;
                $scope.lblsaupdated_by = resp.data.sa_updated_by;
                $scope.lblsareportingmanager = resp.data.sa_reportingmanager;
                $scope.cbogender = resp.data.gender;
                $scope.interviewevalution = resp.data.interviewevalution;
                $scope.applicationform = resp.data.applicationform;
                $scope.kycdocuments = resp.data.kycdocuments;
                $scope.vettingstatus = resp.data.vettingstatus;
                $scope.addressproof = resp.data.addressproof;
                $scope.photographs = resp.data.photographs;
                $scope.cancelledcheckleaf = resp.data.cancelledcheckleaf;
                $scope.agreementexecutiondate = resp.data.agreementexecutiondate;
                $scope.agreementexpirydate = resp.data.agreementexpirydate;
                $scope.agreementstatus = resp.data.agreementstatus;
                $scope.agroagreementexecutiondate = resp.data.agroagreementexecutiondate;
                $scope.agroagreementexpirydate = resp.data.agroagreementexpirydate;
                $scope.agroagreementstatus = resp.data.agroagreementstatus;
                $scope.verificationremarks = resp.data.verificationremarks;
                $scope.approvalinitated_flag = resp.data.approvalinitated_flag;
                $scope.txtassessmentagency_name = resp.data.assessmentagency_name;
                $scope.txtassessmentagencyrating_name = resp.data.assessmentagencyrating_name;
                $scope.txtrasteason_date = resp.data.ratingas_datecredit;
                $scope.txtreferredby = resp.data.referred_by;
                $scope.rejected_remarks = resp.data.rejected_remarks;
                $scope.rejected_by = resp.data.rejected_by;
                $scope.rejected_date = resp.data.rejected_date;
                $scope.txtutrno = resp.data.utr_no;
                $scope.present_occupation = resp.data.present_occupation;
                $scope.work_experience = resp.data.work_experience;
                $scope.Expagri_business = resp.data.Expagri_business;

                unlockUI();
            });

            var url = 'api/MstSAOnboardingIndividual/GetPANForm60EditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactpanform60_list = resp.data.contactpanform60sa_list;
            });

            var url = 'api/MstSAOnboardingIndividual/EditPANAbsenceReasonList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.panabsencereason_list = resp.data.panabsencereasonsa_list;
                for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                    if ($scope.panabsencereason_list[i].check_status == true) {
                        $scope.panabsencereason_list[i].checked = true;
                    }
                }
            });

            var url = 'api/MstSAOnboardingIndividual/ContactPANAbsenceReasonList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactpanabsencereason_list = resp.data.contactpanabsencereasonsa_list;
            });

            unlockUI();
            $scope.havenotpan = false;
            $scope.havepan = false;
            $scope.view_nopanreasons = false;
        }

        $scope.document_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.recproof_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        //$scope.individualdoc_downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

        $scope.back = function () {


            if (lspage == 'IndividualPending') {
                $location.url('app/MstSAVerificationIndividualCheckerDistractSummary');
            }
            else if (lspage == 'IndividualInitiate') {
                $location.url('app/MstSAVerificationIndividualCheckerDistractSummary');
            }
        }
        $scope.view_query = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/bdverificationview.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = queryresponse_by;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.view_checkerquerydesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/mycampaigncheckerqueryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = queryresponse_by;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.view_campaignquerydesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/makerindividualView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = queryresponse_by;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.mycampaignquery_close = function (approverindividualraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/mycampaignqueryClose.html',
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
                        approverindividualraisequery_gid: approverindividualraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        sacontact_gid: $location.search().lssacontact_gid
                    }
                    var url = 'api/MstSAOnboardingIndividual/PostApproverIndividualresponsequery';
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
        $scope.view_campaignquerydesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/mycampaignqueryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = queryresponse_by;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.final_approval = function () {
            var params = {

                interviewevalution: $scope.rbointerview_evalution_form,
                applicationform: $scope.rboapplication_form,
                yearsitreturns: $scope.rbo3Yearsitreturn,
                bankstatement: $scope.rbobank_statement,
                kycdocuments: $scope.rbokycdocuments,
                prospect: $scope.rboprospect20,
                vettingstatus: $scope.rbovettingstatus,
                scannedcopyreception: $scope.rboscannedcopyreception,
                addressproof: $scope.rboaddressproof,
                photographs: $scope.rbophotographs,
                cancelledcheckleaf: $scope.rbocancelledcheckleaf,
                houseofficeverification: $scope.rbohouseandofficeverificationform,
                agreementexecution_date: $scope.txtagreementexecution_date,
                agreementexpiry_date: $scope.txtagreementexpiry_date,
                agreementstatus: $scope.cboagreement_status,
                bookletnumber: $scope.txtbooklet_number,
                sacontact_gid: $scope.sacontact_gid,
                verificationremarks: $scope.txtremarks,
                sa_reportingmanager: $scope.lblsareportingmanager

            }
            var url = 'api/MstSAOnboardingIndividual/ApprovalInitated';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go("app.MstSAVerificationIndPendingSummary");
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

        $scope.CICDocumentUpload = function (val, val1, name) {
            var item = {
                file: val[0]
            };
            var frm = new FormData();
            frm.append('file', item.file);

            frm.append('document_name', $scope.documentname);
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstSAOnboardingIndividual/SaInstitutionDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#file").val('');
                        $scope.txtcic_document = "";
                        $scope.uploadfrm = undefined;
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var param = {
                        saindividual2bureau_gid: $scope.saindividual2bureau_gid
                    };
                    var url = 'api/MstSAOnboardingIndividual/SAUploadIndividualDocList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.lrfilename = resp.data.filename;
                        $scope.lrfilepath = resp.data.filepath;
                        $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                    });
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }

        $scope.uploaddocumentcancel = function (individualsabureaudocumentupload_gid) {
            lockUI();
            var params = {
                individualsabureaudocumentupload_gid: individualsabureaudocumentupload_gid
            }
            var url = 'api/MstSAOnboardingIndividual/DeleteBureauDocuments';
            SocketService.getparams(url, params).then(function (resp) {
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
                var param = {
                    saindividual2bureau_gid: $scope.saindividual2bureau_gid
                };
                var url = 'api/MstSAOnboardingIndividual/SAUploadIndividualDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.lrfilename = resp.data.filename;
                    $scope.lrfilepath = resp.data.filepath;
                    $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                });
                unlockUI();
            });
        }

        //$scope.downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}
        $scope.report_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        //$scope.submit_verification=function(){
        //    var params = {
        //    saassociate_name : $scope.cbosamunnati_associate_name,
        //    satype_name:$scope.txtsatype,
        //    saentitytype_name:$scope.txtsaentitytype,
        //    designation:$scope.txtdesignation,
        //    sacontactperson_name:$scope.txtsacontactperson_name,
        //    dateofincorporation:$scope.txtdateofincorporation,
        //    annual_turnover:$scope.txtannual_turnover,
        //    interviewevaluation_form:$scope.rbointerview_evalution_form,
        //    application_form:$scope.rboapplication_form,
        //    yrs_itreturns:$scope.rbo3Yearsitreturn,
        //    bank_statement:$scope.rbobank_statement,
        //    kyc_documents:$scope.rbokycdocuments,
        //    prospect_20:$scope.rboprospect20,
        //    vetting_status:$scope.rbovettingstatus,
        //    scannedcopy_reception:$scope.rboscannedcopyreception,
        //    address_proof:$scope.rboaddressproof,
        //    photographs:$scope.rbophotographs,
        //    Cancelled_chequeleaf:$scope.rbocancelledcheckleaf,
        //    houseandoffice_verificationform:$scope.rbohouseandofficeverificationform,
        //    agreementexecution_date:$scope.txtagreementexecution_date,
        //    agreementexpiry_date:$scope.txtagreementexpiry_date,
        //    agreement_status:$scope.cboagreement_status,
        //    booletno_finagro:$scope.txtbooklet_number,
        //    remarks:$scope.txtremarks,
        //    bureau_name:$scope.txtbureau_name,
        //    bureau_score:$scope.txtbureau_score,
        //    scoreas_on:$scope.txtscoreason_date,
        //    observations:$scope.txtobservation,
        //    bureau_response:$scope.txtbureauresponse,
        //    creditassessment_agency:$scope.cbocreditassessmentagency,
        //    creditassessment_agency_gid:$scope.cbocreditassessmentagency,
        //    assessment_rating:$scope.cboassessmentrating,
        //    assessment_rating_gid:$scope.cboassessmentrating,
        //    ratingas_on:$scope.txtrasteason_date,
        //    utr:$scope.txtutr_number,
        //    credited_date:$scope.txtcrediteddate,
        //    credited_amount:$scope.txtcredited_amount,
        //    verified:$scope.cboverified,
        //    ifsc_code:$scope.txtifsc_code,
        //    bank_account_number:$scope.txtbank_account_number,
        //    account_holder_name:$scope.txtaccount_holder_name,
        //    cancelled_cheque_number:$scope.txtcancelled_cheque_number,
        //    bank_name:$scope.txtbank_name,
        //    bank_branch:$scope.txtbank_branch
        //    }
        //    var url = 'api/MstApplicationGradingTool/GetAssessmentCriteriaDropDown';
        //    SocketService.get(url, params).then(function (resp) {
        //        if (resp.data.status == true) {
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        } 
        //        $state.go("app.MstSAOnboardingVerificationSummary");
        //    });

        //}
        //$scope.save_verification=function(){
        //    var params={
        //        saassociate_name : $scope.cbosamunnati_associate_name,
        //        satype_name:$scope.txtsatype,
        //        saentitytype_name:$scope.txtsaentitytype,
        //        designation:$scope.txtdesignation,
        //        sacontactperson_name:$scope.txtsacontactperson_name,
        //        dateofincorporation:$scope.txtdateofincorporation,
        //        annual_turnover:$scope.txtannual_turnover,
        //        interviewevaluation_form:$scope.rbointerview_evalution_form,
        //        application_form:$scope.rboapplication_form,
        //        yrs_itreturns:$scope.rbo3Yearsitreturn,
        //        bank_statement:$scope.rbobank_statement,
        //        kyc_documents:$scope.rbokycdocuments,
        //        prospect_20:$scope.rboprospect20,
        //        vetting_status:$scope.rbovettingstatus,
        //        scannedcopy_reception:$scope.rboscannedcopyreception,
        //        address_proof:$scope.rboaddressproof,
        //        photographs:$scope.rbophotographs,
        //        Cancelled_chequeleaf:$scope.rbocancelledcheckleaf,
        //        houseandoffice_verificationform:$scope.rbohouseandofficeverificationform,
        //        agreementexecution_date:$scope.txtagreementexecution_date,
        //        agreementexpiry_date:$scope.txtagreementexpiry_date,
        //        agreement_status:$scope.cboagreement_status,
        //        booletno_finagro:$scope.txtbooklet_number,
        //        remarks:$scope.txtremarks,
        //        bureau_name:$scope.txtbureau_name,
        //        bureau_score:$scope.txtbureau_score,
        //        scoreas_on:$scope.txtscoreason_date,
        //        observations:$scope.txtobservation,
        //        bureau_response:$scope.txtbureauresponse,
        //        creditassessment_agency:$scope.cbocreditassessmentagency,
        //        creditassessment_agency_gid:$scope.cbocreditassessmentagency,
        //        assessment_rating:$scope.cboassessmentrating,
        //        assessment_rating_gid:$scope.cboassessmentrating,
        //        ratingas_on:$scope.txtrasteason_date,
        //        utr:$scope.txtutr_number,
        //        credited_date:$scope.txtcrediteddate,
        //        credited_amount:$scope.txtcredited_amount,
        //        verified:$scope.cboverified,
        //        ifsc_code:$scope.txtifsc_code,
        //        bank_account_number:$scope.txtbank_account_number,
        //        account_holder_name:$scope.txtaccount_holder_name,
        //        cancelled_cheque_number:$scope.txtcancelled_cheque_number,
        //        bank_name:$scope.txtbank_name,
        //        bank_branch:$scope.txtbank_branch
        //    }
        //    var url = 'api/MstApplicationGradingTool/GetAssessmentCriteriaDropDown';
        //    SocketService.get(url, params).then(function (resp) {
        //        if (resp.data.status == true) {
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        } 
        //        $state.go("app.MstSAOnboardingVerificationSummary");
        //    });
        //}  
        $scope.onchangedate = function () {
            var date2 = new Date($scope.txtagreementexecution_date);
            var date3 = date2.setDate(date2.getDate());
            var date = new Date(date3);
            var day = date.getDate();
            var month = date.getMonth() + 1;
            var year = date.getFullYear() + 1;
            if ((month == 2) && (date == 29)) {
                date = 28;
            }
            var newdate = (day < 10 ? '0' : '') + day + '-' + (month < 10 ? '0' : '') + month + '-' + year;
            $scope.txtagreementexpiry_date = newdate;
        }
        function inWords(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';
            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
        }
        $scope.annual_turnoverChange = function () {
            var input = document.getElementById('annual_turnover').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_annualturnover = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtannual_turnover = "";
            }
            else {
                $scope.txtannual_turnover = output;
                document.getElementById('words_annualturnover').innerHTML = lswords_annualturnover;
            }
        }
        $scope.credited_amountChange = function () {
            var input = document.getElementById('credited_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditedamount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtcredited_amount = "";
            }
            else {
                $scope.txtcredited_amount = output;
                document.getElementById('words_creditedamount').innerHTML = lswords_creditedamount;
            }
        }
        $scope.uploadattachment = function (val, val1, name) {
            lockUI();
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                var frm = new FormData();
                for (var i = 0; i <= fi.files.length - 1; i++) {
                    frm.append(fi.files[i].name, fi.files[i])
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                }
                var url = 'api/OsdTrnCustomerQueryMgmt/MailAttachment';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                    $scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Document Uploaded Successfully..!!', 'success')
                        var url = 'api/OsdTrnCustomerQueryMgmt/GetMailAttachment';
                        SocketService.get(url).then(function (resp) {
                            $scope.uploaddocument = resp.data.MdlDocDetails;
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('File Format Not Supported!')
                    }
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }
        $scope.UploadDocCancel = function (id) {
            var params = {
                mailattachment_gid: id
            }
            var url = 'api/OsdTrnCustomerQueryMgmt/DeleteAttachment';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document deleted Successfully..!!', 'success')
                    var url = 'api/OsdTrnCustomerQueryMgmt/GetMailAttachment';
                    SocketService.get(url).then(function (resp) {
                        $scope.uploaddocument = resp.data.MdlDocDetails;
                    });
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred')
                }
            });
        }
        $scope.DocumentUpload = function (val, val1, name) {

            var item = {

                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);

            frm.append('document_name', $scope.txtdocument_name);
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.documentupload_list = resp.data.application_list;
                    unlockUI();

                    $("#document").val('');
                    $scope.txtdocument_name = "";
                    $scope.uploadfrm = undefined;

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
                    unlockUI();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }
        }
        //$scope.downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}
        $scope.verification_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        // verification 


        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.downloadall1 = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        //Bureau
        $scope.addbureau_individual = function () {
            if (($scope.cboBureauName == undefined || $scope.cboBureauName == null || $scope.cboBureauName == '') || ($scope.txtbureauscore_date == null || $scope.txtbureauscore_date == undefined || $scope.txtbureauscore_date == '') ||
                ($scope.txtbureau_score == null || $scope.txtbureau_score == undefined || $scope.txtbureau_score == '') || ($scope.txtobservations == null || $scope.txtobservations == undefined || $scope.txtbureauscore_date == undefined || $scope.txtbureauscore_date == '') ||
                ($scope.txtbureau_score == null || $scope.txtbureau_score == undefined || $scope.txtbureau_score == '') || ($scope.txtobservations == null || $scope.txtobservations == undefined || $scope.txtobservations == '') || ($scope.txtbureau_response == '' || $scope.txtbureau_response == undefined || $scope.txtbureau_response == null)) {

                Notify.alert('Enter All Mandatory Values', 'warning');
            }
            else {
                var lsassessmentagency_gid = '';
                var lsassessmentagency_name = '';
                var lsassessmentagencyrating_gid = '';
                var lsassessmentagencyrating_name = '';

                if ($scope.cbosaassessmentagency != undefined || $scope.cbosaassessmentagency != null) {
                    lsassessmentagency_gid = $scope.cbosaassessmentagency.assessmentagency_gid;
                    lsassessmentagency_name = $scope.cbosaassessmentagency.assessmentagency_name;
                }
                if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
                    lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
                    lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
                }

                var params = {
                    assessmentagency_gid: lsassessmentagency_gid,
                    assessmentagency_name: lsassessmentagency_name,
                    assessmentagencyrating_name: lsassessmentagencyrating_name,
                    assessmentagencyrating_gid: lsassessmentagencyrating_gid,

                    sacontact_gid: $scope.sacontact_gid,
                    bureauname_gid: $scope.cboBureauName.bureauname_gid,
                    bureauname_name: $scope.cboBureauName.bureauname_name,
                    bureau_score: $scope.txtbureau_score,
                    bureauscore_date: $scope.txtbureauscore_date,
                    observations: $scope.txtobservations,
                    bureau_response: $scope.txtbureau_response,
                    ratingas_date: $scope.txtrasteason_date,
                }
                var url = 'api/MstSAOnboardingIndividual/PostSABureauInstitution';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        $scope.cboBureauName = '';
                        $scope.txtbureauscore_date = '';
                        $scope.txtobservations = '';
                        $scope.txtbureau_response = '';
                        $scope.txtbureau_score = '';
                        $scope.cicuploaddoc_list = '';
                        $scope.cboassessmentagencyrating = '';
                        $scope.cbosaassessmentagency = '';
                        $scope.txtrasteason_date = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var params = {
                        sacontact_gid: $scope.sacontact_gid,
                    }
                    var url = 'api/MstSAOnboardingIndividual/GetSABureauInstitutionList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.contactbureau_list = resp.data.sainstitutebureau_list;

                    });



                });
            }
        }

        //$scope.bureau_view = function (saindividual2bureau_gid) {
        //    $location.url('app/MstSAonboardingIndBureauView?lssaindividual2bureau_gid=' + saindividual2bureau_gid + '&lssacontact_gid=' + $scope.sacontact_gid);
        //}

        $scope.bureau_view = function (saindividual2bureau_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/bureau_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var param = {
                    saindividual2bureau_gid: saindividual2bureau_gid
                }

                var url = 'api/MstSAOnboardingIndividual/SABureauView';

                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bureauname_name = resp.data.bureauname_name;
                    $scope.bureau_gid = resp.data.bureauname_gid;
                    $scope.txtbureau_score = resp.data.bureau_score;
                    $scope.txtbureauscore_date = resp.data.bureauscore_date;
                    $scope.txtobservations = resp.data.observations;
                    $scope.txtbureau_response = resp.data.bureau_response;
                    $scope.contact2bureau_gid = resp.data.contact2bureau_gid;

                    $scope.txtassessmentagency_name = resp.data.assessmentagency_name;
                    $scope.txtassessmentagencyrating_name = resp.data.assessmentagencyrating_name;
                    $scope.ratingas_date = resp.data.ratingas_date;

                    unlockUI();
                });

                var url = 'api/MstSAOnboardingIndividual/SAUploadIndDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.lrfilename = resp.data.filename;
                    $scope.lrfilepath = resp.data.filepath;
                    $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                //$scope.downloads = function (val1, val2) {
                //    var phyPath = val1;
                //    var relPath = phyPath.split("StoryboardAPI");
                //    var relpath1 = relPath[1].replace("\\", "/");
                //    var hosts = window.location.host;
                //    var prefix = location.protocol + "//";
                //    var str = prefix.concat(hosts, relpath1);
                //    var link = document.createElement("a");
                //    link.download = val2;
                //    var uri = str;
                //    link.href = uri;
                //    link.click();
                //}
                $scope.downloadallbureau = function (val1, val2) {

                    for (var i = 0; i < val2.length; i++) {
                        //  console.log(array[i]);
                        DownloaddocumentService.Downloaddocument(val1, val2[i]);
                    }

                }
                $scope.documentbureauviewer = function (val1, val2) {
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
                $scope.downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

            }
        }
        $scope.bureau_delete = function (saindividual2bureau_gid) {
            var params = {
                saindividual2bureau_gid: saindividual2bureau_gid
            }
            var url = 'api/MstSAOnboardingIndividual/DeleteContactBureau';
            SocketService.getparams(url, params).then(function (resp) {
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
                    activate();
                }

            });
        }

        //Mail approval

        $scope.maildocumentupload = function (val, val1, name) {

            var item = {
                file: val[0]
            };
            var frm = new FormData();
            frm.append('file', item.file);

            frm.append('document_name', $scope.documentname);
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstSAOnboardingIndividual/SaMailDocument';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    //$scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#file").val('');
                        $scope.txtcic_document = "";
                        $scope.uploadfrm = undefined;
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var param = {
                        sacontact_gid: $scope.sacontact_gid
                    };
                    var url = 'api/MstSAOnboardingIndividual/SAMailDocumentTempList';
                    lockUI();
                    SocketService.getparams(url, param).then(function (resp) {
                        unlockUI();
                        $scope.mailuploaddoc_list = resp.data.sauploaddoc_list;
                    });

                });
            }
            else {
                alert('Please select a file.')
            }
        }

        $scope.maildocumentcancel = function (saindividualmaildocument_gid) {

            var params = {
                saindividualmaildocument_gid: saindividualmaildocument_gid
            }
            var url = 'api/MstSAOnboardingIndividual/DeleteSAMailDocument';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
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
                var param = {
                    sacontact_gid: $scope.sacontact_gid
                };
                var url = 'api/MstSAOnboardingIndividual/SAMailDocumentTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.mailuploaddoc_list = resp.data.sauploaddoc_list;
                });
                unlockUI();
            });
        }

        //Verification document
        $scope.verifyindividualdocument = function (val, val1, name) {
            if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                $("#companyfile").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                //frm.append('document_title', $scope.cbocompanydocumentname.sadocumentlist_name);
                //frm.append('sadocumentlist_gid', $scope.cbocompanydocumentname.sadocumentlist_gid);
                frm.append('document_title', $scope.txtdocument_title);
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstSAOnboardingIndividual/SaVerifyDocument';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        //$scope.saOnboardInstiDocument_list = resp.data.saOnboardInstiDocument_list;
                        unlockUI();
                        $("#companyfile").val('');
                        //$scope.cbocompanydocumentname = "";
                        $scope.txtdocument_title = "";
                        $scope.uploadfrm = undefined;
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            var param = {
                                sacontact_gid: $scope.sacontact_gid
                            };
                            var url = 'api/MstSAOnboardingIndividual/SaVerifyDocumentTempList';
                            SocketService.getparams(url, param).then(function (resp) {
                                $scope.lufilename = resp.data.filename;
                                $scope.lufilepath = resp.data.filepath;
                                $scope.instverificationdocument_list = resp.data.sauploaddoc_list;
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
        $scope.verifydelete = function (saindividualverifydocument_gid) {
            lockUI();
            var params = {
                saindividualverifydocument_gid: saindividualverifydocument_gid
            }
            var url = 'api/MstSAOnboardingIndividual/DeleteSaVerifyDocument';
            SocketService.getparams(url, params).then(function (resp) {
                //$scope.saOnboardInstiDocument_list = resp.data.saOnboardInstiDocument_list;
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
                var param = {
                    sacontact_gid: $scope.sacontact_gid
                };
                var url = 'api/MstSAOnboardingIndividual/SaVerifyDocumentTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.lufilename = resp.data.filename;
                    $scope.lufilepath = resp.data.filepath;
                    $scope.instverificationdocument_list = resp.data.sauploaddoc_list;
                });
                unlockUI();
            });
        }
        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, sapostal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    longitude: longitude,
                    latitude: latitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", sapostal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", sapostal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
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


    }
})();
