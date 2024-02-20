(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCreditCompanyDtlViewController', AgrTrnCreditCompanyDtlViewController);

    AgrTrnCreditCompanyDtlViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

    function AgrTrnCreditCompanyDtlViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCreditCompanyDtlViewController';
        var institution_gid = localStorage.getItem('institution_gid');

        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lspagetype = $location.search().lspagetype;
        var lspagetype = $scope.lspagetype;

        const lspagename = 'AgrTrnCreditCompanyDtlView';

        lockUI();
        activate();

        function activate() {
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
            var params = {
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditOperationsView';
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

            var url = 'api/AgrTrnAppCreditUnderWriting/GetGeneticCodeList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.geneticcode_list = resp.data.mstcuwgeneticcode_list;
            });

            var params = {
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/EditSocialAndTradeCapital';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtsocial_capital = resp.data.social_capital;
                $scope.txttrade_capital = resp.data.trade_capital;
                unlockUI();
            });

            var url = 'api/AgrTrnAppCreditUnderWriting/EditPSLDataFlagging';
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

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditSupplierList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.supplierdtls_list = resp.data.supplier_list;
            });

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditBuyerList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.Buyerdtls_list = resp.data.creditbuyer_list;
            });

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCrediBankAccList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.BankacctDtl_list = resp.data.creditbankacc_list;
            });

            var params = {
                credit_gid: institution_gid
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/GetExistingBankFacility';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ExistingBankacctDtl_list = resp.data.cuwexistingbankfacility_list;
            });

            var url = 'api/AgrTrnAppCreditUnderWriting/GetRepaymentTrack';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.RepaymentDtl_list = resp.data.cuwrepaymenttrack_list;
            });

            var params = {
                credit_gid: institution_gid
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditObservationList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditobservation_list = resp.data.CreditObservation_list;
            });

            // Document Checklist
            var params = {
                credit_gid: institution_gid
            }
            var geturl = "";
            if (lspage != "myapp" || lspage != "CreditApproval")
                geturl = "api/AgrTrnCAD/GetCADTrnTaggedDocList";
            else
                geturl = "api/AgrTrnCAD/GetCADTaggedDocList";
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

            var url = "api/AgrTrnCAD/GetCovenantDocumentTypeList";
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
            if (lspage != "myapp" || lspage != "CreditApproval"|| lspage != "submittoapp")
                geturl = "api/AgrTrnCAD/GetCADTrnCovenantTaggedDocList";
            else
                geturl = "api/AgrTrnCAD/GetCADCovenantTaggedDocList";
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
                            /*  value.covenantperiod = getselected.covenantperiod == 'Every month' ? 1 : getselected.covenantperiod == 'Every 3 months' ? 2 : getselected.covenantperiod == 'Every 6 months' ? 3 : getselected.covenantperiod == 'Every 1 year' ? 4 : ""*/
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
            var url = 'api/AgrMstApplicationAdd/GetInstitutionBureauList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionbureau_list = resp.data.institutionbureau_list;
            });

            $scope.bureau_view = function (institution2bureau_gid) {
                $location.url('app/AgrTrnCreditInstitutionBureauView?lsinstitution2bureau_gid=' + institution2bureau_gid + '&lsinstitution_gid=' + institution_gid + '&lsapplication_gid=' + application_gid + '&lspage=' + lspage + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype );
            }
           
            // Bank Statement Analysis
            var params = {
                credit_gid: institution_gid,
                application_gid : application_gid
             }
             var url = 'api/AgrTrnAppCreditUnderWriting/GetBankStatementList';
             SocketService.getparams(url, params).then(function (resp) {
                 unlockUI();
                $scope.BankStatement_list = resp.data.BankStatement_list;
              });

            // FSA Details
            var params = {
                application_gid: application_gid,
                credit_gid: institution_gid                
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/GetFSASummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.FSASummary_list = resp.data.MstFSASummary_list;
            });

            $scope.view_fsa = function (application_gid, credit_gid, template_name) {
                $location.url('app/AgrTrnCreditFSAView?application_gid=' + application_gid + '&institution_gid=' + credit_gid + '&template_name=' + template_name + '&lspage=' + lspage + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype); 
            }

            $scope.download_fsa = function (val1, val2) {
                // var phyPath = val1;
                // var relPath = phyPath.split("EMS");
                // var relpath1 = relPath[1].replace("\\", "/");
                // var hosts = window.location.host;
                // var prefix = location.protocol + "//";
                // var str = prefix.concat(hosts, relpath1);
                // var link = document.createElement("a");
                // var name = val2.split('.');
                // link.download = name[0];
                // var uri = str;
                // link.href = uri;
                // link.click();
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
            
            // Norms Details
                    // ---- This list previously called, so here no need to use here ----.

                    // var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditObservationList';
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
        
                    // var url = 'api/AgrTrnAppCreditUnderWriting/GetCrepolicy';
                    // lockUI();
                    // SocketService.get(url).then(function (resp) {
                    //     unlockUI();
                    //     $scope.creditpolicy_list = resp.data.CreditObservation_list;
                    // });
                    
                     var params = {
                         creditobservation_gid: creditobservation_gid
                    }
                     var url = 'api/AgrTrnAppCreditUnderWriting/EditGetCreditObservation';
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
                var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditRepaymentDtlRemarks';
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
              var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditExistingBankDtlRemarks';
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
                var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditSupplierTextData';
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
                 var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditBuyerTextData';
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
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance', 'DownloaddocumentService', 'cmnfunctionService'];
            function ModalInstanceCtrl($scope, $modalInstance, DownloaddocumentService, cmnfunctionService) {
                   var params =
                     {
                         creditbankdtl_gid: creditbankdtl_gid
                     }
                   var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditBankDocumentUpload';
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
                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.chequeleaf_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.chequeleaf_list[i].chequeleaf_path, $scope.chequeleaf_list[i].chequeleaf_name);
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

        }

        
    }
})();
