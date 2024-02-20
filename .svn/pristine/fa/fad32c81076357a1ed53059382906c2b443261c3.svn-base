(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADCreditCompanyDtlViewController', MstCADCreditCompanyDtlViewController);

    MstCADCreditCompanyDtlViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstCADCreditCompanyDtlViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADCreditCompanyDtlViewController';
        var institution_gid = localStorage.getItem('institution_gid');
        var application_gid = $location.search().application_gid;

        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lspagetype = $location.search().lspagetype;
        var lspagetype = $scope.lspagetype;

        const lspagename = 'MstCADCreditCompanyDtlView';

        lockUI();
        activate();

        function activate() {
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
            var params = {
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            }

            var url = 'api/MstCADApplication/GetCadCreditOperationsView';
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

            var url = 'api/MstCADApplication/GetCadGeneticCodeList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.geneticcode_list = resp.data.mstcuwgeneticcode_list;
            });

            var params = {
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            }

            var url = 'api/MstCADCreditAction/EditSocialAndTradeCapital';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtsocial_capital = resp.data.social_capital;
                $scope.txttrade_capital = resp.data.trade_capital;
              
            });

            var url = 'api/MstCADCreditAction/EditPSLDataFlagging';
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

            var url = 'api/MstCADApplication/GetCadCreditSupplierList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.supplierdtls_list = resp.data.supplier_list;
            });

            var url = 'api/MstCADApplication/GetCadCreditBuyerList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.Buyerdtls_list = resp.data.creditbuyer_list;
            });

            var url = 'api/MstCADApplication/GetCadCrediBankAcctList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.BankacctDtl_list = resp.data.creditbankacc_list;
            });

            var params = {
                credit_gid: institution_gid
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
                credit_gid: institution_gid
            }

            var url = 'api/MstCADApplication/GetCadCreditObservationList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditobservation_list = resp.data.CreditObservation_list;
            });

            // Guarantee Details
            var params = {
                credit_gid: institution_gid
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

            // Co-lending Details
            // var params = {
            //     credit_gid: institution_gid,
            //     application_gid: application_gid
            // }           
            // var url = 'api/MstCADApplication/GetColendingDtlSummary';
            // SocketService.getparams(url,params).then(function (resp) {
            //     unlockUI();
            //     $scope.creditcolendingdtl_list = resp.data.creditcolendingdtl_list;
            // });  

            // $scope.creditcolending_views = function (creditcolendingdtl_gid, colendingprogram_gid, portfolio_gid) {
            //     var creditcolendingdtl_gid = creditcolendingdtl_gid;
            //     var colendingprogram_gid = colendingprogram_gid;
            //     var portfolio_gid = portfolio_gid;
            //     //var application_gid = $location.search().application_gid;
            //     //var credit_gid = $location.search().institution_gid;
    
            //     var modalInstance = $modal.open({
            //         templateUrl: '/CreditColendingAdd.html',
            //         controller: ModalInstanceCtrl,
            //         backdrop: 'static',
            //         keyboard: false,
            //         size: 'lg'
            //     });
            //     ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            //     function ModalInstanceCtrl($scope, $modalInstance) {
                   
                        
            //         var params = {
            //             creditcolendingdtl_gid: creditcolendingdtl_gid,
            //             application_gid: application_gid
            //         }
            //         var url = 'api/MstCADApplication/GetColendingDtlsView';
            //         lockUI();
            //         SocketService.getparams(url, params).then(function (resp) {
            //             unlockUI();
            //             $scope.rdbapplicability = resp.data.applicability;
            //             $scope.txtcolendingremarks = resp.data.remarks;
            //             $scope.creditcolendingdtl_gid = resp.data.creditcolendingdtl_gid;
            //         });
    
            //         var params = {
            //             creditcolendingdtl_gid: creditcolendingdtl_gid,
            //             credit_gid: institution_gid,
            //             application_gid: application_gid
            //         }
            //         var url = 'api/MstCADApplication/ColendingDtlDocumentView';
            //         lockUI();
            //         SocketService.getparams(url, params).then(function (resp) {
            //             unlockUI();
            //             $scope.creditcolendingdocument_list = resp.data.creditcolendingdocument_list;
            //         });
    
                       
            //         $scope.downloads = function (val1, val2) {
            //             DownloaddocumentService.Downloaddocument(val1, val2);
            //         }
    
                       
            //         $scope.downloadall_colender = function () {
            //             for (var i = 0; i < $scope.creditcolendingdocument_list.length; i++) {
            //                 DownloaddocumentService.Downloaddocument($scope.creditcolendingdocument_list[i].document_path, $scope.creditcolendingdocument_list[i].document_name);
            //             }
            //         }
    
            //         $scope.ok = function () {
            //             $modalInstance.close('closed');
            //         };
    
    
            //     }
    
            // }

            // $scope.colending_docview = function (portfolio_gid) {
            //     var modalInstance = $modal.open({
            //         templateUrl: '/ColenderdocumentsView.html',
            //         controller: ModalInstanceCtrl,
            //         backdrop: 'static',
            //         keyboard: false,
            //         size: 'lg'
            //     });
            //     ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            //     function ModalInstanceCtrl($scope, $modalInstance) {
            //         var params = {
            //             portfolio_gid: portfolio_gid
            //         }
            //         var url = 'api/MstCADApplication/GetColendingRemarksView';
            //         lockUI();
            //         SocketService.getparams(url, params).then(function (resp) {
            //             unlockUI();
            //             $scope.txtcolending_remarks = resp.data.colending_remarks;
            //             $scope.txtwef_date = resp.data.wef_date;
    
            //         });
                    
            //         var params = {
            //             portfolio_gid: portfolio_gid
            //         }
            //         var url = 'api/MstCADApplication/GetColendingDocDtl';
            //         lockUI();
            //         SocketService.getparams(url, params).then(function (resp) {
            //             unlockUI();
            //             $scope.ColendingDocumentView_List = resp.data.ColendingDocumentView_List;
    
            //         });
    
            //         $scope.ok = function () {
            //             $modalInstance.close('closed');
            //         };
    
            //         $scope.downloadallcolending_doc = function () {
            //             for (var i = 0; i < $scope.ColendingDocumentView_List.length; i++) {
            //                 DownloaddocumentService.Downloaddocument($scope.ColendingDocumentView_List[i].document_path, $scope.ColendingDocumentView_List[i].document_name);
            //             }
            //         }
    
            //         $scope.download_colendingdoc = function (val1, val2) {
            //             DownloaddocumentService.Downloaddocument(val1, val2);
            //         }
    
    
            //     }
    
            // }

            // $scope.CreditColendingRule = function (colendingprogram_gid) {
            //     $location.url('app/MstCreditColendingRuleView?lscolendingprogram_gid=' + colendingprogram_gid + '&application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage + '&lscompany=' + "Company" + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype );
            // } 

            // Document Checklist
            var params = {
                credit_gid: institution_gid
            }
            var geturl = "";
            if (lspage != "StartCreditUnderwriting")
                geturl = "api/MstCADCreditAction/GetCADTrnTaggedDocList";
            else
                geturl = "api/MstCADCreditAction/GetCADTaggedDocList";
                lockUI();
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
                credit_gid: '',
                application_gid: application_gid
            }

            var url = "api/MstCADCreditAction/GetCovenantDocumentTypeList";
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
            if (lspage != "StartCreditUnderwriting")
                geturl = "api/MstCADCreditAction/GetCADTrnCovenantTaggedDocList";
            else
                geturl = "api/MstCADCreditAction/GetCADCovenantTaggedDocList";
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
                institution_gid: institution_gid
            }
            var url = 'api/MstCADCreditAction/GetInstitutionBureauList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionbureau_list = resp.data.institutionbureau_list;
            });

            $scope.bureau_view = function (institution2bureau_gid) {
                $location.url('app/MstCADCreditInstitutionBureauView?lsinstitution2bureau_gid=' + institution2bureau_gid + '&lsinstitution_gid=' + institution_gid + '&lsapplication_gid=' + application_gid + '&lspage=' + lspage + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype );
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

            var url = 'api/MstCADCreditAction/GetFSASummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.FSASummary_list = resp.data.MstFSASummary_list;
            });

            $scope.view_fsa = function (application_gid, credit_gid, template_name) {
                $location.url('app/MstCADCreditFSAView?application_gid=' + application_gid + '&institution_gid=' + credit_gid + '&template_name=' + template_name + '&lspage=' + lspage + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype); 
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
            
            // Norms Details

                var params = {
                    credit_gid: institution_gid
                }
            
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
        
                    // var url = 'api/MstCADApplication/GetCrepolicy';
                    // lockUI();
                    // SocketService.get(url).then(function (resp) {
                    //     unlockUI();
                    //     $scope.creditpolicy_list = resp.data.CreditObservation_list;
                    // });
                    
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
              var url = 'api/MstCADApplication/GetCadCreditExistingBankDtlRemarks';
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
                var url = 'api/MstCADApplication/GetCadCreditSupplierTextData';
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
                 var url = 'api/MstCADApplication/GetCadCreditBuyerTextData';
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

                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.chequeleaf_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.chequeleaf_list[i].chequeleaf_path, $scope.chequeleaf_list[i].chequeleaf_name);
                    }
                }

            }

        }

    }
})();
