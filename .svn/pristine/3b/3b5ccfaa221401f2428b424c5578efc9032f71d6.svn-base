(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprStartCreditUnderwritingController', AgrTrnSuprStartCreditUnderwritingController);

    AgrTrnSuprStartCreditUnderwritingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function AgrTrnSuprStartCreditUnderwritingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService,cmnfunctionService) {

        var vm = this;
        vm.title = 'AgrTrnSuprStartCreditUnderwritingController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.appcreditapproval_gid = $location.search().appcreditapproval_gid;
        var appcreditapproval_gid = $scope.appcreditapproval_gid;
        $scope.lstab = $location.search().lstab;
        var lstab = $scope.lstab;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.product_gid = $location.search().product_gid;
        var product_gid = $scope.product_gid;
        $scope.variety_gid = $location.search().variety_gid;
        var variety_gid = $scope.variety_gid;

        lockUI();
        activate();
        function activate() {
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/AgrMstSuprApplicationView/GetApplicationBasicView';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtcustomer_urn = resp.data.customer_urn;
                $scope.txtvertical = resp.data.vertical_name;
                $scope.txtvertical_tag = resp.data.verticaltaggs_name;
                $scope.txtconstitution = resp.data.constitution_name;
                $scope.txt_strategicbusiness_unit = resp.data.businessunit_name;
                $scope.txtprimayvalue_chain = resp.data.primaryvaluechain_name;
                $scope.txtsecondaryvalue_chain = resp.data.secondaryvaluechain_name;
                $scope.txtvernacular_language = resp.data.vernacular_language;
                $scope.txtApplfrom_SA = resp.data.sa_status;
                $scope.txtSAM_associateID = resp.data.sa_id;
                $scope.txtSAM_associatename = resp.data.sa_name;
                $scope.txtcontactperson_name = resp.data.contactperson_name;
                $scope.txtbasicdesignation = resp.data.designation_type;
                $scope.txtlandline_number = resp.data.landline_no;
                $scope.txtsocial_capital = resp.data.social_capital;
                $scope.txttrade_capital = resp.data.trade_capital;
                $scope.borrower_flag = resp.data.borrower_flag;
                $scope.borrower_type = resp.data.borrower_type;
                $scope.lblapproval_status = resp.data.approval_status;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.product_gid = resp.data.product_gid;
                $scope.variety_gid = resp.data.variety_gid;
                $scope.txtproduct_name = resp.data.product_name;
                $scope.txtsector_name = resp.data.sector_name;
                $scope.txtcategory_name = resp.data.category_name;
                $scope.txtvariety_name = resp.data.variety_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;
                $scope.txtprogram_name = resp.data.program_name;
                unlockUI();
            });

            var url = 'api/AgrMstSuprApplicationView/GetGeneticDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.geneticcode_list = resp.data.geneticdetails_list;
            });

            var url = 'api/AgrMstSuprApplicationView/GetMobileMailDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtprimary_number = resp.data.primary_mobileno;
                $scope.basicmobileno_list = resp.data.mobilenumber_list;
                $scope.txtprimary_emailassdress = resp.data.primary_email;
                $scope.mailaddress_list = resp.data.mail_list;
            });

            var url = 'api/AgrMstSuprApplicationView/GetProductChargesDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;
                $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;
                $scope.loandtls_list = resp.data.mstLoan_list;
                $scope.buyer_list = resp.data.mstbuyer_list;
                $scope.txtpurposeof_loan = resp.data.enduse_purpose;
                $scope.txt_processingfee = resp.data.processing_fee;
                $scope.txtprocessing_collecttype = resp.data.processing_collectiontype;
                $scope.txtdoc_charges = resp.data.doc_charges;
                $scope.txtdoc_collecttype = resp.data.doccharge_collectiontype;
                $scope.txtfield_visitcharges = resp.data.fieldvisit_charge;
                $scope.txtfield_collecttype = resp.data.fieldvisit_collectiontype;
                $scope.txtadhoc_fee = resp.data.adhoc_fee;
                $scope.txtadhoccollection_type = resp.data.adhoc_collectiontype;
                $scope.txtlife_insurance = resp.data.life_insurance;
                $scope.txtlife_collectiontype = resp.data.lifeinsurance_collectiontype;
                $scope.txtaccident_insurance = resp.data.acct_insurance;
                $scope.txttotal_collectible = resp.data.total_collect;
                $scope.txttotal_deductible = resp.data.total_deduct;
                $scope.Collateral_list = resp.data.mstcollateral_list;
                $scope.txtproduct_type = resp.data.product_type;
                $scope.servicecharge_List = resp.data.servicecharge_List;
                $scope.txtsecurity_type = resp.data.security_type;
                $scope.txtsecurity_description = resp.data.security_description;
                $scope.txtsecurity_value = resp.data.security_value;
                $scope.txtsecurityassessed_date = resp.data.securityassessed_date;
                $scope.txtasset_id = resp.data.asset_id;
                $scope.txtroc_fillingid = resp.data.roc_fillingid;
                $scope.txtCERSAI_fillingid = resp.data.CERSAI_fillingid;
                $scope.txthypoobservation_summary = resp.data.hypoobservation_summary;
                $scope.txtprimary_security = resp.data.primary_security;
                $scope.application2hypothecation_gid = resp.data.application2hypothecation_gid;
            });

            var url = 'api/AgrMstSuprApplicationView/GetIndividualList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditIndividual_List = resp.data.individual_List;
            });

            var url = 'api/AgrMstSuprApplicationView/GetInstitutionList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditInstitution_List = resp.data.institution_List;
            });

            var url = 'api/AgrMstSuprApplicationView/GetRMDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtrmdepartment_name = resp.data.department_name;
                $scope.txtrm_name = resp.data.RM_Name;
                $scope.txtappl_initiateddate = resp.data.applicationinitiated_date;
            });
            //Get CAM Document 
            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCAM';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.camuploaddocument_list = resp.data.camdocument_list;
            });

            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/AgrTrnSuprCreditApproval/Getcreditheadsview';
            SocketService.getparams(url, params).then(function (resp) {
                lockUI();
                $scope.txtcredit_head = resp.data.credithead_name;
                $scope.txtnational_manager = resp.data.nationalcredit_name;
                $scope.txtregional_manager = resp.data.regionalcredit_name;
                $scope.txtcredit_manager = resp.data.creditmanager_name;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.remarks = resp.data.remarks;
                unlockUI();
            });

            var url = 'api/AgrTrnSuprCreditApproval/GetApprmquerysSummary';
            SocketService.getparams(url, params).then(function (resp) {
                lockUI();
                $scope.appcreditquerylist = resp.data.appcreditquerylist;
                unlockUI();
            });


            var url = 'api/AgrTrnSuprCreditApproval/GetAppqueryStatus';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.querystatus_flag = resp.data.querystatus_flag;
                $scope.submitapproval_flag = resp.data.submitapproval_flag;

            });

        }

        $scope.Back = function () {
            if (lspage == "myapp") {
                $state.go('app.AgrMstSuprMyApplicationsSummary');
            }
            else if (lspage == "submittoapp") {
                $state.go('app.AgrMstSuprSubmittedtoApprovalSummary');
            }
            else if (lspage == "submittocc") {
                $state.go('app.AgrMstSuprSubmittedtoCCSummary');
            }
            else if (lspage == "rejecthold") {
                $state.go('app.AgrMstSuprRejectandHoldSummary');
            }
            else {
                $state.go('app.AgrMstSuprMyApplicationsSummary');
            }
        }
        $scope.Kyc_view = function (institution_gid) {
            var application_gid = $scope.application_gid;
            $location.url('app/AgrTrnSuprCreditUnderwritingKycLogView?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
        }
        $scope.CAM_generate = function () {
            localStorage.setItem('RefreshTemplate', 'N');
            var application_gid = $scope.application_gid;
            $location.url('app/AgrTrnSuprCAMGenerate?application_gid=' + application_gid);
        }
        $scope.appcreagradingtool_view = function (application2gradingtool_gid) {
            var application2gradingtool_gid = application2gradingtool_gid;
            localStorage.setItem('application2gradingtool_gid', application2gradingtool_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnSuprApplCreationGradingToolView";
            window.open(URL, '_blank');
        }

        $scope.appcreavisitreport_view = function (visitreport_gid) {
            var visitreport_gid = visitreport_gid;
            localStorage.setItem('visitreport_gid', visitreport_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnSuprApplCreationVisitReportView";
            window.open(URL, '_blank');
        }

        $scope.uploadeddoc_Collateral = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Collateraldocuments.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       application2loan_gid: application2loan_gid
                   }
                var url = 'api/AgrMstSuprApplicationView/GetCollateralDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.Collateraldoc_list = resp.data.CollatralDocumentList;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_Collateraldoc = function (val1, val2) {
                    //var phyPath = val1;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall_3 = function () {
                    for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.UploadDocumentList[i].document_path, $scope.UploadDocumentList[i].document_name);
                    }
                }

            }

        }
        $scope.submitto_approval = function () {
            lockUI();
            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnSuprCreditApproval/Getappcreditapprovalinitiate';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    if (lspage == "myapp") {
                        $state.go('app.AgrMstSuprMyApplicationsSummary');
                    }
                    else {
                        $state.go('app.AgrMstSuprSubmittedtoApprovalSummary');
                    }
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });

        }
        $scope.uploadeddoc_Hypothecation = function (application2hypothecation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Hypothecationdocuments.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      application2hypothecation_gid: application2hypothecation_gid
                  }
                var url = 'api/AgrMstSuprApplicationView/GetHypoDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.Hypothecationdoc_list = resp.data.HypoDocumentList;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.download_Hypothecationdoc = function (val1, val2) {
                    //var phyPath = val1;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall_2 = function () {
                    for (var i = 0; i < $scope.Hypothecationdoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.Hypothecationdoc_list[i].document_path, $scope.Hypothecationdoc_list[i].document_name);
                    }
                }

            }

        }

        $scope.PurposeofLoanOther_view = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/PurposeOfLoanOther.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    application2loan_gid: application2loan_gid,
                    application_gid: application_gid
                }
                var url = 'api/AgrMstSuprApplicationView/GetLoanProgramValueChain';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.program = resp.data.program;
                    $scope.primaryvaluechain_name = resp.data.primaryvaluechain_name;
                    $scope.secondaryvaluechain_name = resp.data.secondaryvaluechain_name;
                    $scope.otherdetails = resp.data;
                    $scope.mstproductdtl_list = resp.data.mstproductdtl_list;
                });

                var params =
                   {
                       application2loan_gid: application2loan_gid
                   }
                var url = 'api/AgrMstSuprApplicationView/GetPurposeofLoan';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtpurposeof_loan = resp.data.enduse_purpose;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }


        $scope.Buyer_view = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/BuyerDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      application2loan_gid: application2loan_gid
                  }
                var url = 'api/AgrMstSuprApplicationView/GetLoantoBuyerList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.buyer_list = resp.data.mstbuyer_list;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.group_view = function (group_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/GroupView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    group_gid: group_gid
                }

                lockUI();
                var url = 'api/AgrMstSuprApplicationEdit/EditGroup';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtgroup_name = resp.data.group_name;
                    $scope.txtdate_formation = resp.data.date_of_formation;
                    $scope.txtgroup_type = resp.data.group_type;
                    $scope.txtmember_count = resp.data.groupmember_count;
                    $scope.txtmember_URN = resp.data.group_urn;
                    $scope.groupurn_status = resp.data.groupurn_status;
                });

                var params = {
                    group_gid: group_gid
                }
                lockUI();
                var url = 'api/AgrMstSuprApplicationEdit/GroupAddressList';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.memberaddress_list = resp.data.mstaddress_list;
                });

                var params = {
                    group_gid: group_gid
                }

                lockUI();
                var url = 'api/AgrMstSuprApplicationEdit/GroupBankList';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.memberbank_list = resp.data.mstbank_list;
                });

                var params = {
                    group_gid: group_gid
                }
                lockUI();
                var url = 'api/AgrMstSuprApplicationEdit/GroupDocumentList';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.UploadMemberDocumentList = resp.data.groupdocument_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.group_docs = function (val1, val2) {
                    //var phyPath = val1;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

            }

        }

        $scope.member_view = function (contact_gid) {
            var contact_gid = contact_gid;
            localStorage.setItem('contact_gid', contact_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnApplGroupMemberdtlView";
            window.open(URL, '_blank');
        }

        var params =
         {
             application_gid: application_gid
         }
        var url = "api/AgrMstSuprApplicationEdit/GetGroupSummary";
        SocketService.getparams(url, params).then(function (resp) {
            $scope.group_list = resp.data.group_list;
            angular.forEach($scope.group_list, function (value, key) {
                var params = {
                    group_gid: value.group_gid
                };

                var url = 'api/AgrMstSuprApplicationView/GetGrouptoMemberList';
                SocketService.getparams(url, params).then(function (resp) {
                    value.groupmember_list = resp.data.groupmember_list;
                    value.expand = false;
                });
            });
        });

        $scope.appcreainstitution_view = function (institution_gid) {
            var institution_gid = institution_gid;
            localStorage.setItem('institution_gid', institution_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnSuprRMInstitutionView";
            window.open(URL, '_blank');
        }

        $scope.appcreaindividual_view = function (contact_gid) {
            var contact_gid = contact_gid;
            localStorage.setItem('contact_gid', contact_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnSuprRMIndividualView";
            window.open(URL, '_blank');
        }

        $scope.creditinstitution_view = function (institution_gid) {
            var institution_gid = institution_gid;
            localStorage.setItem('institution_gid', institution_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnSuprCreditCompanyDtlView";
            window.open(URL, '_blank');
        }

        $scope.creditindividual_view = function (contact_gid) {
            var contact_gid = contact_gid;
            localStorage.setItem('contact_gid', contact_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnSuprCreditIndividualDtlView";
            window.open(URL, '_blank');
        }

        $scope.creditgroup_view = function (group_gid) {
            var group_gid = group_gid;
            localStorage.setItem('group_gid', group_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnSuprCreditGroupDtlView";
            window.open(URL, '_blank');
        }

        $scope.creditmember_view = function (contact_gid) {
            var contact_gid = contact_gid;
            localStorage.setItem('contact_gid', contact_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnSuprCreditIndividualDtlView";
            window.open(URL, '_blank');
        }

        $scope.creditinstitution_add = function (institution_gid) {
            $location.url('app/AgrTrnSuprDocumentCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=StartCreditUnderwriting&lstype=Institution');
        }

        $scope.creditindividual_add = function (contact_gid) {
            $location.url('app/AgrTrnSuprIndividualDocCheckList?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=StartCreditUnderwriting&lstype=Individual');
        }

        $scope.creditgroup_add = function (group_gid) {
            $location.url('app/AgrTrnSuprGroupDocCheckList?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=StartCreditUnderwriting&lstype=Group');
        }

        $scope.creditmember_add = function (contact_gid) {
            $location.url('app/AgrTrnSuprIndividualDocCheckList?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=StartCreditUnderwriting&lstype=Individual');
        }

        $scope.creditgeneraldtl_edit = function (product_gid, variety_gid) {
            $location.url('app/AgrTrnSuprCreditGeneralDtlEdit?application_gid=' + application_gid + '&lspage=StartCreditUnderwriting' + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
        }

        $scope.applcreainstitution_edit = function (institution_gid) {
            $location.url('app/AgrTrnSuprCreditInstitutionDtlEdit?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=StartCreditUnderwriting');
        }

        $scope.appcreaindividual_edit = function (contact_gid) {
            $location.url('app/AgrTrnSuprCreditIndividualDtlEdit?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=StartCreditUnderwriting');
        }

        $scope.appcreagroup_edit = function (group_gid) {
            $location.url('app/AgrTrnSuprCreditGroupDtlEdit?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=StartCreditUnderwriting');
        }

        $scope.appcreamember_edit = function (contact_gid) {
            $location.url('app/AgrTrnSuprCreditIndividualDtlEdit?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=StartCreditUnderwriting');
        }

        $scope.productcharges_edit = function (product_gid, variety_gid) {
            $location.url('app/AgrTrnSuprCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage=StartCreditUnderwriting' + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
        }

        $scope.hypothecation_edit = function () {
            $location.url('app/AgrTrnSuprCreditHypothecationEdit?application_gid=' + application_gid + '&lspage=StartCreditUnderwriting');
        }
        $scope.submit = function () {
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrTrnSuprAppCreditUnderWriting/PostUnderwrite';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lspage == "myapp") {
                        $state.go('app.AgrMstSuprMyApplicationsSummary');
                    }
                    else {
                        $state.go('app.AgrMstSuprSubmittedtoApprovalSummary');
                    }
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
        //CAM Document Upload

        $scope.camdocumentUpload = function (val) {
            if (($scope.txtcamdocument_title == null) || ($scope.txtcamdocument_title == '') || ($scope.txtcamdocument_title == undefined)) {
                $("#momdocument").val('');
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
                    frm.append('document_title', $scope.txtcamdocument_title);
                    frm.append('application_gid', $scope.application_gid);
                    frm.append('project_flag', "documentformatonly");
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

                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/AgrTrnSuprAppCreditUnderWriting/CAMocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.camuploaddocument_list = resp.data.camdocument_list;
                        unlockUI();

                        $("#camdocument").val('');
                        $scope.uploadfrm = undefined;

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.$parent.txtcamdocument_title = '';
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

        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.deleteCAM = function (val) {
            var params = {
                application2camdoc_gid: val,
                application_gid: application_gid
            };

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/CAMdoc_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.camuploaddocument_list = resp.data.camdocument_list;

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }

        $scope.view_Querydescription = function (appcreditquery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                 {
                     appcreditquery_gid: appcreditquery_gid
                 }
                var url = 'api/AgrTrnSuprCreditApproval/GetAppcreditqueryesc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquery_desc = resp.data.querydesc;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }


        $scope.create_Query = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addquery.html',
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
                $scope.query_add = function () {
                    var params = {
                        querytitle: $scope.txtquery_title,
                        querydesc: $scope.txtquery_desc,
                        application_gid: application_gid,
                        appcreditapproval_gid: appcreditapproval_gid,
                        queryraised_to: 'RM'
                    }
                    var url = 'api/AgrTrnSuprCreditApproval/PostAppcreditqueryadd';
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

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.camuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.camuploaddocument_list[i].document_path, $scope.camuploaddocument_list[i].document_name);
            }
        }
        $scope.downloadall_2 = function () {
            for (var i = 0; i < $scope.Hypothecationdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.Hypothecationdoc_list[i].document_path, $scope.Hypothecationdoc_list[i].document_name);
            }
        }
        $scope.downloadall_3 = function () {
            for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadDocumentList[i].document_path, $scope.UploadDocumentList[i].document_name);
            }
        }
        $scope.downloadall_8 = function () {
            for (var i = 0; i < $scope.UploadMemberDocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadMemberDocumentList[i].document_path, $scope.UploadMemberDocumentList[i].document_name);
            }
        }

    }
})();
