(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCcCommitteeApplicationViewController', AgrTrnSuprCcCommitteeApplicationViewController);

    AgrTrnSuprCcCommitteeApplicationViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService'];

    function AgrTrnSuprCcCommitteeApplicationViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCcCommitteeApplicationViewController';

        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lstab = $location.search().lstab;
        var lstab = $scope.lstab;
       lockUI();
        activate();
        function activate() {

            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            var params = {
                application_gid: $scope.application_gid
            }

            var url =  'api/AgrMstSuprApplicationView/GetApplicationBasicView';

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
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.txtproduct_name = resp.data.product_name;
                $scope.txtsector_name = resp.data.sector_name;
                $scope.txtcategory_name = resp.data.category_name;
                $scope.txtvariety_name = resp.data.variety_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;
                $scope.txtprogram_name = resp.data.program_name;
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

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM'

            }
            var url = 'api/AgrMstSuprApplicationView/GetVisitReportList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.VisitReport_List = resp.data.VisitReport_List;
            });
            
            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM'
            }
            var url = 'api/AgrMstSuprApplicationView/GetGradingToolDtls';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.gradetoolsummary_list = resp.data.mstgradetoolsummary_list;
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
                $scope.txtunderwritten_date = resp.data.creditunderwritten_date;
                $scope.txtunderwritten_by = resp.data.creditunderwritten_by;
            });

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'Credit',
            }
            var url = 'api/AgrMstApplicationGradingTool/GetGradingTool';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.gradingtoolcredit_list = resp.data.grading_list;

            });

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'Credit',
            }
            var url = 'api/AgrMstApplicationVisitReport/GetVisitReportList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.VisitReportCreditList = resp.data.VisitReportList;

            });

            //Get CAM Document
            var url = 'api/AgrTrnAppCreditUnderWriting/GetCAM';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.camuploaddocument_list = resp.data.camdocument_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = "api/AgrTrnCAMGeneration/GetApp2CAM"
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lspath = resp.data.lspath;
                $scope.lsname = resp.data.lsname;
            });

        }

        var params = 
        {
           application_gid : application_gid
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
                       application2hypothecation_gid : application2hypothecation_gid
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
                    ////var phyPath = val1;
                    ////var relPath = phyPath.split("EMS");
                    ////var relpath1 = relPath[1].replace("\\", "/");
                    ////var hosts = window.location.host;
                    ////var prefix = location.protocol + "//";
                    ////var str = prefix.concat(hosts, relpath1);
                    ////var link = document.createElement("a");
                    ////link.download = val2;
                    ////var uri = str;
                    ////link.href = uri;
                    ////link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall_3 = function () {
                    for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
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
                var url = 'api/AgrMstSuprApplicationView/GetLoanProgramValueChain';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.program = resp.data.program;
                    $scope.primaryvaluechain_name = resp.data.primaryvaluechain_name;
                    $scope.secondaryvaluechain_name = resp.data.secondaryvaluechain_name;
                    $scope.product_gid = resp.data.product_gid;
                    $scope.product_name = resp.data.product_name;
                    $scope.variety_gid = resp.data.variety_gid;
                    $scope.variety_name = resp.data.variety_name;
                    $scope.sector_name = resp.data.sector_name;
                    $scope.category_name = resp.data.category_name;
                    $scope.botanical_name = resp.data.botanical_name;
                    $scope.alternative_name = resp.data.alternative_name;
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
                    application2loan_gid : application2loan_gid
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
        
        $scope.Back = function () {          
            if (lspage == 'ScheduleMeeting') {
                $location.url('app/AgrTrnSuprCreditCommitteeSummary');
            }
            else if (lspage == 'ScheduledMeetingsummary') {
                $state.go('app.AgrTrnSuprCCscheduledSummary');
            } 
            else if (lspage == 'CompletedMeetingsummary') {
                $state.go('app.AgrTrnSuprCCCompletedSummary');
            } 
            else if (lspage == 'CCMmeetingScheduledcompleted') {
                $state.go('app.AgrTrnSuprCcCompletedScheduledMeeting');
            }
            else if (lstab == 'RejectHoldAppl') {
                $state.go('app.AgrTrnSuprRejectandHoldSummary');
            }
            else if (lstab == 'CCSkippedAppl') {
                $state.go('app.AgrTrnSuprCCSkippedApplicationSummary');
            }
            else if (lstab == 'SubmittedToApproval') {
                $state.go('app.AgrTrnSuprSubmittedtoApprovalSummary');
            }
            else if (lstab == 'SubmittedToCC') {
                $state.go('app.AgrTrnSuprSubmittedtoCCSummary');
            }
            else if (lstab == 'CreditApproval') {
                $state.go('app.AgrTrnSuprCreditApprovalSummary');
            }
            else if (lstab == 'CreditApproved') {
                $state.go('app.AgrTrnSuprCreditApprovedSummary');
            }
            else if (lstab == 'CreditSubmittedtoCC') {
                $state.go('app.AgrTrnSuprCreditSubmittedtoCCSummary');
            }
            else if (lstab == 'CreditCCSkipped') {
                $state.go('app.AgrTrnSuprCreditCCSkippedSummary');
            }
            else if (lstab == 'CreditRejectHold') {
                $state.go('app.AgrTrnSuprCreditRejectandHoldSummary');
            }
            else if (lstab == 'Pencreditmapping') {
                $state.go('app.AgrTrnSuprApplicationAssignmentSummary');
            }
            else if (lstab == 'Asscreditmapping') {
                $state.go('app.AgrTrnSuprAppassignedAssignmentSummary');
            }
            else if (lstab == 'ApplSubmittedToCC') {
                $state.go('app.AgrTrnSuprApplSubmittedtoCCSummary');
            }
            else if (lspage == 'SentBackToCredit') {
                $state.go('app.AgrTrnSuprSentbackcctoCredit');
            }
            else {
                $state.go('app.AgrTrnSuprCcScheduledMeetingSummary');
            }  
        }
        
        $scope.Kyc_view = function () {
            var application_gid = $scope.application_gid;
            $location.url('app/AgrMstSuprCcCommitteeKycView?application_gid=' + application_gid + '&lspage=' + lspage);
        }

        $scope.gradingtool_view = function (application2gradingtool_gid) {
            var application2gradingtool_gid=application2gradingtool_gid;
            localStorage.setItem('application2gradingtool_gid', application2gradingtool_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrMstSuprApplCreationGradingToolView";
            window.open(URL, '_blank');
        }

        $scope.visitreport_view = function (visitreport_gid) {
            var visitreport_gid=visitreport_gid;
            localStorage.setItem('visitreport_gid', visitreport_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrMstSuprApplCreationVisitReportView";
            window.open(URL, '_blank');
        }

        $scope.institution_view = function (institution_gid) {
            var institution_gid = institution_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('institution_gid', institution_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrMstSuprCcCommitteeInstitutionView";
            window.open(URL, '_blank');
        }

        $scope.individual_view = function (contact_gid) {
            var contact_gid = contact_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('contact_gid', contact_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrMstSuprCcCommitteeIndividualView";
            window.open(URL, '_blank');
        }

        $scope.group_view = function (group_gid) {
            var group_gid = group_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('group_gid', group_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrMstSuprCcCommitteeGroupView";
            window.open(URL, '_blank');
        }

        $scope.member_view = function (contact_gid) {
            var contact_gid = contact_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('contact_gid', contact_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrMstSuprCcCommitteeIndividualView";
            window.open(URL, '_blank');
        }

        $scope.downloadsdocument = function (val1, val2) {
            //var phyPath = val1;
            //console.log(val1)
            //var relPath = phyPath.split("EMS");
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
                DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
            }
        }

    }
})();
