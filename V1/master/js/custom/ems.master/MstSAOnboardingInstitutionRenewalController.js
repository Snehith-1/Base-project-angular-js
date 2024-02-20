(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingInstitutionRenewalController', MstSAOnboardingInstitutionRenewalController);

    MstSAOnboardingInstitutionRenewalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstSAOnboardingInstitutionRenewalController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingInstitutionRenewalController';

        //$scope.sacontactinstitution_gid = $location.search().lssacontactinstitution_gid;
        //var sacontactinstitution_gid = $scope.sacontactinstitution_gid;

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);

        var sacontactinstitution_gid = searchObject.lssacontactinstitution_gid;

        $scope.saentitytype_gid = searchObject.saentitytype_gid;

        var saentitytype_gid = $scope.saentitytype_gid;

        $scope.samfin_code = searchObject.samfin_code;
        var samfin_code = $scope.samfin_code;
        $scope.samagro_code = searchObject.samagro_code;
        var samagro_code = $scope.samagro_code;

        activate();
        function activate() {
            var params = {
                saentitytype_gid: saentitytype_gid,

            }

            var url = 'api/MstSAOnboardingIndividual/GetEntitytype';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
            });
            var param = {
                sacontactinstitution_gid: sacontactinstitution_gid
            }

            var url = 'api/MstSAOnboardingInstitution/InstitutionGSTList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.gst_Onboard_list = resp.data.gst_Onboard_list;
            });
            var url = 'api/MstSAOnboardingInstitution/InstitutionMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.SacontactInstimobileno_list = resp.data.SacontactInstimobileno_list;
            });
            var url = 'api/MstSAOnboardingInstitution/InstitutionEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.saOnboardInstiemailaddress_list = resp.data.saOnboardInstiemailaddress_list;
            });
            var url = 'api/MstSAOnboardingInstitution/InstitutionAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.saOnboardInstiaddress_list = resp.data.saOnboardInstiaddress_list;

            });
            var url = 'api/MstSAOnboardingInstitution/IndividualList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.onboard_IndividualInsti_list = resp.data.onboard_IndividualInsti_list;
            });
            var url = 'api/MstSAOnboardingInstitution/GetInstitutionProspectsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.saOnboardInstiProspects_list = resp.data.saOnboardInstiProspects_list;
            });
            var url = 'api/MstSAOnboardingInstitution/UploadList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;
                $scope.saOnboardInstiDocument_list = resp.data.saOnboardInstiDocument_list;
            });
            //var url = 'api/MstSAOnboardingInstitution/DocumentUploadViewList';
            //SocketService.getparams(url, param).then(function (resp) {
            //    $scope.lsfilename = resp.data.filename;
            //    $scope.lsfilepath = resp.data.filepath;
            //    $scope.panlist = resp.data.panlist;
            //});
            var url = 'api/MstSAOnboardingInstitution/GetMakerInstitutionRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.makerinstitutionraisequery_list = resp.data.makerinstitutionraisequery_list;
            });
            var url = 'api/MstSAOnboardingBussDevtVerification/GetBDInstitutionRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.bdinstitutionraisequery_list = resp.data.bdinstitutionraisequery_list;
            });
            var url = 'api/MstSAOnboardingInstitution/GetCheckerInstitutionRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.checkerinstitutionraisequery_list = resp.data.checkerinstitutionraisequery_list;
            });
            var url = 'api/MstSAOnboardingInstitution/GetApproverInstitutionRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.approverinstitutionraisequery_list = resp.data.approverinstitutionraisequery_list;
            });

            var url = 'api/MstSAOnboardingInstitution/GetSABureauInstitutionList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.sainstitutebureau_list = resp.data.sainstitutebureau_list;
            });
            var url = 'api/MstSAOnboardingInstitution/InstitutionTempClear';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingInstitution/SaVerifyDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;
                $scope.instverificationdocument_list = resp.data.sauploaddoc_list;
            });
            var url = 'api/MstSAOnboardingInstitution/ApprovalInitatedDetail';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                //  $scope.cbosatype = resp.data.satype_gid;
                $scope.cbosatype = resp.data.satype_name;
                $scope.cbosaentitytype = resp.data.saentitytype_gid;
                $scope.saentitytype_name = resp.data.saentitytype_name;
                $scope.txtsamunnati_associate_name = resp.data.sa_associatename;
                $scope.txtsacontact_person_first_name = resp.data.sa_contactfirstname;
                $scope.txtsacontact_person_middle_name = resp.data.sa_contactmiddlename;
                $scope.txtsacontact_person_last_name = resp.data.sa_contactlastname;
                // $scope.cboDesignation = resp.data.designation_gid;
                $scope.designation_type = resp.data.designation_type;
                $scope.txtdateofincorporation_date = resp.data.sa_dateofincorporation;
                $scope.txtcompanystart_date = resp.data.sa_companystdate;
                $scope.txtyearin_business = resp.data.sa_yearsinbusiness;
                $scope.txtmonthsin_business = resp.data.sa_monthsinbusiness;
                $scope.txtsa_pannumber = resp.data.sa_companypan;
                $scope.txtannual_turnover = resp.data.sa_annualturnover;
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
                $scope.txtRM = resp.data.sa_reportingmanager;
                $scope.txtutr_number = resp.data.sa_apputr;
                $scope.txtcredited_date = resp.data.sa_appcrediteddate;
                $scope.txtcredited_amount = resp.data.sa_appcreditedamount;
                $scope.cbotrainingstatus = resp.data.training_status;
                $scope.txtremarks = resp.data.remarks;
                $scope.lblRM = resp.data.sa_updated_by;

                $scope.txtassessmentagency_name = resp.data.assessmentagency_name;
                $scope.txtassessmentagencyrating_name = resp.data.assessmentagencyrating_name;
                $scope.ratingas_date = resp.data.ratingas_datecredit;
                $scope.lblorigination = resp.data.origination;
                $scope.interviewevalution = resp.data.interviewevalution;
                $scope.applicationform = resp.data.applicationform;
                $scope.yearsitreturns = resp.data.yearsitreturns;
                $scope.bankstatement = resp.data.bankstatement;
                $scope.kycdocuments = resp.data.kycdocuments;
                $scope.prospect = resp.data.prospect;
                $scope.vettingstatus = resp.data.vettingstatus;
                $scope.scannedcopyreception = resp.data.scannedcopyreception;
                $scope.addressproof = resp.data.addressproof;
                $scope.photographs = resp.data.photographs;
                $scope.cancelledcheckleaf = resp.data.cancelledcheckleaf;
                $scope.houseofficeverification = resp.data.houseofficeverification;
                $scope.agreementexecutiondate = resp.data.agreementexecutiondate;
                $scope.agreementexpirydate = resp.data.agreementexpirydate;
                $scope.agreementstatus = resp.data.agreementstatus;
                $scope.agroagreementexecutiondate = resp.data.agroagreementexecutiondate;
                $scope.agroagreementexpirydate = resp.data.agroagreementexpirydate;
                $scope.agroagreementstatus = resp.data.agroagreementstatus;
                $scope.bookletnumber = resp.data.bookletnumber;
                $scope.verificationremarks = resp.data.verificationremarks;
                $scope.approvalinitated_flag = resp.data.approvalinitated_flag;
                $scope.approved_date = resp.data.approved_date;
                $scope.approved_by = resp.data.approved_by;
                $scope.approval_remarks = resp.data.approval_remarks;
                $scope.approvalstatus = resp.data.approvalstatus;
                $scope.approval_flag = resp.data.approval_flag;
                $scope.txtutr_number = resp.data.sa_apputr;
                $scope.txtcredited_date = resp.data.sa_appcrediteddate;
                $scope.txtcredited_amount = resp.data.sa_appcreditedamount;
                $scope.txtassessmentagency_name = resp.data.assessmentagency_name;
                $scope.txtassessmentagencyrating_name = resp.data.assessmentagencyrating_name;
                $scope.ratingas_date = resp.data.ratingas_datecredit;
                $scope.txtreferredby = resp.data.referred_by;
                $scope.lblsamfin = resp.data.samfin_code;
                $scope.lblsamagro = resp.data.samagro_code;
                $scope.lblcodedate = resp.data.codecreation_date;
                $scope.renewal_flag = resp.data.renewal_flag;
                $scope.txtutrno = resp.data.utr_no;

                unlockUI();
            });
            unlockUI();
        }

        $scope.companydoc_downloads = function (val1, val2) {
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
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.back = function () {
            $location.url('app/MstSAOnboardingInstitutionRenewalGrouping?hash=' + cmnfunctionService.encryptURL('sacontactinstitution_gid=' + sacontactinstitution_gid + '&saentitytype_gid=' + saentitytype_gid + '&samfin_code=' + samfin_code + '&samagro_code=' + samagro_code ));

        }
        $scope.mycampaignquery_close = function (makerinstitutionraisequery_gid) {
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
                        makerinstitutionraisequery_gid: makerinstitutionraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        sacontactinstitution_gid: $location.search().lssacontactinstitution_gid
                    }
                    var url = 'api/MstSAOnboardingInstitution/PostMakerInstitutionresponsequery';
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
        $scope.bureau_view = function (sainstitution2bureau_gid) {
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
                    sainstitution2bureau_gid: sainstitution2bureau_gid
                }

                var url = 'api/MstSAOnboardingInstitution/SABureauView';

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

                var url = 'api/MstSAOnboardingInstitution/SAUploadIndDocList';
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
                $scope.downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
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
        $scope.bureau_delete = function (sainstitution2bureau_gid) {
            var params = {
                sainstitution2bureau_gid: sainstitution2bureau_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteContactBureau';
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
       
        $scope.checkerinstitutionquery_close = function (checkerinstitutionraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/checkerinstitutionqueryClose.html',
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
                        checkerinstitutionraisequery_gid: checkerinstitutionraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        sacontactinstitution_gid: $location.search().lssacontactinstitution_gid
                        //   sacontact_gid: $location.search().lssacontact_gid
                    }
                    var url = 'api/MstSAOnboardingInstitution/PostCheckerInstitutionresponsequery';
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
        $scope.downloadall1 = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.submit_renewal = function () {
            //var lssatype_gid = '';
            //var lssatype_name = '';
            //var lssaentitytype_gid = '';
            //var lssaentitytype_name = '';
            //var lsdesignation_gid = '';
            //var lsdesignation_type = '';
            //var lsassessmentagency_gid = '';
            //var lsassessmentagency_name = '';
            //var lsassessmentagencyrating_gid = '';
            //var lsassessmentagencyrating_name = '';



            //if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
            //    lsdesignation_gid = $scope.cboDesignation.designation_gid;
            //    lsdesignation_type = $scope.cboDesignation.designation_type;
            //}
            //if ($scope.cbosatype != undefined || $scope.cbosatype != null) {
            //    lssatype_gid = $scope.cbosatype.satype_gid;
            //    lssatype_name = $scope.cbosatype.satype_name;
            //}
            //if ($scope.cbosaentitytype != undefined || $scope.cbosaentitytype != null) {
            //    lssaentitytype_gid = $scope.cbosaentitytype.saentitytype_gid;
            //    lssaentitytype_name = $scope.cbosaentitytype.saentitytype_name;
            //}



            //if ($scope.cbosaassessmentagency != undefined || $scope.cbosaassessmentagency != null) {
            //    lsassessmentagency_gid = $scope.cbosaassessmentagency.assessmentagency_gid;
            //    lsassessmentagency_name = $scope.cbosaassessmentagency.assessmentagency_name;
            //}
            //if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
            //    lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
            //    lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
            //}
            var params = {
                //satype_gid: 'MSAG202206294',
                //satype_name: 'Company',
                ////satype_gid: lssatype_gid,
                //sa_reportingmanager: $scope.txtRM,
                //reportingmanager_gid: $scope.txtRMGid,
                //// satype_name: lssatype_name,
                //saentitytype_name: lssaentitytype_name,
                ////saentitytype_gid: lssaentitytype_gid,
                //// sa_designation: $scope.cbodesignation,
                //designation_gid: lsdesignation_gid,
                //designation_type: lsdesignation_type,
                //sa_associatename: $scope.txtsamunnati_associate_name,
                //sa_contactfirstname: $scope.txtsacontact_person_first_name,
                //sa_contactmiddlename: $scope.txtsacontact_person_middle_name,
                //sa_contactlastname: $scope.txtsacontact_person_last_name,
                //sa_dateofincorporation: $scope.txtdateofincorporation_date,
                //sa_annualturnover: $scope.txtannual_turnover,
                //sa_companypan: $scope.txtsa_pannumber,
                //sa_companystdate: $scope.txtcompanystart_date,
                //sa_yearsinbusiness: $scope.txtyearin_business,
                //sa_monthsinbusiness: $scope.txtmonthsin_business,
                //// pan_number: $scope.txtpan_number,
                //sa_startdate: $scope.txtstart_date,
                //sa_enddate: $scope.txtend_date,
                //saifsc_code: $scope.txtifsc_code,
                //city: $scope.txtcity,
                //district: $scope.txtdistrict,
                //state: $scope.txtstate,
                //micr: $scope.txtmicr,
                //branch_address: $scope.txtbranch_address,
                //saaccount_number: $scope.txtbankaccount_number,
                //confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                //saaccountholder_name: $scope.txtaccountholder_name,
                //sacanccheque_number: $scope.txtcancelledcheque_number,
                //sabank_name: $scope.txtbank_name,
                //sabranch_name: $scope.txtbranch_name,
                //approvalstatus: 'Submitted For Renewal',
                //sa_apputr: $scope.txtutr_number,
                //sa_appcrediteddate: $scope.txtcredited_date,
                //sa_appcreditedamount: $scope.txtcredited_amount,

                //assessmentagency_gid: lsassessmentagency_gid,
                //assessmentagency_name: lsassessmentagency_name,
                //assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                //assessmentagencyrating_name: lsassessmentagencyrating_name,
                //ratingas_date: $scope.txtrasteason_date,
                //rdbgstregister_status: $scope.rdbgstregister_status,
                sacontactinstitution_gid: sacontactinstitution_gid,
                saentitytype_gid: saentitytype_gid,
            }
          
            var url = 'api/MstSAOnboardingInstitution/InstitutionRenewal';

            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/MstSAOnboardingInstitutionRenewalGrouping?hash=' + cmnfunctionService.encryptURL('sacontactinstitution_gid=' + sacontactinstitution_gid + '&saentitytype_gid=' + saentitytype_gid + '&samfin_code=' + samfin_code + '&samagro_code=' + samagro_code));
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

        

        $scope.view_campaignquerydesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/makerDescriptionView.html',
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
                templateUrl: '/checkerDescriptionView.html',
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
        $scope.view_approverquerydesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/approverDescriptionView.html',
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


        $scope.InstitutionDocumentUpload = function (val, val1, name) {
            if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.cboDocumentName == null) || ($scope.cboDocumentName == '') || ($scope.cboDocumentName == undefined)) {
                $("#file").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
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

                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cboDocumentName.sadocumentlist_name);
                frm.append('companydocument_gid', $scope.cboDocumentName.sadocumentlist_gid);
                //frm.append('document_id', $scope.txtdocument_id);
                frm.append('institution_gid', sacontactinstitution_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
            }
        }
        $scope.institutionDoc_upload = function () {

            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstSAOnboardingInstitution/InstitutionUploadDocument';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    var params = {
                        sacontactinstitution_gid: sacontactinstitution_gid

                    }
                    var url = 'api/MstSAOnboardingInstitution/DocumentUploadList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.renewaldocument_list = resp.data.renewaldocument_list;
                    });

                    unlockUI();

                    $("#file").val('');
                    $scope.cboDocumentName = "";
                    $scope.txtdocument_id = "";
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


        $scope.deletedocument = function (sainstidocument_gid) {
            lockUI();
            var params = {
                sainstidocument_gid: sainstidocument_gid
            }
            var url = 'api/MstSAOnboardingInstitution/UploadDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                //  $scope.documentupload_list = resp.data.documentupload_list;
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $("#pan_document").val('');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                docupload_list();
                unlockUI();
            });
        }
        $scope.downloadall1 = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.uploaddownloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.uploaddocumentviewer = function (val1, val2) {
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
        function docupload_list() {
            var params = {
                sacontactinstitution_gid: sacontactinstitution_gid
            }
            var url = 'api/MstSAOnboardingInstitution/UploadDocumentTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.panlist = resp.data.panlist;
            });
        }
        $scope.doc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewerinstitution = function (val1, val2) {
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
        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }

    }
})();