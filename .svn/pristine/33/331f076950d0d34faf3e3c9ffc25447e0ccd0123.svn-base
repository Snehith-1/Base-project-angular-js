(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstStartScheduledMeetingController', MstStartScheduledMeetingController);

    MstStartScheduledMeetingController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService','cmnfunctionService'];

    function MstStartScheduledMeetingController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, $sce, $anchorScroll, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstStartScheduledMeetingController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;

        const lspagetype = 'CC';
        const lspagename = 'MstStartScheduledMeeting';


        activate();

        function activate() {

            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
            $scope.submit_to_approval = false;
         
            fnapplicationviewinfo();
       
            var param = {
                application_gid: application_gid
            }
            var url = 'api/MstApplicationEdit/GetAppProductList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstproduct_list = resp.data.mstproduct_list;
            });

            var params = {
                application_gid: application_gid
            }
             lockUI();
            var url = "api/MstCAMGeneration/GetApp2CAM"
            SocketService.getparams(url, params).then(function (resp) {
                 unlockUI();

                $scope.cam_content = resp.data.template_content;
                $scope.lspath = resp.data.lspath;
                $scope.lsname = resp.data.lsname;
            });

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM'
            }


            var url = 'api/MstApplicationView/GetRMDetailsView';
             lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtrmdepartment_name = resp.data.department_name;
                $scope.txtrm_name = resp.data.RM_Name;
                $scope.txtappl_initiateddate = resp.data.applicationinitiated_date;
                $scope.txtunderwritten_date = resp.data.ccsubmitted_date;
                $scope.txtunderwritten_by = resp.data.ccsubmitted_by;
            });


            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCC/GetCCRequestorlist';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.requestorlist = resp.data.ccrequestordtl;
            });


        }

        $scope.Back = function () {
            if (lspage == 'CCMmeetingScheduledcompleted') {
                $location.url('app/MstCcCompletedScheduledMeeting');
            }
            else {
                $state.go('app.MstCcScheduledMeetingSummary');
            }
        }

        $scope.sendrequestorclick = function () {
            var params = {
                application_gid: application_gid,
                remarks: $scope.txtqueries
            }
            lockUI();
            var url = "api/MstCC/PostSendCCRequestor";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = "api/MstCC/GetCCRequestorlist"
                    var param = {
                        application_gid: application_gid
                    };
                    SocketService.getparams(url, param).then(function (resp) {
                        if ((resp.data.status == true)) {
                        $scope.requestorlist = resp.data.ccrequestordtl;
                        unlockUI();
                    }
                    else if (resp.data.status == false)
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
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                       
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $scope.txtqueries = "";
            });
        }
        $scope.uploaddocument = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");
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
            frm.append('document_title', $scope.txtdocument_title);
            frm.append('application_gid', $scope.application_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            var url = 'api/MstCC/ConversationCCDocUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                $("#addupload").val('');
                $scope.txtdocument_title = '';
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                    var url = "api/MstCC/GetCCRequestorlist"
                    var param = {
                        application_gid: application_gid
                    };
                    lockUI();
                    SocketService.getparams(url, param).then(function (resp) {
                        if ((resp.data.status == true)) {
                            $scope.requestorlist = resp.data.ccrequestorlist;
                            unlockUI();
                        }
                        else if (resp.data.status == false)
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
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        
                    });
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')

                }

            });

        }

        $scope.downloadsdocument = function (val1, val2) {
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

        $scope.downloaddocument = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
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
                var url = 'api/MstApplicationView/GetHypoDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.Hypothecationdoc_list = resp.data.HypoDocumentList;

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
                $scope.download_Hypothecationdoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.downloadallhyp = function () {
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
                var url = 'api/MstApplicationView/GetCollateralDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.Collateraldoc_list = resp.data.CollatralDocumentList;

                });
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
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };              

                $scope.download_Collateraldoc = function (val1, val2, val3) {
                    if (val3 == 'N') {
                        DownloaddocumentService.Downloaddocument(val1, val2);
                    }
                    else {
                        DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
                    }
                }
           
                $scope.downloadallcol = function () {
                    for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                        if ($scope.Collateraldoc_list[i].migration_flag == 'N') {
                            DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
                        }
                        else {
                            DownloaddocumentService.OtherDownloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name, $scope.Collateraldoc_list[i].migration_flag);
                        }
                    }
                }

            }

        }

        $scope.PurposeofLoan_view = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/PurposeOfLoan.html',
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
                var url = 'api/MstApplicationView/GetPurposeofLoan';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtpurposeof_loan = resp.data.enduse_purpose;

                });

                var params =
                {
                    application2loan_gid: application2loan_gid
                }
                var url = 'api/MstApplicationView/GetLoantoBuyerList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.buyer_list = resp.data.mstbuyer_list;
                });
                var url = 'api/MstApplicationView/GetLoanProgramValueChain';
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
                    $scope.mstproductdtl_list = resp.data.mstproductdtl_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        var params =
        {
            application_gid: application_gid
        }
        var url = "api/MstApplicationEdit/GetGroupSummary";
        SocketService.getparams(url, params).then(function (resp) {
            $scope.group_list = resp.data.group_list;
            angular.forEach($scope.group_list, function (value, key) {
                var params = {
                    group_gid: value.group_gid
                };

                var url = 'api/MstApplicationView/GetGrouptoMemberList';
                SocketService.getparams(url, params).then(function (resp) {
                    value.groupmember_list = resp.data.groupmember_list;
                    value.expand = false;
                });
            });
        });

        $scope.momdocumentUpload = function (val) {
            if (($scope.txtmomdocument_title == null) || ($scope.txtmomdocument_title == '') || ($scope.txtmomdocument_title == undefined)) {
                $("#momdocument").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            } else {
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
                frm.append('document_title', $scope.txtmomdocument_title);
                frm.append('application_gid', $scope.application_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstCC/MOMDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.momuploaddocument_list = resp.data.momdocument_list;
                        unlockUI();

                        $("#momdocument").val('');
                        $scope.uploadfrm = undefined;

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtmomdocument_title = '';
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                        fnmom();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
            }
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.momdocumentcancel = function (application2momdoc_gid) {
            lockUI();
            var params = {
                application2momdoc_gid: application2momdoc_gid,
                application_gid: application_gid
            }
            var url = 'api/MstCC/MOM_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.momuploaddocument_list = resp.data.momdocument_list;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    fnmom();
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

        $scope.MOM_save = function () {

            var params = {
                mom_description: $scope.MOMDescription,
                application_gid: application_gid
            }
            var url = 'api/MstCC/MOMDescSave';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    fnmom();
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

        $scope.MOM_submit = function () {

            var params = {
                application_gid: application_gid,
            }
            lockUI();
            var url = "api/MstCC/PostMOMSubmit";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    fnmom();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });

        }

        $scope.MOM_resubmit = function () {

            var params = {
                application_gid: application_gid,
            }
            lockUI();
            var url = "api/MstCC/PostReMOMSubmit";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    fnmom();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });

        }

        $scope.gradingtool_view = function (application2gradingtool_gid) {
            var application2gradingtool_gid = application2gradingtool_gid;
            localStorage.setItem('application2gradingtool_gid', application2gradingtool_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstApplCreationGradingToolView";
            window.open(URL, '_blank');
        }

        $scope.visitreport_view = function (visitreport_gid) {
            var visitreport_gid = visitreport_gid;
            localStorage.setItem('visitreport_gid', visitreport_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstApplCreationVisitReportView";
            window.open(URL, '_blank');
        }

        $scope.institution_view = function (institution_gid) {
            var institution_gid = institution_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('institution_gid', institution_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstCcCommitteeInstitutionView?application_gid=" + application_gid + '&institution_gid='+ institution_gid + '&lspage='+ lspage +'&lspagetype='+ lspagetype;
            window.open(URL, '_blank');
        }

        $scope.individual_view = function (contact_gid) {
            var contact_gid = contact_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('contact_gid', contact_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstCcCommitteeIndividualView?application_gid=" + application_gid + '&contact_gid='+ contact_gid + '&lspage='+ lspage +'&lspagetype='+ lspagetype;
            window.open(URL, '_blank');
        }

        $scope.group_view = function (group_gid) {
            var group_gid = group_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('group_gid', group_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstCcCommitteeGroupView?application_gid=" + application_gid + '&group_gid='+ group_gid + '&lspage='+ lspage +'&lspagetype='+ lspagetype;
            window.open(URL, '_blank');
        }

        $scope.member_view = function (contact_gid) {
            var contact_gid = contact_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('contact_gid', contact_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstCcCommitteeIndividualView?application_gid=" + application_gid + '&contact_gid='+ contact_gid + '&lspage='+ lspage +'&lspagetype='+ lspagetype;
            window.open(URL, '_blank');
        }

        $scope.gotoGeneticCode = function () {
            $location.hash('GeneticCodedtl');
            $anchorScroll();
        };

        $scope.gotoEconomicCapital = function () {
            $location.hash('EconomicCapitaldtl');
            $anchorScroll();
        };

        $scope.gotoProductCharges = function () {
            $location.hash('ProductChargesdtl');
            $anchorScroll();
        };

        $scope.gotoAssessedScore = function () {
            $location.hash('AssessedScoredtl');
            $anchorScroll();
        };

        $scope.gotoVisitReport = function () {
            $location.hash('VisitReportdtl');
            $anchorScroll();
        };

        $scope.gotoCompanyInfo = function () {
            $location.hash('Companydtl');
            $anchorScroll();
        };

        $scope.gotoIndividualInfo = function () {
            $location.hash('Individualdtl');
            $anchorScroll();
        };

        $scope.gotoGroupInfo = function () {
            $location.hash('Groupdtl');
            $anchorScroll();
        };

        $scope.gotoTop = function () {
            $location.hash('Generaldtl');
            $anchorScroll();
        };

        $scope.scheduledmeeting_view = function () {
            var modalInstance = $modal.open({
                templateUrl: '/ScheduledMeetingdetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    application_gid: application_gid
                }
                lockUI();
                var url = 'api/MstCC/GetScheduleMeeting';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblccmeeting_no = resp.data.ccmeeting_no;
                    $scope.lblccmeeting_title = resp.data.ccmeeting_title;
                    $scope.lblstart_time = resp.data.start_time;
                    $scope.lblend_time = resp.data.end_time;
                    $scope.lblccmeeting_mode = resp.data.ccmeeting_mode;
                    $scope.lblccgroup_name = resp.data.ccgroup_name;
                    $scope.lbldescription = resp.data.description;
                    $scope.lblccmeeting_date = resp.data.ccmeeting_date;
                    $scope.lblotheruser_name = resp.data.otheruser_name;
                });

                var url = 'api/MstApplicationView/GetRMDetailsView';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtrmdepartment_name = resp.data.department_name;
                    $scope.txtrm_name = resp.data.RM_Name;
                    $scope.txtappl_initiateddate = resp.data.applicationinitiated_date;
                    $scope.txtunderwritten_date = resp.data.ccsubmitted_date;
                    $scope.txtunderwritten_by = resp.data.ccsubmitted_by;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.refresh = function () {
    
            activate();
            fnccmember_list();
            fnmom();

        }

        $scope.ccmember_present = function (ccmeeting2members_gid) {
            var params = {
                application_gid: application_gid,
                attendance_status: 'P',
                ccmeeting2members_gid: ccmeeting2members_gid
            }
            lockUI();
            var url = "api/MstCC/PostCCAttendance";
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000

                    });
                    fnccmember_list();
                    unlockUI();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }
        $scope.ccmember_absent = function (ccmeeting2members_gid) {
            var params = {
                application_gid: application_gid,
                attendance_status: 'A',
                ccmeeting2members_gid: ccmeeting2members_gid
            }
            lockUI();
            var url = "api/MstCC/PostCCAttendance";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); fnccmember_list();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }

        function fnccmember_list() {
            var param = {
                application_gid: application_gid
            }
            var url = 'api/MstCC/GetScheduleMeetingList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                //if ((resp.data.ccmember_list != null && resp.data.ccmember_list.length > 0) || (resp.data.otheruser_list != null && resp.data.otheruser_list.length > 0)) {
                if ((resp.data.status == true)) {
                    $scope.ccmember_list = resp.data.ccmember_list;
                    $scope.otheruser_list = resp.data.otheruser_list;
                    unlockUI();
                }
                else if (resp.data.status == false)
                    unlockUI();
            });

        }

        $scope.ccother_present = function (ccmeeting2othermembers_gid) {
            var params = {
                application_gid: application_gid,
                attendance_status: 'P',
                ccmeeting2othermembers_gid: ccmeeting2othermembers_gid
            }
            lockUI();
            var url = "api/MstCC/PostothersAttendance";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); fnccmember_list();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }
        $scope.ccother_absent = function (ccmeeting2othermembers_gid) {
            var params = {
                application_gid: application_gid,
                attendance_status: 'A',
                ccmeeting2othermembers_gid: ccmeeting2othermembers_gid
            }
            lockUI();
            var url = "api/MstCC/PostothersAttendance";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); fnccmember_list();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }



        $scope.undocc_member = function (ccmeeting2members_gid) {
            var params = {
                application_gid: application_gid,
                ccmeeting2members_gid: ccmeeting2members_gid
            }
            lockUI();
            var url = "api/MstCC/PostUndoCCAttendance";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); fnccmember_list();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }

        $scope.undo_others = function (ccmeeting2othermembers_gid) {
            var params = {
                application_gid: application_gid,
                ccmeeting2othermembers_gid: ccmeeting2othermembers_gid
            }
            lockUI();
            var url = "api/MstCC/PostUndoOthersAttendance";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); fnccmember_list();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }

        $scope.companyLLPnoView = function (companyllpno_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewCompanyandLLPNo.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       companyllpno_gid: companyllpno_gid
                   }
                var url = 'api/MstAPIVerifications/CompanyLLPViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcompany_name = resp.data.company_name;
                    $scope.txtroc_code = resp.data.roc_code;
                    $scope.txtregistration_no = resp.data.registration_no;
                    $scope.txtcompany_category = resp.data.company_category;

                    $scope.txtcompany_subcategory = resp.data.company_subcategory;
                    $scope.txtclass_of_company = resp.data.class_of_company;
                    $scope.txtnumber_of_members = resp.data.number_of_members;
                    $scope.txtdate_of_incorporation = resp.data.date_of_incorporation;

                    $scope.txtcompany_status = resp.data.company_status;
                    $scope.txtregistered_address = resp.data.registered_address;
                    $scope.txtalternative_address = resp.data.alternative_address;
                    $scope.txtemail_address = resp.data.email_address;

                    $scope.txtlisted_status = resp.data.listed_status;
                    $scope.txtsuspended_at_stock_exchange = resp.data.suspended_at_stock_exchange;
                    $scope.txtdate_of_last_AGM = resp.data.date_of_last_AGM;
                    $scope.txtdate_of_balance_sheet = resp.data.date_of_balance_sheet;

                    $scope.txtpaid_up_capital = resp.data.paid_up_capital;
                    $scope.txtauthorised_capital = resp.data.authorised_capital;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.mcasignView = function (mcasignatories_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/MCASignatoriesViewDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       mcasignatories_gid: mcasignatories_gid
                   }
                var url = 'api/MstAPIVerifications/MCASignatoriesViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.mcasignatorydetails_list = resp.data.mcasignatorydetails_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.IECView = function (iecdtl_gid) {
            var iecdtl_gid = iecdtl_gid;
            localStorage.setItem('iecdtl_gid', iecdtl_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/IECDetailedProfileView";
            window.open(URL, '_blank');
        }

        $scope.FSSAIView = function (fssailicenseauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewFSSAIDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       fssailicenseauthentication_gid: fssailicenseauthentication_gid
                   }
                var url = 'api/MstAPIVerifications/FSSAIViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtfssai_status = resp.data.fssai_status;
                    $scope.txtlicense_type = resp.data.license_type;
                    $scope.txtlicense_no = resp.data.license_no;
                    $scope.txtfirm_name = resp.data.firm_name;
                    $scope.txtaddress = resp.data.address;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.FDAView = function (fdalicenseauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewFDADetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       fdalicenseauthentication_gid: fdalicenseauthentication_gid
                   }
                var url = 'api/MstAPIVerifications/FDAViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtstore_name = resp.data.store_name;
                    $scope.txtcontact_no = resp.data.contact_no;
                    $scope.txtlicense_detail = resp.data.license_detail;
                    $scope.txtname = resp.data.name;
                    $scope.txtaddress = resp.data.address;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.authenticationView = function (institution2branch_gid) {
            var institution2branch_gid = institution2branch_gid;
            localStorage.setItem('institution2branch_gid', institution2branch_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/GSTAuthenticationView";
            window.open(URL, '_blank');
        }

        $scope.verificationView = function (institution2branch_gid) {
            var institution2branch_gid = institution2branch_gid;
            localStorage.setItem('institution2branch_gid', institution2branch_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/GSPGSTINAuthenticationView";
            window.open(URL, '_blank');
        }

        $scope.returnfillingView = function (institution2branch_gid) {
            var institution2branch_gid = institution2branch_gid;
            localStorage.setItem('institution2branch_gid', institution2branch_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/GSPGSTReturnFilingView";
            window.open(URL, '_blank');
        }

        $scope.LPGIDView = function (lpgiddtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewLPGID.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       lpgiddtl_gid: lpgiddtl_gid
                   }
                var url = 'api/MstAPIVerifications/LPGIDViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtstatus = resp.data.result.status;
                    $scope.txtApproximateSubsidyAvailed = resp.data.result.ApproximateSubsidyAvailed;
                    $scope.txtSubsidizedRefillConsumed = resp.data.result.SubsidizedRefillConsumed;
                    $scope.txtpin = resp.data.result.pin;

                    $scope.txtConsumerEmail = resp.data.result.ConsumerEmail;
                    $scope.txtDistributorCode = resp.data.result.DistributorCode;
                    $scope.txtBankName = resp.data.result.BankName;
                    $scope.txtIFSCCode = resp.data.result.IFSCCode;

                    $scope.txtAadhaarNo = resp.data.result.AadhaarNo;
                    $scope.txtConsumerContact = resp.data.result.ConsumerContact;
                    $scope.txtDistributorAddress = resp.data.result.DistributorAddress;
                    $scope.txtConsumerName = resp.data.result.ConsumerName;

                    $scope.txtConsumerNo = resp.data.result.ConsumerNo;
                    $scope.txtDistributorName = resp.data.result.DistributorName;
                    $scope.txtBankAccountNo = resp.data.result.BankAccountNo;
                    $scope.txtGivenUpSubsidy = resp.data.result.GivenUpSubsidy;

                    $scope.txtConsumerAddress = resp.data.result.ConsumerAddress;
                    $scope.txtLastBookingDate = resp.data.result.LastBookingDate;
                    $scope.txtTotalRefillConsumed = resp.data.result.TotalRefillConsumed;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.ShopView = function (shopandestablishment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewShopDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       shopandestablishment_gid: shopandestablishment_gid
                   }
                var url = 'api/MstAPIVerifications/ShopViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcategory = resp.data.result.category;
                    $scope.txtstatus = resp.data.result.status;
                    $scope.txtcommenceDate = resp.data.result.commenceDate;
                    $scope.txttotalWorkers = resp.data.result.totalWorkers;

                    $scope.txtfatherNameOfOccupier = resp.data.result.fatherNameOfOccupier;
                    $scope.txtemail = resp.data.result.email;
                    $scope.txtwebsiteUrl = resp.data.result.websiteUrl;
                    $scope.txtpdfLink = resp.data.result.pdfLink;

                    $scope.txtownerName = resp.data.result.ownerName;
                    $scope.txtaddress = resp.data.result.address;
                    $scope.txtapplicantName = resp.data.result.applicantName;
                    $scope.txtvalidFrom = resp.data.result.validFrom;

                    $scope.txtnatureOfBusiness = resp.data.result.natureOfBusiness;
                    $scope.txtvalidTo = resp.data.result.validTo;
                    $scope.txtregistrationDate = resp.data.result.registrationDate;


                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.RCAuthAdvancedView = function (vehiclercauthadvanced_gid) {
            var vehiclercauthadvanced_gid = vehiclercauthadvanced_gid;
            localStorage.setItem('vehiclercauthadvanced_gid', vehiclercauthadvanced_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/RCAuthAdvancedView";
            window.open(URL, '_blank');
        }

        $scope.RCSearchView = function (vehiclercsearch_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewRCSearchDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       vehiclercsearch_gid: vehiclercsearch_gid
                   }
                var url = 'api/MstAPIVerifications/RCSearchViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtrc_manu_month_yr = resp.data.result.rc_manu_month_yr;
                    $scope.txtrc_maker_model = resp.data.result.rc_maker_model;
                    $scope.txtrc_f_name = resp.data.result.rc_f_name;
                    $scope.txtrc_eng_no = resp.data.result.rc_eng_no;

                    $scope.txtrc_owner_name = resp.data.result.rc_owner_name;
                    $scope.txtrc_vh_class_desc = resp.data.result.rc_vh_class_desc;
                    $scope.txtrc_present_address = resp.data.result.rc_present_address;
                    $scope.txtrc_color = resp.data.result.rc_color;

                    $scope.txtrc_regn_no = resp.data.result.rc_regn_no;
                    $scope.txttax_paid_upto = resp.data.result.tax_paid_upto;
                    $scope.txtrc_maker_desc = resp.data.result.rc_maker_desc;
                    $scope.txtrc_chasi_no = resp.data.result.rc_chasi_no;

                    $scope.txtrc_mobile_no = resp.data.result.rc_mobile_no;
                    $scope.txtrc_registered_at = resp.data.result.rc_registered_at;
                    $scope.txtrc_valid_upto = resp.data.result.rc_valid_upto;
                    $scope.txtrc_regn_dt = resp.data.result.rc_regn_dt;

                    $scope.txtrc_financer = resp.data.result.rc_financer;
                    $scope.txtrc_permanent_address = resp.data.result.rc_permanent_address;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.PropertyTaxView = function (propertytax_gid) {
            var propertytax_gid = propertytax_gid;
            localStorage.setItem('propertytax_gid', propertytax_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/PropertyTaxView";
            window.open(URL, '_blank');
        }

        $scope.Approve = function (txtremarks) {
            var params = {
                application_gid: application_gid,
                approval_status: 'Approved',
                remarks: txtremarks
            }
            lockUI();
            var url = "api/MstCC/PostCCApprove";
            SocketService.post(url, params).then(function (resp) {
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
                $scope.txtqueries = "";
                fnmom();
            });
        }
        $scope.Reject = function (txtremarks) {
            var params = {
                application_gid: application_gid,
                approval_status: 'Rejected',
                remarks: txtremarks
            }
            lockUI();
            var url = "api/MstCC/PostCCApprove";
            SocketService.post(url, params).then(function (resp) {
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
                $scope.txtqueries = "";
                fnmom();
            });
        }

        $scope.cc_remarksview = function (ccmeeting2members_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewccremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       application_gid: application_gid,
                       ccmeeting2members_gid: ccmeeting2members_gid
                   }
                var url = 'api/MstCC/ViewCCRemarks';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblremarks = resp.data.remarks;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }
        $scope.other_remarksview = function (ccmeeting2othermembers_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewccremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       ccmeeting2othermembers_gid: ccmeeting2othermembers_gid,
                       application_gid: application_gid,
                   }
                var url = 'api/MstCC/ViewOtherRemarks';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblremarks = resp.data.remarks;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.epicauthenticationView = function (kycepicauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewEpicAuthentication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycepicauthentication_gid: kycepicauthentication_gid
                   }
                var url = 'api/KycView/VoterIDViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.name = resp.data.result.name;
                    $scope.rln_name = resp.data.result.rln_name;
                    $scope.rln_type = resp.data.result.rln_type;
                    $scope.gender = resp.data.result.gender;

                    $scope.district = resp.data.result.district;
                    $scope.ac_name = resp.data.result.ac_name;
                    $scope.pc_name = resp.data.result.pc_name;
                    $scope.state = resp.data.result.state;

                    $scope.epic_no = resp.data.result.epic_no;
                    $scope.dob = resp.data.result.dob;
                    $scope.age = resp.data.result.age;
                    $scope.part_no = resp.data.result.part_no;

                    $scope.slno_inpart = resp.data.result.slno_inpart;
                    $scope.ps_name = resp.data.result.ps_name;
                    $scope.part_name = resp.data.result.part_name;
                    $scope.last_update = resp.data.result.last_update;

                    $scope.ps_lat_long = resp.data.result.ps_lat_long;
                    $scope.rln_name_v1 = resp.data.result.rln_name_v1;
                    $scope.rln_name_v2 = resp.data.result.rln_name_v2;
                    $scope.rln_name_v3 = resp.data.result.rln_name_v3;

                    $scope.section_no = resp.data.result.section_no;
                    $scope.id = resp.data.result.id;
                    $scope.name_v1 = resp.data.result.name_v1;
                    $scope.name_v2 = resp.data.result.name_v2;

                    $scope.name_v3 = resp.data.result.name_v3;
                    $scope.ac_no = resp.data.result.ac_no;
                    $scope.st_code = resp.data.result.st_code;
                    $scope.house_no = resp.data.result.house_no;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.dlauthenticationView = function (kycdlauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewDLAuthentication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycdlauthentication_gid: kycdlauthentication_gid
                   }
                var url = 'api/KycView/DrivingLicenseViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.status = resp.data.result.status;
                    $scope.fatherhusband = resp.data.result.fatherhusband;
                    $scope.bloodGroup = resp.data.result.bloodGroup;
                    $scope.dlNumber = resp.data.result.dlNumber;

                    $scope.name = resp.data.result.name;
                    $scope.dob = resp.data.result.dob;
                    $scope.issueDate = resp.data.result.issueDate;

                    $scope.validity_nonTransport = resp.data.result.validity.nonTransport;
                    $scope.validity_transport = resp.data.result.validity.transport;

                    $scope.statusDetails_remarks = resp.data.result.statusDetails.remarks;
                    $scope.statusDetails_to = resp.data.result.statusDetails.to;
                    $scope.statusDetails_from = resp.data.result.statusDetails.from;

                    $scope.address_list = resp.data.result.address;
                    $scope.covDetails_list = resp.data.result.covDetails;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.passportauthenticationView = function (kycpassportauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewPassportAuthentication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycpassportauthentication_gid: kycpassportauthentication_gid
                   }
                var url = 'api/KycView/PassportViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.passportNumberFromSource = resp.data.result.passportNumber.passportNumberFromSource;
                    $scope.passportNumberMatch = resp.data.result.passportNumber.passportNumberMatch;

                    $scope.typeOfApplication = resp.data.result.typeOfApplication;
                    $scope.applicationDate = resp.data.result.applicationDate;

                    $scope.dispatchedOnFromSource = resp.data.result.dateOfIssue.dispatchedOnFromSource;
                    $scope.dateOfIssueMatch = resp.data.result.dateOfIssue.dateOfIssueMatch;

                    $scope.nameFromPassport = resp.data.result.name.nameFromPassport;
                    $scope.surnameFromPassport = resp.data.result.name.surnameFromPassport;
                    $scope.nameMatch = resp.data.result.name.nameMatch;
                    $scope.nameScore = resp.data.result.name.nameScore;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.GSTSBPANView = function (kycgstsbpan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewGSTSBPAN.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycgstsbpan_gid: kycgstsbpan_gid
                   }
                var url = 'api/KycView/GSTSBPANViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.GSTSBPAN_list = resp.data.result;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.IFSCAuthenticationView = function (kycifscauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewIFSCAuthentication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycifscauthentication_gid: kycifscauthentication_gid
                   }
                var url = 'api/KycView/IFSCViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.city = resp.data.result.city;
                    $scope.office = resp.data.result.office;
                    $scope.district = resp.data.result.district;
                    $scope.ifsc = resp.data.result.ifsc;
                    $scope.micr = resp.data.result.micr;
                    $scope.state = resp.data.result.state;
                    $scope.contact = resp.data.result.contact;
                    $scope.branch = resp.data.result.branch;
                    $scope.address = resp.data.result.address;
                    $scope.bank = resp.data.result.bank;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.BankAccVerificationView = function (kycbankaccverification_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewBankAccVerification.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycbankaccverification_gid: kycbankaccverification_gid
                   }
                var url = 'api/KycView/BankAccViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.bankTxnStatus = resp.data.result.bankTxnStatus;
                    $scope.accountNumber = resp.data.result.accountNumber;
                    $scope.ifsc = resp.data.result.ifsc;
                    $scope.accountName = resp.data.result.accountName;
                    $scope.bankResponse = resp.data.result.bankResponse;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.getReApprovalRequest = function (ccmeeting2members_gid, CCMember_name) {
            var modalInstance = $modal.open({
                templateUrl: '/getReApprovalmodal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                //var params =
                //{
                //    ccmeeting2members_gid: ccmeeting2members_gid,
                //    application_gid: application_gid

                //}
                //var url = 'api/MstCC/GetApprovalInitiate';
                //lockUI();
                //SocketService.getparams(url, params).then(function (resp) {
                //    unlockUI();
                //    // $scope.employee_list = resp.data.employee_list;
                //    $scope.txtccmember_name = resp.data.ccmember_name;
                //    $scope.ccmember_gid = resp.data.ccmember_gid;                                  

                //});
                //var url = 'api/OsdMstActivity/GetTeamSummary';
                //SocketService.get(url).then(function (resp) {
                //    $scope.supportdtllist = resp.data.supportdtl;
                //});
                $scope.ok = function () {
                    modalInstance.close('closed');
                };

                $scope.txtccmember_name = CCMember_name;

                $scope.getreapprovalclick = function () {
                    var params = {
                        ccmember_name: $scope.txtccmember_name,
                        ccmeeting2members_gid: ccmeeting2members_gid,
                        application_gid: application_gid

                    }
                    lockUI();
                    var url = "api/MstCC/PostApprovalInitiate";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            fnccmember_list();

                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();

                        }
                    });
                }
            }
        }

        $scope.cancelapprovalinitiate = function (ccmeeting2members_gid) {
            var params = {
                ccmeeting2members_gid: ccmeeting2members_gid,
                application_gid: application_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Cancel the Approval Initiated ?',
                showCancelButton: true,
                cancelButtonText: 'Close',
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, Cancel it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = "api/MstCC/CancelApprovalInitiate"
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            unlockUI();
                            fnccmember_list();

                            $state.go('app.MstStartScheduledMeeting');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            fnccmember_list();
                        }
                    });
                    SweetAlert.swal('Cancelled Successfully!');
                }

            });
        };

        $scope.TaggedCase_View = function (data) {
            var modalInstance = $modal.open({
                templateUrl: '/TaggedCaseView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.lblcinNumber = data.cin_number;
                $scope.lblcaseType = data.case_type;
                $scope.lblcaseStatus = data.case_status;
                $scope.lblpetitioner = data.petitioner;
                $scope.lblpetitionerAddress = data.petitioner_address;
                $scope.lblrespondent = data.respondent;
                $scope.lblrespondentAddress = data.respondent_address;
                $scope.lblcaseTypeName = data.casetype_name;
                $scope.lblcaseName = data.case_name
                $scope.lblcourtType = data.court_type;
                $scope.lbldistrict = data.district;
                $scope.lblstate = data.state;
                $scope.lblyear = data.year;
                $scope.lblgfc_updated_at = data.gfc_updated_at;
                $scope.lblgfc_uniqueid = data.gfc_uniqueid;
                $scope.lblcasedetails_link = data.casedetails_link;





                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.download_crimereport = function (val1, val2) {
            var link = document.createElement("a");
            link.download = val2;
            var uri = val1;
            link.href = uri;
            link.click();
        }

        $scope.getapplicationviewinfo = function () {
            fnapplicationviewinfo();
        }

        $scope.getapiverifiedinfo = function () {
            fngetapiverifiedinfo();
        }

        $scope.getcrimecases = function () {
            fngetcrimecases();
        }

        $scope.getcamviewdownload = function () {
            fncamviewdownload();
        }

        $scope.getmom = function () {
            fnmom();
        }

        $scope.getattendance = function () {
            fnattendance();
        }

        function fnapplicationviewinfo() {
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstApplicationView/GetApplicationBasicView';
            lockUI();
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
                $scope.momapproval_flag = resp.data.momapproval_flag;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.txtproduct_name = resp.data.product_name;
                $scope.txtsector_name = resp.data.sector_name;
                $scope.txtcategory_name = resp.data.category_name;
                $scope.txtvariety_name = resp.data.variety_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;
                $scope.txtprogram_name = resp.data.program_name;
                $scope.cccompleted_flag = resp.data.cccompleted_flag;
            });

            var url = 'api/MstApplicationView/GetMobileMailDetailsView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtprimary_number = resp.data.primary_mobileno;
                $scope.basicmobileno_list = resp.data.mobilenumber_list;
                $scope.txtprimary_emailassdress = resp.data.primary_email;
                $scope.mailaddress_list = resp.data.mail_list;
            });

            var url = 'api/MstApplicationView/GetGeneticDetailsView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.geneticcode_list = resp.data.geneticdetails_list;
            });

            var url = 'api/MstApplicationView/GetProductChargesDtl';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;
                $scope.lblamountseperator = (parseInt($scope.txtoveralllimit_amt.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;
                $scope.loandtls_list = resp.data.mstLoan_list;
                for (var i = 0; i < $scope.loandtls_list.length; i++) {
                    var lblloanfacility_amount = (parseInt($scope.loandtls_list[i].loanfacility_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.loandtls_list[i].loanfacility_amountinwords = defaultamountwordschange(lblloanfacility_amount);
                    $scope.loandtls_list[i].lblloanfacility_amount = lblloanfacility_amount;
                }
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

                if ($scope.Collateral_list != null) {
                    for (var i = 0; i < $scope.Collateral_list.length; i++) {
                        var lblguideline_value = (parseInt($scope.Collateral_list[i].guideline_value.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.Collateral_list[i].guideline_valueinwords = defaultamountwordschange(lblguideline_value);
                        $scope.Collateral_list[i].lblguideline_value = lblguideline_value;

                    }

                    for (var i = 0; i < $scope.Collateral_list.length; i++) {
                        var lblmarket_value = (parseInt($scope.Collateral_list[i].market_value.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.Collateral_list[i].market_valueinwords = defaultamountwordschange(lblmarket_value);
                        $scope.Collateral_list[i].lblmarket_value = lblmarket_value;

                    }

                    for (var i = 0; i < $scope.Collateral_list.length; i++) {
                        var lblforcedsource_value = (parseInt($scope.Collateral_list[i].forcedsource_value.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.Collateral_list[i].forcedsource_valueinwords = defaultamountwordschange(lblforcedsource_value);
                        $scope.Collateral_list[i].lblforcedsource_value = lblforcedsource_value;

                    }

                    for (var i = 0; i < $scope.Collateral_list.length; i++) {
                        var lblcollateralSSV_value = (parseInt($scope.Collateral_list[i].collateralSSV_value.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.Collateral_list[i].collateralSSV_valueinwords = defaultamountwordschange(lblcollateralSSV_value);
                        $scope.Collateral_list[i].lblcollateralSSV_value = lblcollateralSSV_value;

                    }
                }
                if ($scope.servicecharge_List != null) {
                    for (var i = 0; i < $scope.servicecharge_List.length; i++) {
                        var lblprocessing_fee = (parseInt($scope.servicecharge_List[i].processing_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.servicecharge_List[i].processingfeeinwords = defaultamountwordschange(lblprocessing_fee);
                        $scope.servicecharge_List[i].lblprocessing_fee = lblprocessing_fee;

                    }
                    for (var i = 0; i < $scope.servicecharge_List.length; i++) {
                        var lbldoc_charges = (parseInt($scope.servicecharge_List[i].doc_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.servicecharge_List[i].doc_chargesinwords = defaultamountwordschange(lbldoc_charges);
                        $scope.servicecharge_List[i].lbldoc_charges = lbldoc_charges;

                    }
                    for (var i = 0; i < $scope.servicecharge_List.length; i++) {
                        var lblfieldvisit_charge = (parseInt($scope.servicecharge_List[i].fieldvisit_charge.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.servicecharge_List[i].fieldvisit_chargeinwords = defaultamountwordschange(lblfieldvisit_charge);
                        $scope.servicecharge_List[i].lblfieldvisit_charge = lblfieldvisit_charge;

                    }
                    for (var i = 0; i < $scope.servicecharge_List.length; i++) {
                        var lbladhoc_fee = (parseInt($scope.servicecharge_List[i].adhoc_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.servicecharge_List[i].adhoc_feeinwords = defaultamountwordschange(lbladhoc_fee);
                        $scope.servicecharge_List[i].lbladhoc_fee = lbladhoc_fee;

                    }
                    for (var i = 0; i < $scope.servicecharge_List.length; i++) {
                        var lbllife_insurance = (parseInt($scope.servicecharge_List[i].life_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.servicecharge_List[i].life_insuranceinwords = defaultamountwordschange(lbllife_insurance);
                        $scope.servicecharge_List[i].lbllife_insurance = lbllife_insurance;

                    }
                    for (var i = 0; i < $scope.servicecharge_List.length; i++) {
                        var lblacct_insurance = (parseInt($scope.servicecharge_List[i].acct_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.servicecharge_List[i].acct_insuranceinwords = defaultamountwordschange(lblacct_insurance);
                        $scope.servicecharge_List[i].lblacct_insurance = lblacct_insurance;

                    }
                }
                if ($scope.txtsecurity_value != 'undefined') {
                    $scope.txtsecurity_value = resp.data.security_value;
                    $scope.lblamountseperator = (parseInt($scope.txtsecurity_value.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblsecurity_value = defaultamountwordschange($scope.lblamountseperator);
                }
            });


            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM'
            }
            var url = 'api/MstApplicationView/GetGradingToolDtls';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.gradetoolsummary_list = resp.data.mstgradetoolsummary_list;
            });


            var params = {
                application_gid: application_gid,
                statusupdated_by: 'Credit',
            }
            var url = 'api/MstApplicationGradingTool/GetGradingTool';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.gradingtoolcredit_list = resp.data.grading_list;

            });

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM'

            }
            var url = 'api/MstApplicationView/GetVisitReportList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.VisitReport_List = resp.data.VisitReport_List;
            });

            var url = 'api/MstApplicationView/GetIndividualList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditIndividual_List = resp.data.individual_List;
            });

            var url = 'api/MstApplicationView/GetInstitutionList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditInstitution_List = resp.data.institution_List;
            });


            var params = {
                application_gid: application_gid,
                statusupdated_by: 'Credit',
            }
            var url = 'api/MstApplicationVisitReport/GetVisitReportList';

            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.VisitReportCreditList = resp.data.VisitReportList;

            });

            var params = {
           application_gid: application_gid
            }
            var url = "api/MstApplicationEdit/GetGroupSummary";
          
            SocketService.getparams(url, params).then(function (resp) {
                $scope.group_list = resp.data.group_list;
                angular.forEach($scope.group_list, function (value, key) {
                    var params = {
                        group_gid: value.group_gid
                    };

                    var url = 'api/MstApplicationView/GetGrouptoMemberList';
                    SocketService.getparams(url, params).then(function (resp) {
                        value.groupmember_list = resp.data.groupmember_list;
                        value.expand = false;
                    });
                });
            });
        }

        function fngetapiverifiedinfo () {
            var param = {
                application_gid: application_gid
            }

            var url = 'api/KycView/GetPANAuthenticationDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.panauthentication_list = resp.data.panauthentication_list;
            });

            var url = 'api/KycView/GetDLAuthenticationDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.dlauthentication_list = resp.data.dlauthentication_list;
            });
            var url = 'api/KycView/GetEPICAuthenticationDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.epicauthentication_list = resp.data.epicauthentication_list;
            });

            var url = 'api/KycView/GetPassportAuthenticationDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.passportauthentication_list = resp.data.passportauthentication_list;
            });

            var url = 'api/KycView/GetIFSCAuthenticationDtl';
             lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.ifscauthentication_list = resp.data.ifscauthentication_list;
            });

            var url = 'api/KycView/GetBankAccVerificationDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.bankaccverification_list = resp.data.bankaccverification_list;
            });

            var url = 'api/KycView/GetGSTSBPANDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.gstsbpan_list = resp.data.gstsbpan_list;
            });


            var url = 'api/KycView/GetUDYAMAuthenticationDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.udyamauthentication_list = resp.data.udyamauthentication_list;
            });

            var url = 'api/MstAPIVerifications/AppnTANList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.tan_list = resp.data.tan_list;
                $scope.tanlist_length = $scope.tan_list.length;
            });

            var url = 'api/MstAPIVerifications/AppnCompanyLLPList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.cin_list = resp.data.cin_list;
                $scope.cinlist_length = $scope.cin_list.length;
            });
            var url = 'api/MstAPIVerifications/AppnMCASignatureList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mcasign_list = resp.data.cin_list;
                $scope.mcasignlist_length = $scope.mcasign_list.length;
            });

            var url = 'api/MstAPIVerifications/AppnIECDetailedList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.IECDetailed_list = resp.data.IECDetailed_list;
                $scope.IECDetailedlist_length = $scope.IECDetailed_list.length;
            });

            var url = 'api/MstAPIVerifications/AppnFSSAIList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.fssai_list = resp.data.fssai_list;
                $scope.fssailist_length = $scope.fssai_list.length;
            });

            var url = 'api/MstAPIVerifications/AppnFDAList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.fda_list = resp.data.fda_list;
                $scope.fdalist_length = $scope.fda_list.length;
            });

            var url = 'api/MstAPIVerifications/AppnGSTVerificationList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.gstverification_list = resp.data.gst_list;
                $scope.gstverificationlist_length = $scope.gstverification_list.length;
            });

            var url = 'api/MstAPIVerifications/AppnGSTReturnFilingList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.gstreturnfiling_list = resp.data.gst_list;
                $scope.gstreturnfilinglist_length = $scope.gstreturnfiling_list.length;
            });

            var url = 'api/MstAPIVerifications/AppnGSTAuthenticationList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.gstauthentication_list = resp.data.gst_list;
                $scope.gstauthenticationlist_length = $scope.gstauthentication_list.length;
            });

            var url = 'api/MstAPIVerifications/AppnLPGIDAuthenticationList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.LPGID_list = resp.data.LPGID_list;
                $scope.LPGIDlist_length = $scope.LPGID_list.length;
            });

            var url = 'api/MstAPIVerifications/AppnShopList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.shop_list = resp.data.shop_list;
                $scope.shoplist_length = $scope.shop_list.length;
            });

            var url = 'api/MstAPIVerifications/AppnRCAuthAdvancedList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.RCAuthAdvanced_list = resp.data.RCAuthAdvanced_list;
                $scope.RCAuthAdvancedlist_length = $scope.RCAuthAdvanced_list.length;
            });

            var url = 'api/MstAPIVerifications/AppnRCSearchList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.RCSearch_list = resp.data.RCSearch_list;
                $scope.RCSearchlist_length = $scope.RCSearch_list.length;
            });

            var url = 'api/MstAPIVerifications/AppnPropertyTaxList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.PropertyTax_list = resp.data.PropertyTax_list;
                $scope.PropertyTaxlist_length = $scope.PropertyTax_list.length;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCC/GetCCRequestorlist';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.requestorlist = resp.data.ccrequestordtl;
            });
        }

        function fngetcrimecases () {

            var paramcrimetc = {
                application_gid: application_gid
            }
            var url = 'api/CrimeCheckAPI/GetCCCaseTaggedIndividualSummary';
            lockUI();
            SocketService.getparams(url, paramcrimetc).then(function (resp) {
                unlockUI();
                $scope.cccasetaggedindividual_list = resp.data.cccasetaggedindividual_list;
            });
            var url = 'api/CrimeCheckAPI/GetCCCaseTaggedInstitutionSummary';
            lockUI();
            SocketService.getparams(url, paramcrimetc).then(function (resp) {
                unlockUI();
                $scope.cccasetaggedinstitution_list = resp.data.cccasetaggedinstitution_list;
            });
            var url = 'api/CrimeCheckAPI/GetCCReportInstitutionSummary';
            lockUI();
            SocketService.getparams(url, paramcrimetc).then(function (resp) {
                unlockUI();
                $scope.ccreportinstitution_list = resp.data.ccreportinstitution_list;
            });
            var url = 'api/CrimeCheckAPI/GetCCReportIndividualSummary';
            lockUI();
            SocketService.getparams(url, paramcrimetc).then(function (resp) {
                unlockUI();
                $scope.ccreportindividual_list = resp.data.ccreportindividual_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCC/GetCCRequestorlist';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.requestorlist = resp.data.ccrequestordtl;
            });

        }

        function fncamviewdownload () {
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstAppCreditUnderWriting/GetCAM';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.camuploaddocument_list = resp.data.camdocument_list;
            });

           
            var url = 'api/MstCC/GetCCRequestorlist';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.requestorlist = resp.data.ccrequestordtl;
            });


            var url = "api/MstCAMGeneration/GetApp2CAM";
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();

                $scope.cam_content = resp.data.template_content;
                $scope.lspath = resp.data.lspath;
                $scope.lsname = resp.data.lsname;
            });

        }

        function fnmom () {

            var param = {
                application_gid: application_gid
            }
            var url = 'api/MstCC/GetAdminPrivilege';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.privilege_gid = resp.data.privilege_gid;
            });


            var url = 'api/MstApplicationView/GetApplicationBasicView';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();

                $scope.momapproval_flag = resp.data.momapproval_flag;
                $scope.cccompleted_flag = resp.data.cccompleted_flag;

            });

            var url = 'api/MstCC/GetMOMDescription';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.MOMDescription = resp.data.mom_description;
                $scope.momuploaddocument_list = resp.data.momdocument_list;
            });

            var url = 'api/MstCC/GetApprovalList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.ccmembers_list = resp.data.ccmembers_list;
                for (var i = 0; i < $scope.ccmembers_list.length; i++) {
                    if ($scope.ccmembers_list[i].ccapproval_flag == 'Y') {
                        $scope.submit_to_approval = true;
                        break;
                    }
                }

            });

            var url = 'api/MstCC/GetMOMApprovalFlag';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.proceed_flag = resp.data.proceed_flag;
            });

            var url = 'api/MstCC/GetMOMDescriptions';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mom_descflag = resp.data.mom_descflag;
            });

            var url = 'api/MstCC/GetMomFlag';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mom_flag = resp.data.mom_flag;
            });

            var url = 'api/MstCC/GetAdminPrivilege';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.privilege_gid = resp.data.privilege_gid;
            });

            var url = 'api/MstCC/GetApprovalFlag';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.approval_flag = resp.data.approval_flag;
            });

            var url = 'api/MstCC/GetApprovalShowFlag';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.approvalshow_flag = resp.data.approvalshow_flag;
            });

            var url = 'api/MstCC/GetScheduleMeeting';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.ccmember_list = resp.data.ccmember_list;
                $scope.otheruser_list = resp.data.otheruser_list;
            });

            var url = 'api/MstCC/GetCCRequestorlist';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.requestorlist = resp.data.ccrequestordtl;
            });


            var param = {
                application_gid: application_gid
            }
            var url = 'api/MstCC/GetCCRequestorlist';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.requestorlist = resp.data.ccrequestordtl;
            });

            var url = 'api/MstCC/GetMOMReapproval';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mom_reapprove = resp.data.mom_reapprove;
            });
            var url = 'api/MstCC/GetMOMRemail';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mom_ccmailflag = resp.data.mom_ccmailflag;
            });
        }

        function fnattendance () {

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCC/GetScheduleMeeting';
          
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lblccadmin_name = resp.data.ccadmin_name;
               
              
            });

           

            var url = 'api/MstCC/GetScheduleMeeting';

            SocketService.getparams(url, params).then(function (resp) {
                lockUI();
                if ((resp.data.ccmember_list != null && resp.data.ccmember_list.length > 0) || (resp.data.otheruser_list != null && resp.data.otheruser_list.length > 0)) {
            
                    $scope.ccmember_list = resp.data.ccmember_list;
                    $scope.otheruser_list = resp.data.otheruser_list;
                    unlockUI();
                }
                else if (resp.data.status == false)
                    unlockUI();
            });



            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCC/GetCCRequestorlist';
            SocketService.getparams(url, params).then(function (resp) {
               
                $scope.requestorlist = resp.data.ccrequestordtl;
            });

            var url = 'api/MstCC/GetAdminPrivilege';
            SocketService.getparams(url, params).then(function (resp) {
               
                $scope.privilege_gid = resp.data.privilege_gid;
            });
        }


        //$scope.getapiverifiedinfo = function () {
        //    $location.hash('API Verified Info');
        //    $anchorScroll();
        //};


        $scope.downloadall = function () {
            for (var i = 0; i < $scope.momuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.momuploaddocument_list[i].document_path, $scope.momuploaddocument_list[i].document_name);
            }
        }

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
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

        $scope.editRule_Company = function (application_gid,institution_gid,vertical_gid) {
            $location.url('app/MstVerticalBRETrnRule?lsinstitution_gid=' + institution_gid + '&lsvertical_gid=' + vertical_gid + '&lsapplication_gid=' + application_gid + '&lscompany=' + "Company" + '&lspage=' + lspage + '&lspagename=' + lspagename); //+ '&lscompany=' + "Company"
        }

        $scope.editRule_Individual = function (application_gid,contact_gid,vertical_gid) {
            $location.url('app/MstVerticalBRETrnRule?lscontact_gid=' + contact_gid + '&lsvertical_gid=' + vertical_gid + '&lsapplication_gid=' + application_gid + '&lsindividual=' + "Individual" + '&lspage=' + lspage + '&lspagename=' + lspagename); // + '&lsindividual=' + "Individual"
        }

        $scope.loandetails = function (customer_urn) {
            $location.url('app/MstRMLoanDetailsDtls?application_gid=' + application_gid + '&customer_urn=' + customer_urn + '&lspage=StartScheduled');
        }
    }
})();
