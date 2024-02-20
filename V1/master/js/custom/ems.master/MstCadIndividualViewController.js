(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadIndividualViewController', MstCadIndividualViewController);

    MstCadIndividualViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function MstCadIndividualViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadIndividualViewController';
        var contact_gid = localStorage.getItem('contact_gid');
        var application_gid = localStorage.getItem('application_gid');
        //var application_gid = $location.search().application_gid;

        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lspagetype = $location.search().lspagetype;
        var lspagetype = $scope.lspagetype;

        const lspagename = 'MstCadIndividualView';

        lockUI();
        activate();

        function activate() {

            var params = {
                contact_gid: contact_gid
            }

            var url = 'api/MstCADApplication/GetCadGurantorIndividualView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcustomer_name = resp.data.individual_name;
                $scope.txtborrower_type = resp.data.borrower_type;
                $scope.txtpan_number = resp.data.pan_no;
                $scope.txtaadhar_number = resp.data.aadhar_no;
                var aadhar = $scope.txtaadhar_number;
                var mask = aadhar.slice(-4);
                var maskaadhar = 'XXXX-XXXX-' + mask;
                $scope.cad_aadharnumber = maskaadhar;
                $scope.txt_dob = resp.data.individual_dob;
                $scope.txtage = resp.data.age;
                $scope.txtgender = resp.data.gender_name;
                $scope.txtdesignation = resp.data.designation_name;
                $scope.txt_peppoliticallyperson = resp.data.pep_status;
                $scope.txtpep_verifiesdate = resp.data.pepverified_date;
                $scope.txtmarital_status = resp.data.maritalstatus_name;
                $scope.txtfather_name = resp.data.father_name;
                $scope.txtfatherdob_date = resp.data.father_dob;
                $scope.txtfather_age = resp.data.father_age;
                $scope.txtmother_name = resp.data.mother_name;
                $scope.txtmotherdob_date = resp.data.mother_dob;
                $scope.txtmother_age = resp.data.mother_age;
                $scope.txtspouse_name = resp.data.spouse_name;
                $scope.txtspousedob_date = resp.data.spouse_dob;
                $scope.txtspouse_age = resp.data.spouse_age;
                $scope.txtEdu_qualification = resp.data.educationalqualification_name;
                $scope.txtmain_occupation = resp.data.main_occupation;
                $scope.txtannual_income = resp.data.annual_income;
                $scope.lblannual_incomeseperator = (parseInt($scope.txtannual_income.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblannual_incomewords = defaultamountwordschange($scope.lblannual_incomeseperator);
                $scope.txtmonthly_income = resp.data.monthly_income;
                $scope.lblmonthly_incomeseperator = (parseInt($scope.txtmonthly_income.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblmonthly_incomewords = defaultamountwordschange($scope.lblmonthly_incomeseperator);
                $scope.txtincome_type = resp.data.incometype_name;
                $scope.txtindividualprimary_number = resp.data.primaryindividual_mobileno;
                $scope.individualmobile_list = resp.data.contactmobileno_list;
                $scope.txtindividualprimary_emailaddress = resp.data.primaryindividual_email;
                $scope.individualmailaddress_list = resp.data.contactemail_list;
                $scope.txtownership_type = resp.data.ownershiptype_name;
                $scope.txtproperty_name = resp.data.propertyholder_name;
                $scope.txtresidence_type = resp.data.residencetype_name;
                $scope.txtyear_currentresidence = resp.data.currentresidence_years;
                $scope.txtdistance = resp.data.branch_distance;
                $scope.individualproof_list = resp.data.contactidproof_list;
                $scope.individualdoc_list = resp.data.uploadindividualdoc_list;
                $scope.bureauname_gid = resp.data.bureauname_gid;
                $scope.txtindividualbureau_name = resp.data.indbureauname_name;
                $scope.txtindividualbureau_score = resp.data.indbureau_score;
                $scope.txtindividualscore_on = resp.data.indbureauscore_date;
                $scope.txtindividualobservations = resp.data.indobservations;
                $scope.txtindividualbureau_response = resp.data.indbureau_response;
                $scope.cicindividualdocument_name = resp.data.indcicdocument_name;
                $scope.cicindividualdocument_path = resp.data.indcicinddocument_path;
                $scope.individualaddress_list = resp.data.contactaddress_list;
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtprofile = resp.data.profile;
                $scope.txturn_status = resp.data.urn_status;
                $scope.txt_urn = resp.data.urn;
                $scope.txtother_nominee = resp.data.othernominee_status;
                $scope.txtrelationship_type = resp.data.relationshiptype;
                $scope.txtnomineedob_date = resp.data.nominee_dob;
                $scope.nomineefirst_name = resp.data.nomineefirst_name;
                $scope.nominee_middlename = resp.data.nominee_middlename;
                $scope.nominee_lastname = resp.data.nominee_lastname;
                $scope.txtnominee_age = resp.data.nominee_age;
                $scope.txtfathernominee_status = resp.data.fathernominee_status;
                $scope.txtmothernominee_status = resp.data.mothernominee_status;
                $scope.txtspousenominee_status = resp.data.spousenominee_status;
                $scope.txttotal_landacres = resp.data.totallandinacres;
                $scope.txtcultivated_land = resp.data.cultivatedland;
                $scope.txtprevious_crop = resp.data.previouscrop;
                $scope.txtproposed_crop = resp.data.prposedcrop;
                $scope.txtinstitution_name = resp.data.institution_name;
                $scope.contactpanabsencereasons_list = resp.data.contactpanabsencereasons_list;
                $scope.txtpan_status = resp.data.pan_status;
                $scope.txtindnearsamunnati_name = resp.data.nearsamunnatiabranch_name;
                $scope.txtindphysicalstatus_gid = resp.data.physicalstatus_gid;
                $scope.txtindphysical_status = resp.data.physicalstatus_name;
                $scope.txtindinternalrating_gid = resp.data.internalrating_gid;
                $scope.txtindinternal_rating = resp.data.internalrating_name;
                $scope.mstindlivestockholding_list = resp.data.mstlivestockholding_list;
                $scope.mstindequipmentholding_list = resp.data.mstequipmentholding_list;

                $scope.borrowercontact_gid = resp.data.contact_gid;

                var parambur = {
                    contact_gid: $scope.borrowercontact_gid
                }
                var url = 'api/MstCADApplication/GetCadContactBureauList';
                SocketService.getparams(url, parambur).then(function (resp) {
                    $scope.contactbureau_list = resp.data.contactbureau_list;
                });

            });

            var params = {
                credit_gid: contact_gid,
                applicant_type: 'Individual'
            }

            var url = 'api/MstCADApplication/GetCadCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtindividual_name = resp.data.individual_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
                $scope.txturn_status = resp.data.urn_status;
                $scope.txturn_number = resp.data.urn;
            });

            var url = 'api/MstCADApplication/GetCadPSLDataFlagging';
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
                credit_gid: contact_gid
            }

            var url = 'api/MstCADApplication/GetCadCrediBankAcctList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.BankacctDtl_list = resp.data.creditbankacc_list;
            });

            var params = {
                credit_gid: contact_gid
            }

            var url = 'api/MstCADApplication/GetCadExistingBankFacility';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ExistingBankacctDtl_list = resp.data.cuwexistingbankfacility_list;
            });

            var url = 'api/MstCADApplication/GetCadRepaymentTrack';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.RepaymentDtl_list = resp.data.cuwrepaymenttrack_list;
            });

            var params = {
                credit_gid: contact_gid
            }

            var url = 'api/MstCADApplication/GetCadCreditObservationList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditobservation_list = resp.data.CreditObservation_list;
            });

            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstCADApplication/GetApplicationBasicView';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
            });

            // Guarantee Details
            var params = {
                credit_gid: contact_gid
            }
            var url = 'api/MstCADApplication/GetInstitutionGuaranteeDtl';           
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditguaranteedtl_list = resp.data.creditguaranteedtl_list;
            });

            // Guarantee Detail Popup functions
            $scope.guarantee_remarks = function (cadcreditguaranteedtl_gid) {
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
                        cadcreditguaranteedtl_gid: cadcreditguaranteedtl_gid
                    }
                    var url = 'api/MstCADApplication/GetGuaranteeRemarksView';
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
            $scope.guarantee_docview = function (cadcreditguaranteedtl_gid) {
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
                        cadcreditguaranteedtl_gid: cadcreditguaranteedtl_gid
                    }
                    var url = 'api/MstCADApplication/GetGuaranteeDocDtl';
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

    //         // Co-lending Details
    //         var params = {
    //             credit_gid: contact_gid,
    //             application_gid: application_gid
    //         }           
    //         var url = 'api/MstAppCreditUnderWriting/GetColendingDtlSummary';
    //         SocketService.getparams(url,params).then(function (resp) {
    //             unlockUI();
    //             $scope.creditcolendingdtl_list = resp.data.creditcolendingdtl_list;
    //         });  

    //         $scope.creditcolending_views = function (creditcolendingdtl_gid, colendingprogram_gid, portfolio_gid) {
    //             var creditcolendingdtl_gid = creditcolendingdtl_gid;
    //             var colendingprogram_gid = colendingprogram_gid;
    //             var portfolio_gid = portfolio_gid;
    //             //var application_gid = $location.search().application_gid;
    //             //var credit_gid = $location.search().contact_gid;
    
    //             var modalInstance = $modal.open({
    //                 templateUrl: '/CreditColendingAdd.html',
    //                 controller: ModalInstanceCtrl,
    //                 backdrop: 'static',
    //                 keyboard: false,
    //                 size: 'lg'
    //             });
    //             ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
    //             function ModalInstanceCtrl($scope, $modalInstance) {
                   
                        
    //                 var params = {
    //                     creditcolendingdtl_gid: creditcolendingdtl_gid,
    //                     application_gid: application_gid
    //                 }
    //                 var url = 'api/MstAppCreditUnderWriting/GetColendingDtlsView';
    //                 lockUI();
    //                 SocketService.getparams(url, params).then(function (resp) {
    //                     unlockUI();
    //                     $scope.rdbapplicability = resp.data.applicability;
    //                     $scope.txtcolendingremarks = resp.data.remarks;
    //                     $scope.creditcolendingdtl_gid = resp.data.creditcolendingdtl_gid;
    //                 });
    
    //                 var params = {
    //                     creditcolendingdtl_gid: creditcolendingdtl_gid,
    //                     credit_gid: contact_gid,
    //                     application_gid: application_gid
    //                 }
    //                 var url = 'api/MstAppCreditUnderWriting/ColendingDtlDocumentView';
    //                 lockUI();
    //                 SocketService.getparams(url, params).then(function (resp) {
    //                     unlockUI();
    //                     $scope.creditcolendingdocument_list = resp.data.creditcolendingdocument_list;
    //                 });
    
                       
    //                 $scope.downloads = function (val1, val2) {
    //                     DownloaddocumentService.Downloaddocument(val1, val2);
    //                 }
    
                       
    //                 $scope.downloadall_colender = function () {
    //                     for (var i = 0; i < $scope.creditcolendingdocument_list.length; i++) {
    //                         DownloaddocumentService.Downloaddocument($scope.creditcolendingdocument_list[i].document_path, $scope.creditcolendingdocument_list[i].document_name);
    //                     }
    //                 }
    
    //                 $scope.ok = function () {
    //                     $modalInstance.close('closed');
    //                 };
    
    //             }
    
    //         }

    //         $scope.colending_docview = function (portfolio_gid) {
    //             var modalInstance = $modal.open({
    //                 templateUrl: '/ColenderdocumentsView.html',
    //                 controller: ModalInstanceCtrl,
    //                 backdrop: 'static',
    //                 keyboard: false,
    //                 size: 'lg'
    //             });
    //             ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
    //             function ModalInstanceCtrl($scope, $modalInstance) {
    //                 var params = {
    //                     portfolio_gid: portfolio_gid
    //                 }
    //                 var url = 'api/MstAppCreditUnderWriting/GetColendingRemarksView';
    //                 lockUI();
    //                 SocketService.getparams(url, params).then(function (resp) {
    //                     unlockUI();
    //                     $scope.txtcolending_remarks = resp.data.colending_remarks;
    //                     $scope.txtwef_date = resp.data.wef_date;
    // ;
    //                 });
                    
    //                 var params = {
    //                     portfolio_gid: portfolio_gid
    //                 }
    //                 var url = 'api/MstAppCreditUnderWriting/GetColendingDocDtl';
    //                 lockUI();
    //                 SocketService.getparams(url, params).then(function (resp) {
    //                     unlockUI();
    //                     $scope.ColendingDocumentView_List = resp.data.ColendingDocumentView_List;
    
    //                 });
    
    //                 $scope.ok = function () {
    //                     $modalInstance.close('closed');
    //                 };
    
    //                 $scope.downloadallcolending_doc = function () {
    //                     for (var i = 0; i < $scope.ColendingDocumentView_List.length; i++) {
    //                         DownloaddocumentService.Downloaddocument($scope.ColendingDocumentView_List[i].document_path, $scope.ColendingDocumentView_List[i].document_name);
    //                     }
    //                 }
    
    //                 $scope.download_colendingdoc = function (val1, val2) {
    //                     DownloaddocumentService.Downloaddocument(val1, val2);
    //                 }
        
    //             }
    
    //         }

    //         $scope.CreditColendingRule = function (colendingprogram_gid) {
    //             $location.url('app/MstCreditColendingRuleView?lscolendingprogram_gid=' + colendingprogram_gid + '&application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage + '&lsindividual=' + "Individual" + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype );
    //         } 

            // Document Checklist
            var params = {
                credit_gid: contact_gid
            }
            // var url = "api/MstCADCreditAction/GetIndividualTypeList";
            // SocketService.getparams(url, params).then(function (resp) {
            //     unlockUI();
            //     if (resp.data.CADDocument != null) {
            //         $scope.documentlist_gid = resp.data.CADDocument;
            //     }
            // });
             
            var geturl = "";
            if (lspage != "StartCreditUnderwriting")
                geturl = "api/MstCADCreditAction/GetCADTrnTaggedDocList";
            else
                geturl = "api/MstCADCreditAction/GetCADTaggedDocList"; 
            SocketService.getparams(geturl, params).then(function (resp) {
                unlockUI();
                if (resp.data.TaggedDocument != null) {
                    $scope.taggeddoc_list = resp.data.TaggedDocument;
                    angular.forEach($scope.taggeddoc_list, function (value, key) {
                        if (lspage == "StartCreditUnderwriting" && value.taggedby == "Credit")
                            value.taggedby = '';
                        else if (lspage != "StartCreditUnderwriting" && value.taggedby == "CAD")
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
                application_gid: application_gid,
                credit_gid: "" 
            }
            var url = "api/MstCADCreditAction/GetCovenantIndividualDocumentList";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.CADDocument != null) {
                    $scope.covenant_documentlist = resp.data.CADDocument;
                }
            }); 

            var params = {
                credit_gid: contact_gid
            }
            var geturl = "";
            if (lspage != "StartCreditUnderwriting")
                geturl = "api/MstCADCreditAction/GetCADTrnCovenantTaggedDocList";
            else
                geturl = "api/MstCADCreditAction/GetCADCovenantTaggedDocList"; 
            SocketService.getparams(geturl, params).then(function (resp) {
                unlockUI();
                if (resp.data.TaggedDocument != null) {
                    $scope.covenant_taggeddoclist = resp.data.TaggedDocument;
                    angular.forEach($scope.covenant_documentlist, function (value, key) {
                        var getselected = $scope.covenant_taggeddoclist.find(function (a) { return a.companydocument_gid === value.document_gid });
                        if (getselected != null) {
                            value.covenantchecked = true;
                            value.covenantperiod = getselected.covenantperiod;
                            value.buffer_days = getselected.buffer_days;
                            value.dropdownchk = false; 
                            value.groupcovdocumentchecklist_gid = getselected.groupcovdocumentchecklist_gid;
                            if (lspage == "StartCreditUnderwriting" && getselected.taggedby == "Credit")
                                value.taggedby = '';
                            else if (lspage != "StartCreditUnderwriting" && getselected.taggedby == "CAD")
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
                contact_gid: contact_gid
            }
            var url = 'api/MstCADCreditAction/GetContactBureauList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactbureau_list = resp.data.contactbureau_list;
            });

            $scope.bureau_view = function (contact2bureau_gid) {
                $location.url('app/MstCADCreditIndividualBureauView?lscontact2bureau_gid=' + contact2bureau_gid + '&lscontact_gid=' + contact_gid + '&lsapplication_gid=' + application_gid + '&lspage=' + lspage + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype);
            }
           
            // Bank Statement Analysis
            var params = {
                credit_gid: contact_gid,
                application_gid : application_gid
             }
             var url = 'api/MstCADCreditAction/GetBankStatementList';
             SocketService.getparams(url, params).then(function (resp) {
                 unlockUI();
                $scope.BankStatement_list = resp.data.BankStatement_list;
              });

            
            // Norms Details

                var url = 'api/MstCADCreditAction/GetCreditObservationList';
                SocketService.getparams(url,params).then(function (resp) {
                    unlockUI();
                    $scope.CreditObservation_list = resp.data.CreditObservation_list;
                }); 
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
        
                     var params = {
                         creditobservation_gid: creditobservation_gid
                    }
                     var url = 'api/MstCADCreditAction/EditGetCreditObservation';
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

        

        $scope.individualproof_downloads = function (val1, val2) {
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

        $scope.individualdoc_downloads = function (val1, val2, val3) {
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
            if (val3 == 'N') {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDownloaddocument(val1, val2,val3);
            }

        }

        $scope.individualbureaudoc_downloads = function (val1, val2) {
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

        $scope.downloadallind = function () {
            for (var i = 0; i < $scope.individualproof_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.individualproof_list[i].document_path, $scope.individualproof_list[i].document_name);
            }
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.individualdoc_list.length; i++) {
               // DownloaddocumentService.Downloaddocument($scope.individualdoc_list[i].document_path, $scope.individualdoc_list[i].document_name);


                if ($scope.individualdoc_list[i].migration_flag == 'N') {
                    //DownloaddocumentService.Downloaddocument(val1, val2);
                    DownloaddocumentService.Downloaddocument($scope.individualdoc_list[i].document_path, $scope.individualdoc_list[i].document_name);
                }
                else {
                    //DownloaddocumentService.OtherDownloaddocument(val1, val2);
                    DownloaddocumentService.OtherDownloaddocument($scope.individualdoc_list[i].document_path, $scope.individualdoc_list[i].document_name, $scope.individualdoc_list[i].migration_flag );
                }
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
                var url = 'api/MstCADApplication/GetCadCreditBankDocumentUpload';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.chequeleaf_list = resp.data.credituploaddocument_list;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_chequeleafdoc = function (val1, val2) {
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
                var url = 'api/MstCADApplication/GetCadCreditRepaymentDtlRemarks';
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
                var url = 'aapi/MstCADApplication/GetCadCreditExistingBankDtlRemarks';
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

        $scope.bureaucontact_view = function (contact2bureau_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/IndBureauRespObsDoc.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var param = {
                    contact2bureau_gid: contact2bureau_gid
                };

                var url = 'api/MstCADApplication/GetCadCICIndividualDtl';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bureauname_name = resp.data.bureauname_name;
                    $scope.bureau_gid = resp.data.bureauname_gid;
                    $scope.txtbureau_score = resp.data.bureau_score;
                    $scope.txtbureauscore_date = resp.data.bureauscore_date;
                    $scope.txtobservations = resp.data.observations;
                    $scope.txtbureau_response = resp.data.bureau_response;
                    $scope.contact2bureau_gid = resp.data.contact2bureau_gid;

                    unlockUI();
                });
                var url = 'api/MstCADApplication/GetCadCICUploadIndividualDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.bureaudoc_downloads = function (val1, val2, val3) {
                    if (val3 == 'N') {
                        DownloaddocumentService.Downloaddocument(val1, val2);
                    }
                    else {
                        DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
                    }
                }

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

        $scope.equipment_View = function (contact2equipment_gid) {
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
                    contact2equipment_gid: contact2equipment_gid
                }
                var url = 'api/MstApplicationAdd/GetContactEquipmentHoldingView';
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

        $scope.livestock_View = function (contact2livestock_gid) {
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
                    contact2livestock_gid: contact2livestock_gid
                }
                var url = 'api/MstApplicationAdd/GetContactLivestockHoldingView';
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

        $scope.documentviewerindividual = function (val1, val2, val3) {
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
