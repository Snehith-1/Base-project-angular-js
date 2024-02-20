(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditIndividualDtlViewController', MstCreditIndividualDtlViewController);

    MstCreditIndividualDtlViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstCreditIndividualDtlViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditIndividualDtlViewController';
        var contact_gid = localStorage.getItem('contact_gid');

        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lspagetype = $location.search().lspagetype;
        var lspagetype = $scope.lspagetype;

        const lspagename = 'MstCreditIndividualDtlView';

        lockUI();
        activate();

        function activate() {
            
            var params = {
                credit_gid: contact_gid,
                applicant_type: 'Individual'
            }

            var url = 'api/MstAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtindividual_name = resp.data.individual_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
                $scope.txturn_status = resp.data.urn_status;
                $scope.txturn_number = resp.data.urn;
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
                credit_gid: contact_gid
            }

            var url = 'api/MstAppCreditUnderWriting/GetCrediBankAccList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.BankacctDtl_list = resp.data.creditbankacc_list;
            });

            var params = {
                credit_gid: contact_gid
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
                credit_gid: contact_gid
            }

            var url = 'api/MstAppCreditUnderWriting/GetCreditObservationList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditobservation_list = resp.data.CreditObservation_list;
            });

            // Guarantee Details
            var params = {
                credit_gid: contact_gid
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
                    $scope.downloadallguarantee_doc = function () {
                        for (var i = 0; i < $scope.GuaranteeDocumentView_List.length; i++) {
                            DownloaddocumentService.Downloaddocument($scope.GuaranteeDocumentView_List[i].document_path, $scope.GuaranteeDocumentView_List[i].document_name);
                        }
                    }
    
                    $scope.download_guaranteedoc = function (val1, val2) {
                        DownloaddocumentService.Downloaddocument(val1, val2);
                    }
    
                }
            }

            // Co-lending Details
            var params = {
                credit_gid: contact_gid,
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
                //var credit_gid = $location.search().contact_gid;
    
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
                        credit_gid: contact_gid,
                        application_gid: application_gid
                    }
                    var url = 'api/MstAppCreditUnderWriting/ColendingDtlDocumentView';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.creditcolendingdocument_list = resp.data.creditcolendingdocument_list;
                    });
    
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
                    $scope.downloads = function (val1, val2) {
                        DownloaddocumentService.Downloaddocument(val1, val2);
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
                    $scope.downloadallcolending_doc = function () {
                        for (var i = 0; i < $scope.ColendingDocumentView_List.length; i++) {
                            DownloaddocumentService.Downloaddocument($scope.ColendingDocumentView_List[i].document_path, $scope.ColendingDocumentView_List[i].document_name);
                        }
                    }
    
                    $scope.download_colendingdoc = function (val1, val2) {
                        DownloaddocumentService.Downloaddocument(val1, val2);
                    }
    
    
                }
    
            }

            $scope.CreditColendingRule = function (colendingprogram_gid) {
                $location.url('app/MstCreditColendingRuleView?lscolendingprogram_gid=' + colendingprogram_gid + '&application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage + '&lsindividual=' + "Individual" + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype );
            } 

            // Document Checklist
            var params = {
                credit_gid: contact_gid
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

            var url = "api/MstCAD/GetCovenantIndividualDocumentList";
            lockUI();
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
                contact_gid: contact_gid
            }
            var url = 'api/MstApplicationAdd/GetContactBureauList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactbureau_list = resp.data.contactbureau_list;
            });

            $scope.bureau_view = function (contact2bureau_gid) {
                $location.url('app/MstCreditIndividualBureauView?lscontact2bureau_gid=' + contact2bureau_gid + '&lscontact_gid=' + contact_gid + '&lsapplication_gid=' + application_gid + '&lspage=' + lspage + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype);
            }
           
            // Bank Statement Analysis
            var params = {
                credit_gid: contact_gid,
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
                credit_gid: contact_gid                
            }

            var url = 'api/MstAppCreditUnderWriting/GetFSASummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.FSASummary_list = resp.data.MstFSASummary_list;
            });

            $scope.view_fsa = function (application_gid, credit_gid, template_name) {
                $location.url('app/MstCreditFSAView?application_gid=' + application_gid + '&contact_gid=' + credit_gid + '&template_name=' + template_name + '&lspage=' + lspage + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype); 
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
            //activate function end
        }

        $scope.close = function () {
            window.close();
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
                $scope.downloadall = function () {
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

       
    }
})();
