(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCcCommitteeInstitutionViewController', MstCcCommitteeInstitutionViewController);

        MstCcCommitteeInstitutionViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', 'DownloaddocumentService','cmnfunctionService' ];

    function MstCcCommitteeInstitutionViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, DownloaddocumentService,cmnfunctionService ) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCcCommitteeInstitutionViewController';
        var institution_gid = localStorage.getItem('institution_gid');
        var application_gid = localStorage.getItem('application_gid');

        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lspagetype = $location.search().lspagetype;
        var lspagetype = $scope.lspagetype;

        const lspagename = 'MstCcCommitteeInstitutionView';

        lockUI();
        activate();

        function activate() {

            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
            
            var params = {
                institution_gid: institution_gid
            }

            var url = 'api/MstApplicationView/GetGurantorInstitutionView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcompany_name = resp.data.company_name;
                $scope.txtborrower_type = resp.data.borrower_type;
                $scope.txtCIN_number = resp.data.cin_no;
                $scope.txtcompanyPAN_number = resp.data.companypan_no;
                $scope.txtincorporation_date = resp.data.date_incorporation;
                $scope.txtbusiness_year = resp.data.year_business;
                $scope.txtbusiness_month = resp.data.month_business;
                $scope.txtcompany_type = resp.data.companytype_name;
                $scope.txtescrow = resp.data.escrow;
                $scope.txtlastyear_turnover = resp.data.lastyear_turnover;
                $scope.lbllastyearseperator = (parseInt($scope.txtlastyear_turnover.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lbllastyearwords = defaultamountwordschange($scope.lbllastyearseperator);
                $scope.txtstart_date = resp.data.start_date;
                $scope.txtend_date = resp.data.end_date;
                $scope.txtofficial_teleno = resp.data.official_telephoneno;
                $scope.txtofficial_mailaddress = resp.data.officialemail_address;
                $scope.gst_list = resp.data.mstgst_list;
                $scope.txtcredit_assessmentagency = resp.data.assessmentagency_name;
                $scope.txtassessment_rating = resp.data.assessmentagencyrating_name;
                $scope.txtrating_on = resp.data.ratingas_on;
                $scope.txtAML_category = resp.data.amlcategory_name;
                $scope.txtbusiness_category = resp.data.businesscategory_name;
                $scope.txtinstituionprimary_number = resp.data.primaryinstitution_mobileno;
                $scope.instituionmobile_list = resp.data.instituionmobilenumber_list;
                $scope.txtinstituionprimary_emailaddress = resp.data.primaryinstitution_email;
                $scope.instituionmailaddress_list = resp.data.mail_list;
                $scope.instituionaddress_list = resp.data.mstaddress_list;
                $scope.institutionform60_list = resp.data.institutionform60_list;
                $scope.institutiondoc_list = resp.data.institutiondoc_list;
                $scope.mstlicense_list = resp.data.mstlicense_list;
                $scope.bureauname_gid = resp.data.bureauname_gid;
                $scope.txbureau_name = resp.data.bureauname_name;
                $scope.txtbureau_score = resp.data.bureau_score;
                $scope.txtscore_on = resp.data.bureau_response;
                $scope.txtobservations = resp.data.observations;
                $scope.txtbureau_response = resp.data.bureauscore_date;
                $scope.cicdocument_name = resp.data.cicdocument_name;
                $scope.cicdocument_path = resp.data.cicdocument_path;
                $scope.txturn_status = resp.data.urn_status;
                $scope.txturn = resp.data.urn;
                $scope.txtcontact_firstname = resp.data.contactperson_firstname;
                $scope.txtcontact_middlename = resp.data.contactperson_middlename;
                $scope.txtcontact_lastname = resp.data.contactperson_lastname;
                $scope.txtdesignation = resp.data.designation;
                $scope.txtbusinessstart_date = resp.data.businessstart_date;
                $scope.borrowerinstitution_gid = resp.data.institution_gid;
                $scope.txtnearsamunnatiabranch_gid = resp.data.nearsamunnatiabranch_gid;
                $scope.txtnearsamunnati_branch = resp.data.nearsamunnatiabranch_name;
                $scope.txtudhayam_registration = resp.data.udhayam_registration;
                $scope.txttan_number = resp.data.tan_number;
                $scope.txtbusiness_description = resp.data.business_description;
                $scope.txttanstate_gid = resp.data.tanstate_gid;
                $scope.txttan_state = resp.data.tanstate_name;
                $scope.txtinternalrating_gid = resp.data.internalrating_gid;
                $scope.txtinternal_rating = resp.data.internalrating_name;
                $scope.mstlivestockholding_list = resp.data.mstlivestockholding_list;
                $scope.mstequipmentholding_list = resp.data.mstequipmentholding_list;
                $scope.mstreceivable_list = resp.data.mstreceivable_list;
                $scope.city_name = resp.data.city_name;
                $scope.txtcalamities_prone = resp.data.calamities_prone;
                $scope.txtsales = resp.data.sales;
                $scope.txtpurchase = resp.data.purchase;
                $scope.txtcredit_summation = resp.data.credit_summation;
                $scope.txtcheque_bounce = resp.data.cheque_bounce;
                $scope.txtnumberof_boardmeetings = resp.data.numberof_boardmeetings;
                $scope.txtfarmer_count = resp.data.farmer_count;
                $scope.txtcrop_cycle = resp.data.crop_cycle;

                var parambur = {
                    institution_gid: $scope.borrowerinstitution_gid
                }
                var url = 'api/MstApplicationAdd/GetInstitutionBureauList';
                SocketService.getparams(url, parambur).then(function (resp) {
                    $scope.institutionbureau_list = resp.data.institutionbureau_list;
                });

            }); 
           
            var params = {
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            }

            var url = 'api/MstAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtinstitution_name = resp.data.company_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
                $scope.txturn_status = resp.data.urn_status;
                $scope.txturn_number = resp.data.urn;
            });

            var params = {
                credit_gid: institution_gid
            }

            var url = 'api/MstAppCreditUnderWriting/GetGeneticCodeList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.geneticcode_list = resp.data.mstcuwgeneticcode_list;
            });

            var params = {
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            }

            var url = 'api/MstAppCreditUnderWriting/EditSocialAndTradeCapital';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtsocial_capital = resp.data.social_capital;
                $scope.txttrade_capital = resp.data.trade_capital;
                unlockUI();
            });

            var url = 'api/MstAppCreditUnderWriting/EditPSLDataFlagging';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtstartloan_date = resp.data.startupasofloansanction_date;
                $scope.txtoccupation = resp.data.occupation;
                $scope.txtline_activity = resp.data.lineofactivity;
                $scope.txtbsr_code = resp.data.bsrcode;
                $scope.txtpsl_category = resp.data.pslcategory;
                $scope.txtweaker_section = resp.data.weakersection;
                $scope.txtpsl_purpose = resp.data.pslpurpose;
                $scope.txttotal_financialinstitution = resp.data.totalsanction_financialinstitution;
                $scope.txtpsl_sanctionlimit = resp.data.pslsanction_limit;
                $scope.txtnature_entity = resp.data.natureofentity;
                $scope.txtmarketing_activities = resp.data.indulgeinmarketing_activity;
                $scope.txtplant_machienery = resp.data.plantandmachineryinvestment;
                $scope.txtturnover = resp.data.turnover;
                $scope.txtmsme_classification = resp.data.msmeclassification;
                $scope.txtloansanction_date = resp.data.loansanction_date;
                $scope.txtentityincorporate_date = resp.data.entityincorporation_date;
                $scope.txthq_city = resp.data.hq_metropolitancity;
                $scope.txtclient_details = resp.data.clientdtl_name;
            });

            var params = {
                credit_gid: institution_gid
            }

            var url = 'api/MstAppCreditUnderWriting/GetCreditSupplierList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.supplierdtls_list = resp.data.supplier_list;
            });

            var url = 'api/MstAppCreditUnderWriting/GetCreditBuyerList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.Buyerdtls_list = resp.data.creditbuyer_list;
            });

            var url = 'api/MstAppCreditUnderWriting/GetCrediBankAccList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.BankacctDtl_list = resp.data.creditbankacc_list;
            });

            var params = {
                credit_gid: institution_gid
            }

            var url = 'api/MstAppCreditUnderWriting/GetExistingBankFacility';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ExistingBankacctDtl_list = resp.data.cuwexistingbankfacility_list;
            });

            var url = 'api/MstAppCreditUnderWriting/GetRepaymentTrack';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.RepaymentDtl_list = resp.data.cuwrepaymenttrack_list;
            });

            var params = {
                credit_gid: institution_gid
            }

            var url = 'api/MstAppCreditUnderWriting/GetCreditObservationList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditobservation_list = resp.data.CreditObservation_list;
            });

            var params = {
                application_gid: application_gid
            }

            var url =  'api/MstApplicationView/GetApplicationBasicView';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
            });

            // Guarantee Details
            var params = {
                credit_gid: institution_gid
            }
            var url = 'api/MstAppCreditUnderWriting/GetInstitutionGuaranteeDtl';           
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditguaranteedtl_list = resp.data.creditguaranteedtl_list;
            });

            // Guarantee Detail Popup functions
            $scope.guarantee_remarks = function (creditguaranteedtl_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/RemarksView.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'lg'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
                    var params =
                    {
                        creditguaranteedtl_gid: creditguaranteedtl_gid
                    }
                    var url = 'api/MstAppCreditUnderWriting/GetGuaranteeRemarksView';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.txtguarantee_remarks = resp.data.remarks;
    
                    });               
    
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
    
                }
            }
            $scope.guarantee_docview = function (creditguaranteedtl_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/GuaranteedocumentsView.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'lg'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
                    var params =
                    {
                        creditguaranteedtl_gid: creditguaranteedtl_gid
                    }
                    var url = 'api/MstAppCreditUnderWriting/GetGuaranteeDocDtl';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.GuaranteeDocumentView_List = resp.data.GuaranteeDocumentView_List;
    
                    });
    
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
    
                    $scope.downloadallguarantee_doc = function () {
                        for (var i = 0; i < $scope.GuaranteeDocumentView_List.length; i++) {
                            DownloaddocumentService.Downloaddocument($scope.GuaranteeDocumentView_List[i].document_path, $scope.GuaranteeDocumentView_List[i].document_name);
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
                    $scope.download_guaranteedoc = function (val1, val2) {
                        DownloaddocumentService.Downloaddocument(val1, val2);
                    }
    
                }
            }

            // Co-lending Details
            var params = {
                credit_gid: institution_gid,
                application_gid: application_gid
            }           
            var url = 'api/MstAppCreditUnderWriting/GetColendingDtlSummary';
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.creditcolendingdtl_list = resp.data.creditcolendingdtl_list;
            });  

            var url = 'api/MstAppCreditUnderWriting/GetColendingApplicabilityStatus';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.rdbcolendingapplicability = resp.data.colendingaapplicability_stats;
                if ($scope.rdbcolendingapplicability == 'Yes') {
                    $scope.creditcolendingsummary = true;
                }
                else {
                    $scope.creditcolendingsummary = false;
                }
            });

            $scope.creditcolending_views = function (creditcolendingdtl_gid, colendingprogram_gid, portfolio_gid) {
                var creditcolendingdtl_gid = creditcolendingdtl_gid;
                var colendingprogram_gid = colendingprogram_gid;
                var portfolio_gid = portfolio_gid;
                //var application_gid = $location.search().application_gid;
                //var credit_gid = $location.search().institution_gid;
    
                var modalInstance = $modal.open({
                    templateUrl: '/CreditColendingAdd.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'lg'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
                   
                        
                    var params = {
                        creditcolendingdtl_gid: creditcolendingdtl_gid,
                        application_gid: application_gid
                    }
                    var url = 'api/MstAppCreditUnderWriting/GetColendingDtlsView';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.rdbapplicability = resp.data.applicability;
                        $scope.txtcolendingremarks = resp.data.remarks;
                        $scope.creditcolendingdtl_gid = resp.data.creditcolendingdtl_gid;
                    });
    
                    var params = {
                        creditcolendingdtl_gid: creditcolendingdtl_gid,
                        credit_gid: institution_gid,
                        application_gid: application_gid
                    }
                    var url = 'api/MstAppCreditUnderWriting/ColendingDtlDocumentView';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.creditcolendingdocument_list = resp.data.creditcolendingdocument_list;
                    });
    
                       
                    $scope.downloads = function (val1, val2) {
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
                       
                    $scope.downloadall_colender = function () {
                        for (var i = 0; i < $scope.creditcolendingdocument_list.length; i++) {
                            DownloaddocumentService.Downloaddocument($scope.creditcolendingdocument_list[i].document_path, $scope.creditcolendingdocument_list[i].document_name);
                        }
                    }
    
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
    
    
                }
    
            }

            $scope.colending_docview = function (portfolio_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/ColenderdocumentsView.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'lg'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
                    var params = {
                        portfolio_gid: portfolio_gid
                    }
                    var url = 'api/MstAppCreditUnderWriting/GetColendingRemarksView';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.txtcolending_remarks = resp.data.colending_remarks;
                        $scope.txtwef_date = resp.data.wef_date;
    ;
                    });
                    
                    var params = {
                        portfolio_gid: portfolio_gid
                    }
                    var url = 'api/MstAppCreditUnderWriting/GetColendingDocDtl';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.ColendingDocumentView_List = resp.data.ColendingDocumentView_List;
    
                    });
    
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
    
                    $scope.downloadallcolending_doc = function () {
                        for (var i = 0; i < $scope.ColendingDocumentView_List.length; i++) {
                            DownloaddocumentService.Downloaddocument($scope.ColendingDocumentView_List[i].document_path, $scope.ColendingDocumentView_List[i].document_name);
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
                    $scope.download_colendingdoc = function (val1, val2) {
                        DownloaddocumentService.Downloaddocument(val1, val2);
                    }
    
    
                }
    
            }

            $scope.CreditColendingRule = function (colendingprogram_gid) {
                $location.url('app/MstCreditColendingRuleView?lscolendingprogram_gid=' + colendingprogram_gid + '&application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage + '&lscompany=' + "Company" + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype );
            } 

            // Document Checklist
            var params = {
                credit_gid: institution_gid
            }
            var geturl = "";
            if (lspage != "myapp" || lspage != "CreditApproval")
                geturl = "api/MstCAD/GetCADTrnTaggedDocList";
            else
                geturl = "api/MstCAD/GetCADTaggedDocList";
            lockUI();
            SocketService.getparams(geturl, params).then(function (resp) {
                unlockUI();
                if (resp.data.TaggedDocument != null) {
                    $scope.taggeddoc_list = resp.data.TaggedDocument;
                    angular.forEach($scope.taggeddoc_list, function (value, key) {
                        if ((lspage == "myapp" || lspage == "CreditApproval") && value.taggedby == "Credit")
                            value.taggedby = '';
                        else if ((lspage != "myapp" || lspage != "CreditApproval") && value.taggedby == "CAD")
                            value.taggedby = '';
                        else if (value.taggedby == "N")
                            value.taggedby = 'Business';
                    });
                }
                else if (resp.data.TaggedDocument == null) {
                    $scope.taggeddoc_list = resp.data.TaggedDocument;
                }
            });

            // Covenant Checklist

            var params = {
                credit_gid: '',
                application_gid: application_gid
            }

            var url = "api/MstCAD/GetCovenantDocumentTypeList";
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.CADDocument != null) {
                    $scope.covenant_documentlist = resp.data.CADDocument;
                }
            });
            var params = {
                credit_gid: institution_gid
            }
            var geturl = "";
            if (lspage != "myapp" || lspage != "CreditApproval")
                geturl = "api/MstCAD/GetCADTrnCovenantTaggedDocList";
            else
                geturl = "api/MstCAD/GetCADCovenantTaggedDocList";
            lockUI();
            SocketService.getparams(geturl, params).then(function (resp) {
                unlockUI();
                if (resp.data.TaggedDocument != null) {
                    $scope.covenant_taggeddoclist = resp.data.TaggedDocument;
                    angular.forEach($scope.covenant_documentlist, function (value, key) {
                        var getselected = $scope.covenant_taggeddoclist.find(function (a) { return a.companydocument_gid === value.document_gid })
                        if (getselected != null) {
                            value.groupcovdocumentchecklist_gid = getselected.groupcovdocumentchecklist_gid;
                            value.covenantchecked = true;
                            value.covenantperiod = getselected.covenantperiod;
                             value.buffer_days = getselected.buffer_days;
                            value.dropdownchk = false;
                            if ((lspage == "myapp" || lspage == "CreditApproval") && getselected.taggedby == "Credit")
                                value.taggedby = '';
                            else if ((lspage != "myapp" || lspage != "CreditApproval") && getselected.taggedby == "CAD")
                                value.taggedby = '';
                            else if (getselected.taggedby == "N")
                                value.taggedby = 'Business';
                            else
                                value.taggedby = getselected.taggedby;
                        }
                        else {
                            value.dropdownchk = true;
                            value.covenantperiod = '';
                            value.covenantchecked = false;
                        }
                    });
                }
                else {
                    angular.forEach($scope.covenant_documentlist, function (value, key) {
                        value.dropdownchk = true;
                        value.covenantchecked = false;
                    });
                }
            });

            // Bureau Details
            var param = {
                institution_gid: institution_gid
            }
            var url = 'api/MstApplicationAdd/GetInstitutionBureauList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionbureau_list = resp.data.institutionbureau_list;
            });

            $scope.bureau_view = function (institution2bureau_gid) {
                $location.url('app/MstCreditInstitutionBureauView?lsinstitution2bureau_gid=' + institution2bureau_gid + '&lsinstitution_gid=' + institution_gid + '&lsapplication_gid=' + application_gid + '&lspage=' + lspage + '&lspagename=' + lspagename +'&lspagetype='+ lspagetype);
            }
           
            // Bank Statement Analysis
            var params = {
                credit_gid: institution_gid,
                application_gid : application_gid
             }
             var url = 'api/MstAppCreditUnderWriting/GetBankStatementList';
             SocketService.getparams(url, params).then(function (resp) {
                 unlockUI();
                $scope.BankStatement_list = resp.data.BankStatement_list;
              });

            // FSA Details
            var params = {
                application_gid: application_gid,
                credit_gid: institution_gid                
            }

            var url = 'api/MstAppCreditUnderWriting/GetFSASummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.FSASummary_list = resp.data.MstFSASummary_list;
            });

            $scope.view_fsa = function (application_gid, credit_gid, template_name) {
                $location.url('app/MstCreditFSAView?application_gid=' + application_gid + '&institution_gid=' + credit_gid + '&template_name=' + template_name + '&lspage=' + lspage + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype); 
            }

            $scope.download_fsa = function (val1, val2) {
                var phyPath = val1;
                var relPath = phyPath.split("EMS");
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
            
            // Norms Details
                    // ---- This list previously called, so here no need to use here ----.

                    // var url = 'api/MstAppCreditUnderWriting/GetCreditObservationList';
                    //   SocketService.getparams(url,params).then(function (resp) {
                    //       unlockUI();
                    //       $scope.CreditObservation_list = resp.data.CreditObservation_list;
                    //    }); 
                // Norms Detail Popup functions
                $scope.creditobservation_view = function (creditobservation_gid) {
                    var creditobservation_gid = creditobservation_gid;
                    var modalInstance = $modal.open({
                    templateUrl: '/viewcreditobservation.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'lg'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
        
                    // var url = 'api/MstAppCreditUnderWriting/GetCrepolicy';
                    // lockUI();
                    // SocketService.get(url).then(function (resp) {
                    //     unlockUI();
                    //     $scope.creditpolicy_list = resp.data.CreditObservation_list;
                    // });
                    
                     var params = {
                         creditobservation_gid: creditobservation_gid
                    }
                     var url = 'api/MstAppCreditUnderWriting/EditGetCreditObservation';
                     lockUI();
                     SocketService.getparams(url, params).then(function (resp) {
                         unlockUI();
                        $scope.cboeditCreditObservations = resp.data.credit_policy;
                        $scope.rdbeditCompliednonComplied = resp.data.complied_status;
                        $scope.txtedit_Observations = resp.data.observation;
                    });  
                    console.log(params)
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
        
                }
            }


        }

        $scope.close = function () {
            window.close();
        }

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }

        

        $scope.bureaudoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.institutiondoc_downloads = function (val1, val2, val3) {
            if (val3 == 'N') {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDownloaddocument(val1, val2,val3);
            }
        }

        $scope.form60_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.institutiondoc_list.length; i++) {
                //DownloaddocumentService.Downloaddocument($scope.institutiondoc_list[i].document_path, $scope.institutiondoc_list[i].document_name);
                if ($scope.institutiondoc_list[i].migration_flag == 'N') {
                    //DownloaddocumentService.Downloaddocument(val1, val2);
                    DownloaddocumentService.Downloaddocument($scope.institutiondoc_list[i].document_path, $scope.institutiondoc_list[i].document_name);
                }
                else {
                    //DownloaddocumentService.OtherDownloaddocument(val1, val2);
                    DownloaddocumentService.OtherDownloaddocument($scope.institutiondoc_list[i].document_path, $scope.institutiondoc_list[i].document_name, $scope.institutiondoc_list[i].migration_flag );
                }
            }
        }

        $scope.repayment_remarks = function (creditrepaymentdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Remarksdetails.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false

            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                    creditrepaymentdtl_gid: creditrepaymentdtl_gid
                  }
                var url = 'api/MstAppCreditUnderWriting/GetCreditRepaymentDtlRemarks';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.repayment_remarks = resp.data.Repayment_remarks;

                }); 

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.Existingbank_remarks = function (existingbankfacility_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ExistingRemarksdetails.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false

            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                  existingbankfacility_gid: existingbankfacility_gid
                }
              var url = 'api/MstAppCreditUnderWriting/GetCreditExistingBankDtlRemarks';
              lockUI();
              SocketService.getparams(url, params).then(function (resp) {
                  unlockUI();
                  $scope.existing_remarks = resp.data.Existingbank_remarks;

              }); 

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.relationship_supplier = function (creditsupplier_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/SupplierRelationship.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false

            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      creditsupplier_gid: creditsupplier_gid
                  }
                var url = 'api/MstAppCreditUnderWriting/GetCreditSupplierTextData';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtrelationship_supplier = resp.data.relationship_supplier;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.relationship_buyer = function (creditbuyer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/SupplierRelationshipMonitoring.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false

            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                 var params =
                   {
                       creditbuyer_gid: creditbuyer_gid
                   }
                 var url = 'api/MstAppCreditUnderWriting/GetCreditBuyerTextData';
               lockUI();
               SocketService.getparams(url, params).then(function (resp) {
                   unlockUI();
                   $scope.txtrelationship_borrower = resp.data.relationship_borrower;
                   $scope.txtenduse_monitoring = resp.data.enduse_monitoring;
    
               });   

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.uploadeddoc_bankacctdtl = function (creditbankdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Bankacctdocuments.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                   var params =
                     {
                         creditbankdtl_gid: creditbankdtl_gid
                     }
                   var url = 'api/MstAppCreditUnderWriting/GetCreditBankDocumentUpload';
                 lockUI();
                 SocketService.getparams(url, params).then(function (resp) {
                     unlockUI();
                     $scope.chequeleaf_list = resp.data.credituploaddocument_list;
      
                 }); 

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_chequeleafdoc = function (val1, val2) {
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
                $scope.downloadallche = function () {
                    for (var i = 0; i < $scope.chequeleaf_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.chequeleaf_list[i].chequeleaf_path, $scope.chequeleaf_list[i].chequeleaf_name);
                    }
                }

            }

        }

        $scope.bureauinstitution_view = function (institution2bureau_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/InsBureauRespObsDoc.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var param = {
                    institution2bureau_gid: institution2bureau_gid
                };

                var url = 'api/MstApplicationEdit/CICInstitutionEdit';

                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bureauname_name = resp.data.bureauname_name;
                    $scope.bureau_gid = resp.data.bureauname_gid;
                    $scope.txtbureau_score = resp.data.bureau_score;
                    $scope.txtbureauscore_date = resp.data.bureauscore_date;
                    $scope.txtobservations = resp.data.observations;
                    $scope.txtbureau_response = resp.data.bureau_response;
                    $scope.institution2bureau_gid = resp.data.institution2bureau_gid;

                    unlockUI();
                });
                var url = 'api/MstApplicationEdit/CICUploadInstitutionDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.documentviewer = function (val1, val2, val3) {
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

                    if (val3 == 'N') {
                        DownloaddocumentService.DocumentViewer(val1, val2);
                    }
                    else {
                        DownloaddocumentService.OtherDocumentViewer(val1, val2, val3);
                    }

                }
               
                $scope.bureaudoc_downloads = function (val1, val2, val3) {
                    if (val3 == 'N') {
                        DownloaddocumentService.Downloaddocument(val1, val2);
                    }
                    else {
                        DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
                    }
                }

               
                $scope.downloadallcic = function () {
                    for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                        if ($scope.cicuploaddoc_list[i].migration_flag == 'N') {
                            DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
                        }
                        else {
                            DownloaddocumentService.OtherDownloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name, $scope.cicuploaddoc_list[i].migration_flag);
                        }
                    }
                }
            }

        }

        $scope.equipment_View = function (institution2equipment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EquipmentholdingView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    institution2equipment_gid: institution2equipment_gid
                }
                var url = 'api/MstApplicationAdd/GetEquipmentHoldingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquantity = resp.data.quantity;
                    $scope.lbldescription = resp.data.description;
                    $scope.lblinsurancedetails = resp.data.insurance_details;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.livestock_View = function (institution2livestock_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/LiveStockHoldingView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    institution2livestock_gid: institution2livestock_gid
                }
                var url = 'api/MstApplicationAdd/GetLivestockHoldingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblbreed = resp.data.Breed;
                    $scope.lbllivestockinsurance_details = resp.data.insurance_details;
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

        $scope.documentviewerinstitution = function (val1, val2, val3) {
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

            if (val3 == 'N') {
                DownloaddocumentService.DocumentViewer(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDocumentViewer(val1, val2,val3);
            }

        }

    }
})();
