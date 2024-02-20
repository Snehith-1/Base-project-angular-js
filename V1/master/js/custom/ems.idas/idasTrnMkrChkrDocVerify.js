(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnMkrChkrDocVerify', idasTrnMkrChkrDocVerify);

    idasTrnMkrChkrDocVerify.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$anchorScroll', 'DownloaddocumentService','cmnfunctionService'];

    function idasTrnMkrChkrDocVerify($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $anchorScroll, DownloaddocumentService,cmnfunctionService) {

        var sanction_gid;
        var customer_gid;
        activate();

        var vm = this;
        vm.title = 'idasTrnMkrChkrDocVerify';

        function activate() {


            $scope.DivFile = false;
            $scope.show = true;
            $scope.options = false;
            sanction_gid = localStorage.getItem('sanction_gid');

            //$location.hash('down');
            //$anchorScroll();

            var url = 'api/IdasTrnSanctionDoc/SanctionDtlsView';
            var params = {
                sanction_gid: sanction_gid
            };

            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionrefno = resp.data.sanctionrefno;
                $scope.SanctionDate = resp.data.SanctionDate;
                $scope.SanctionAmount = resp.data.SanctionAmount;
                $scope.FacilityType = resp.data.FacilityType;

                $scope.customerName = resp.data.customerName;
                $scope.Customerurn = resp.data.Customerurn;
                $scope.collateral_security = resp.data.collateral_security;
                $scope.zonalHeadName = resp.data.zonalHeadName;
                $scope.businessHeadName = resp.data.businessHeadName;
                $scope.clusterManager = resp.data.clusterManager;
                $scope.creditManager = resp.data.creditManager;
                $scope.relationshipmgmt = resp.data.relationshipmgmt;
                $scope.customercode = resp.data.customercode;
                $scope.verticalCode = resp.data.verticalCode;
                $scope.contactperson = resp.data.contactperson;
                $scope.mobileno = resp.data.mobileno;
                $scope.addressline1 = resp.data.addressline1;
                $scope.addressline2 = resp.data.addressline2;
                customer_gid = resp.data.customer_gid;
                $scope.rdb_lsastatus = resp.data.lsa_status;
                $scope.maker_status = resp.data.maker_status;
                $scope.checker_status = resp.data.checker_status;
            });

            var url = "api/IdasTrnSanctionDoc/ScanDocSummary";
            var params = {
                sanction_gid: sanction_gid
            };
            SocketService.getparams(url, params).then(function (resp) {
                $scope.documentation_list = resp.data.MdlScannDocSummary;

            });

            var url = "api/IdasTrnSentMail/GetSentMailSummary";
            var params = {
                sanction_gid: sanction_gid
            };
            SocketService.getparams(url, params).then(function (resp) {
                $scope.senddata = resp.data.sendmail;

            });

            var url = 'api/IdasTrnSanctionDoc/GetCommonDoc';
            var params = {
                sanction_gid: sanction_gid
            };
            SocketService.getparams(url, params).then(function (resp) {

                $scope.commondocument = resp.data.uploaddocument;

            });
        }
        // document.getElementById('pagecount').onkeyup = function () {
        //     // console.log(document.getElementById('pagecount').value);
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };
        $scope.cancel = function () {

            $scope.show = true;

        }
        $scope.deletedocument = function (val) {
            var params = {
                commondocument_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to delete this Document ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, Delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/IdasTrnSanctionDoc/CommonDocDelete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            SweetAlert.swal('Document Deleted Successfully!');
                            unlockUI();
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });

                }

            });
        }

        $scope.FileShow = function () {
            if ($scope.DivFile == true) {
                $scope.DivFile = false;
            }
            else {
                $scope.DivFile = true;
            }
        }

        $scope.commondocumentupload = function (val, val1, name) {

            var frm = new FormData();

            // for (i = 0; i < val.length; i++) {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
            // }

            frm.append('document_name', $scope.documentname);
            frm.append('document_title', $scope.txtdocument_title);
            frm.append('sanction_gid', sanction_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/IdasTrnSanctionDoc/CommonDocUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    var url = 'api/IdasTrnSanctionDoc/GetCommonDoc';
                    var params = {
                        sanction_gid: sanction_gid
                    };
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.commondocument = resp.data.uploaddocument;

                    });
                    unlockUI();

                    $scope.txtdocument_title = '';
                    $("#commonupload").val('');
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

        //$scope.commondocumentupload = function (val, val1, name) {
        //    for (var i in $scope.documentname) {
        //    }
        //    var item = {
        //        name: val[0].name,
        //        file: val[0]
        //    };
        //    var frm = new FormData();
        //    frm.append('fileupload', item.file);
        //    frm.append('file_name', item.name);
        //    frm.append('document_name', $scope.documentname);
        //    frm.append('document_title', $scope.txtdocument_title);
        //    frm.append('sanction_gid', sanction_gid);
        //    frm.append('project_flag', "Default");
        //    $scope.uploadfrm = frm;
        //    var url = 'api/IdasTrnSanctionDoc/CommonDocUpload';
        //    lockUI();
        //    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

        //        $scope.txtdocument_title = '';
        //        $("#commonupload").val('');
        //        if (resp.data.status == true) {
        //            unlockUI();
        //            Notify.alert('Document Uploaded Successfully..!!', 'success')

        //            var url = 'api/IdasTrnSanctionDoc/GetCommonDoc';
        //            var params = {
        //                sanction_gid: sanction_gid
        //            };
        //            SocketService.getparams(url, params).then(function (resp) {

        //                $scope.commondocument = resp.data.uploaddocument;

        //            });
        //        }
        //        else {
        //            unlockUI();
        //            Notify.alert('File Format Not Supported!')

        //        }

        //    });

        //}


        $scope.mailconversation = function () {
            var modalInstance = $modal.open({
                templateUrl: '/mailconversation.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var url = "api/idasMstTemplate/MailContent";
                SocketService.get(url).then(function (resp) {
                    $scope.mailcontent = resp.data.template_content;

                });

                var url = "api/idasTrnMakerCheckerDtls/GetMailId";
                var params = {
                    customer_gid: customer_gid
                };
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rmmail_id = resp.data.rmmail_id;

                });

                var params = {
                    sanction_gid: sanction_gid,
                    type_of_copy: 'Scan Copy'
                }
                var url = 'api/IdasTrnSanctionDoc/ScanDocConExport';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.phyPath = resp.data.attachment_path;
                    }
                    else {
                        Notify.alert('Error in attachment...!', 'success')
                        activate();

                    }

                });
                $scope.sendMail = function () {
                    var params = {
                        sanction_gid: sanction_gid,
                        document_path: $scope.phyPath,
                        to_mail: $scope.rmmail_id,
                        cc_mail: $scope.cc_mail,
                        bcc_mail: $scope.bcc_mail,
                        body_content: $scope.mailcontent,
                        subject: $scope.subject_mail
                    }
                    var url = 'api/IdasTrnSentMail/PostSendMail';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            $scope.subject_mail = '';
                            $scope.bcc_mail = '';
                            $scope.cc_mail = '';
                            activate();
                        }
                        else {
                            unlockUI();
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                        }
                    });
                }
                $scope.export = function () {
                    var params = {
                        sanction_gid: sanction_gid,
                        type_of_copy: 'Scan Copy'
                    }
                    var url = 'api/IdasTrnSanctionDoc/ScanDocConExport';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            DownloaddocumentService.Downloaddocument(resp.data.attachment_cloudpath, resp.data.attachment_name);
                            // var phyPath = resp.data.attachment_path;
                            // var relPath = phyPath.split("EMS");
                            // var relpath1 = relPath[1].replace("\\", "/");
                            // var hosts = window.location.host;
                            // var prefix = location.protocol + "//";
                            // var str = prefix.concat(hosts, relpath1);
                            // var link = document.createElement("a");
                            // var name = resp.data.attachment_name.split('.');
                            // link.download = name[0];
                            // var uri = str;
                            // link.href = uri;
                            // link.click();

                        }
                        else {
                            Notify.alert(resp.data.message, 'success')
                            //  activate();

                        }

                    });
                }
            }
        }
        $scope.MkrVerify = function (sanctiondocument_gid) {
            var url = 'api/IdasTrnSanctionDoc/DocVerifyMkr';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.ShowMkrDocumentConfirmation = false;
                    $location.hash('DocumentVerifiedBy');
                    $anchorScroll();
                    activate();
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
                activate();
            });
        }
        $scope.ChkrVerify = function (sanctiondocument_gid) {
            var url = 'api/IdasTrnSanctionDoc/DocVerifyChkr';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.ShowChrDocumentConfirmation = false;
                    $location.hash('DocumentVerifiedBy');
                    $anchorScroll();
                    activate();
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
                activate();
            });
        }

        $scope.export = function () {
            var params = {
                sanction_gid: sanction_gid,
                type_of_copy: 'Scan Copy'
            }
            var url = 'api/IdasTrnSanctionDoc/ScanDocConExport';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.attachment_cloudpath, resp.data.attachment_name);
                    // var phyPath = resp.data.attachment_path;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.attachment_name.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();

                }
                else {
                    Notify.alert(resp.data.message, 'success')
                    activate();

                }

            });
        }
        $scope.exportattach = function (path, attchment_name) {

            var phyPath = path;
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = attchment_name.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();


        }
        $scope.downloadsdocument = function (val1, val2) {

            //var phyPath = val1;

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
        $scope.openpanel = function () {

            $scope.show = false;
            $scope.options = true;
        }
        $scope.uploadallocation1 = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('sanction_gid', sanction_gid);
            frm.append('types_of_doc', 'Scan Copy');
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            var url = 'api/IdasTrnSanctionDoc/ConversationUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {


                $("#addupload").val('');
                $("#editupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                    var url = 'api/IdasTrnSanctionDoc/GetConverseDoc';

                    SocketService.get(url).then(function (resp) {

                        $scope.uploaddocument = resp.data.uploaddocument;

                    });
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')

                }

            });

        }
        $scope.gotoback = function () {
            $state.go('app.idasTrnMakerSummary');
        }
        $scope.gotobackChkr = function () {
            $state.go('app.idasTrnCheckerSummary');
        }

        $scope.docConMkr = function (sanctiondocument_gid, conversation_count) {


            var url = "api/idasTrnMakerCheckerDtls/RmResponseCadViwed";
            var params = {
                sanctiondocument_gid: sanctiondocument_gid,
                types_of_conversation: 'Internal'
            };
            SocketService.post(url, params).then(function (resp) {

            });
            var url = "api/idasTrnMakerCheckerDtls/RmResponseCadViwed";
            var params = {
                sanctiondocument_gid: sanctiondocument_gid,
                types_of_conversation: 'External'
            };
            SocketService.post(url, params).then(function (resp) {

            });


            $scope.docconClose = function () {

                $scope.ShowMkrDocumentConfirmation = false;
                $location.hash('DocumentVerifiedBy');
                $anchorScroll();
            }

            $scope.ShowMkrDocumentConfirmation = true;

            $location.hash('DocumentConfirmationTab');
            $anchorScroll();

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1,

            };

            $scope.DivFile = false;
            $scope.IsVisible = false;
            $scope.Visible = true;
            $scope.valueExternal = false;
            $scope.valueInternal = false;

            if (conversation_count == '0') {
                $scope.showraisequery = false;
                $scope.shownoquery = true;
            }
            else {
                $scope.showraisequery = true;
                $scope.shownoquery = false;
            }

            var url = 'api/IdasTrnSanctionDoc/ScanDocConversationInternal';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.docconversationlistInternal = resp.data.MdlDocConversation;

                    $scope.valueInternal = true;
                } else {
                    $scope.valueInternal = false;

                }
            });

            var url = 'api/IdasTrnSanctionDoc/ScanDocConversationExternal';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.docconversationlistExternal = resp.data.MdlDocConversation;
                    $scope.valueExternal = true;
                } else {
                    $scope.valueExternal = false;

                }
            });

            $scope.typeofcopy = 'Scan Copy';
            var url = 'api/IdasTrnSanctionDoc/GetDocDetailsView';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();

                $scope.sanction_gid = resp.data.sanction_gid;
                $scope.document_gid = resp.data.document_gid;
                $scope.document_code = resp.data.document_code;
                $scope.document_name = resp.data.document_name;
                $scope.document_date = resp.data.scandocument_date;
                $scope.documentrecord_id = resp.data.documentrecord_id;
                $scope.scanfinal_remarks = resp.data.scanfinal_remarks;
                $scope.maker_status = resp.data.maker_status;
                $scope.checker_status = resp.data.checker_status;
                $scope.types_of_copy = resp.data.types_of_copy;
                $scope.txtfinalremarks = resp.data.finalremarks;

                if (resp.data.finalremarks == 'Others') {
                    $scope.other_remarks = true;
                } else {
                    $scope.other_remarks = false;
                }
            });
            var url = 'api/IdasTrnSanctionDoc/GetDocComments';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.doc_comments = resp.data.doc_comments;

            });

            $scope.onchangeremarks = function (txtfinalremarks) {
                if (txtfinalremarks == 'Others') {
                    $scope.other_remarks = true;
                    $scope.scanfinal_remarks = '';
                } else {
                    $scope.other_remarks = false;
                    $scope.scanfinal_remarks = '';
                }
            }

            $scope.ShowRaiseQuery = function () {
                if ($scope.showraisequery == false) {
                    $scope.showraisequery = true;
                }
                else {
                    $scope.showraisequery = false;
                }

            }

            $scope.raiseNoQuery = function () {
                var params = {
                    sanctiondocument_gid: sanctiondocument_gid,
                    sanction_gid: $scope.sanction_gid,
                    document_gid: $scope.document_gid,
                    cad_query: 'No Query',
                    document_name: $scope.document_name,
                    type_of_conversation: 'Internal',
                    noquery_flag: 'Y',
                }

                SweetAlert.swal({
                    title: 'Are you sure?',
                    text: 'Do you want to send "No Query"?',

                    showCancelButton: true,
                    confirmButtonColor: '#DD6B55',
                    confirmButtonText: 'Yes, Send it!',
                    closeOnConfirm: false
                }, function (isConfirm) {
                    if (isConfirm) {
                        lockUI();
                        var url = 'api/IdasTrnSanctionDoc/RaiseConversation';
                        SocketService.post(url, params).then(function (resp) {

                            if (resp.data.status == true) {
                                SweetAlert.swal('Query Sent Successfully!');
                                unlockUI();
                                conversation_count = "1";
                                
                                $scope.shownoquery = false;
                                $scope.showraisequery = true;
                                var url = 'api/IdasTrnSanctionDoc/ScanDocConversationInternal';
                                var params = {
                                    sanctiondocument_gid: sanctiondocument_gid
                                };
                                lockUI();
                                SocketService.getparams(url, params).then(function (resp) {
                                    unlockUI();
                                    if (resp.data.status == true) {
                                        $scope.docconversationlistInternal = resp.data.MdlDocConversation;

                                        $scope.valueInternal = true;
                                    } else {
                                        $scope.valueInternal = false;

                                    }
                                });
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                            }
                        });

                    }
                });
            }
            $scope.onchangecopy = function (types_of_copy) {
                var params = {
                    sanctiondocument_gid: sanctiondocument_gid,
                    type_copy: $scope.types_of_copy
                }
                var url = "api/IdasTrnDocConversation/PostTypeOfCopy";
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message)
                    }

                });
            }
            $scope.MkrVerify = function () {
                var url = 'api/IdasTrnSanctionDoc/DocumentConfirmation';
                var params = {
                    sanctiondocument_gid: sanctiondocument_gid,
                    confirmation_type: 'Maker'
                };

                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        $scope.ShowMkrDocumentConfirmation = false;
                        $location.hash('DocumentVerifiedBy');
                        $anchorScroll();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }

                });
            }
            $scope.btnShow = function (id, reply) {
                $scope.IsVisible = true;
                $scope.Visible = false;

            }
            $scope.btnHide = function () {
                $scope.IsVisible = false;
                $scope.Visible = true;
            }
            $scope.PopupDownload = function (docconversation_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/mailconversationdownload.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                    $scope.downloads = function (val1, val2) {

                        //var phyPath = val1;

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

                    var url = "api/IdasTrnDocConversation/GetUploadDoc";
                    var params = {
                        docconversation_gid: docconversation_gid
                    };
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.viewDocumentList = resp.data.uploaddocument;

                    });
                }
            }

            $scope.raiseQueryRM = function () {

                var params = {
                    sanctiondocument_gid: sanctiondocument_gid,
                    sanction_gid: $scope.sanction_gid,
                    document_gid: $scope.document_gid,
                    cad_query: $scope.content,
                    document_name: $scope.document_name,
                    document_title: $scope.txtdocument_title,
                    type_of_conversation: 'External'
                }

                var url = 'api/IdasTrnSanctionDoc/RaiseConversation';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Conversation Added Successfully..!!', 'success')
                        $scope.content = '';
                        var url = 'api/IdasTrnSanctionDoc/ScanDocConversationExternal';
                        var params = {
                            sanctiondocument_gid: sanctiondocument_gid
                        };
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                $scope.docconversationlistExternal = resp.data.MdlDocConversation;
                                $scope.valueExternal = true;
                            } else {
                                $scope.valueExternal = false;

                            }
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message)
                    }
                });


            }
            $scope.raiseQueryChecker = function () {

                var params = {
                    sanctiondocument_gid: sanctiondocument_gid,
                    sanction_gid: $scope.sanction_gid,
                    document_gid: $scope.document_gid,
                    cad_query: $scope.content,
                    document_name: $scope.document_name,
                    document_title: $scope.txtdocument_title,
                    type_of_conversation: 'Internal'
                }

                var url = 'api/IdasTrnSanctionDoc/RaiseConversation';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Conversation Added Successfully..!!', 'success')
                        $scope.content = '';
                        var url = 'api/IdasTrnSanctionDoc/ScanDocConversationInternal';
                        var params = {
                            sanctiondocument_gid: sanctiondocument_gid
                        };
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                $scope.docconversationlistInternal = resp.data.MdlDocConversation;

                                $scope.valueInternal = true;
                                $scope.shownoquery = false;
                            } else {
                                $scope.valueInternal = false;

                            }
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message)
                    }
                });


            }
            $scope.FileShow = function () {
                if ($scope.DivFile == true) {
                    $scope.DivFile = false;
                }
                else {
                    $scope.DivFile = true;
                }
            }
            $scope.uploadallocation = function (val, val1, name) {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

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
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/IdasTrnSanctionDoc/ConversationDocUpload';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $("#addupload").val('');
                    $scope.txtdocument_title = '';
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Document Uploaded Successfully..!!', 'success')

                        var url = 'api/IdasTrnSanctionDoc/GetConverseDoc';

                        SocketService.get(url).then(function (resp) {

                            $scope.uploaddocument = resp.data.uploaddocument;

                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('File Format Not Supported!')

                    }

                });

            }

            $scope.update = function () {

                var params = {
                    sanctiondocument_gid: sanctiondocument_gid,
                    document_date: $scope.document_date
                }

                var url = 'api/IdasTrnSanctionDoc/PostScanDocDate';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert(resp.data.message, 'success')
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
            $scope.forwardedRaiseQuery = function (query, ref_no, response) {
                var lssendmsg;

                if (response == 'Query Confirmed.') {
                    lssendmsg = query;
                }
                else {
                    lssendmsg = response;
                }

                var params = {
                    sanctiondocument_gid: sanctiondocument_gid,
                    sanction_gid: $scope.sanction_gid,
                    document_gid: $scope.document_gid,
                    cad_query: lssendmsg,
                    document_name: $scope.document_name,
                    type_of_conversation: 'External',
                    reference_query: ref_no
                }
                SweetAlert.swal({
                    title: 'Are you sure?',
                    text: 'Do you want to send this query to RM ?',

                    showCancelButton: true,
                    confirmButtonColor: '#DD6B55',
                    confirmButtonText: 'Yes, Send it!',
                    closeOnConfirm: false
                }, function (isConfirm) {
                    if (isConfirm) {
                        lockUI();
                        var url = 'api/IdasTrnSanctionDoc/RaiseConversation';
                        SocketService.post(url, params).then(function (resp) {

                            if (resp.data.status == true) {
                                SweetAlert.swal('Query Sent Successfully!');
                                unlockUI();
                                var url = 'api/IdasTrnSanctionDoc/ScanDocConversationInternal';
                                var params = {
                                    sanctiondocument_gid: sanctiondocument_gid
                                };
                                lockUI();
                                SocketService.getparams(url, params).then(function (resp) {
                                    unlockUI();
                                    if (resp.data.status == true) {
                                        $scope.docconversationlistInternal = resp.data.MdlDocConversation;

                                        $scope.valueInternal = true;
                                    } else {
                                        $scope.valueInternal = false;

                                    }
                                });
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                            }
                        });

                    }

                });
            }
            $scope.btncopy = function (query, response) {
                if (response == 'Query Confirmed.') {
                    $scope.content = query;

                }
                else {
                    $scope.content = response;
                }

                $location.hash('down');
                $anchorScroll();

            }
            $scope.updateFinalRemarks = function () {
                if ($scope.txtfinalremarks == 'Others' && ($scope.scanfinal_remarks == '' || $scope.scanfinal_remarks == undefined || $scope.scanfinal_remarks == null)) {
                    Notify.alert('Kindly Enter Remarks', 'warning')
                } else {
                    var params = {
                        sanctiondocument_gid: sanctiondocument_gid,
                        scanfinal_remarks: $scope.scanfinal_remarks,
                        finalremarks: $scope.txtfinalremarks
                    }

                    var url = 'api/IdasTrnSanctionDoc/DocScanFinalRemarks';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, 'success')
                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, 'warning'),
                            $scope.txtfinalremarks = '',
                            $scope.scanfinal_remarks = ''
                        }
                    });
                }
            }
            $scope.UploadDocCancel = function (conversationdocument_gid) {
                var params = {
                    conversationdocument_gid: conversationdocument_gid
                }
                var url = 'api/IdasTrnSanctionDoc/deleteconversedoc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Document deleted Successfully..!!', 'success')

                        var url = 'api/IdasTrnSanctionDoc/GetConverseDoc';

                        SocketService.get(url).then(function (resp) {

                            $scope.uploaddocument = resp.data.uploaddocument;

                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred')

                    }

                });
            }

            $scope.raiseResponse = function (id, count, textArea) {
                var params = {
                    docconversation_gid: id,
                    rm_response: textArea
                }

                var url = 'api/IdasTrnSanctionDoc/DocRmResponse';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Conversation Added Successfully..!!', 'success')
                        $scope.content = " ";
                        var url = 'api/IdasTrnSanctionDoc/ScanDocConversationExternal';
                        var params = {
                            sanctiondocument_gid: sanctiondocument_gid
                        };
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                $scope.docconversationlistExternal = resp.data.MdlDocConversation;
                                $scope.valueExternal = true;
                            } else {
                                $scope.valueExternal = false;

                            }
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred!')
                    }
                    activate();
                });
            }
        }

        $scope.docConChkr = function (sanctiondocument_gid, conversation_count) {

            var url = "api/idasTrnMakerCheckerDtls/CadQuieryRMViwed";
            var params = {
                sanctiondocument_gid: sanctiondocument_gid,
                types_of_conversation: 'Internal'
            };
            SocketService.post(url, params).then(function (resp) {


            });

            var url = "api/idasTrnMakerCheckerDtls/RmResponseCadViwed";
            var params = {
                sanctiondocument_gid: sanctiondocument_gid,
                types_of_conversation: 'External'
            };
            SocketService.post(url, params).then(function (resp) {

            });

            //localStorage.setItem('sanctiondocument_gid', sanctiondocument_gid);
            //localStorage.setItem('conversation_count', conversation_count);
            //$state.go('app.idasTrnDocConversationChkr');

            $scope.docconChrClose = function () {

                $scope.ShowChrDocumentConfirmation = false;
                $location.hash('DocumentVerifiedBy');
                $anchorScroll();
            }

            $scope.ShowChrDocumentConfirmation = true;

            $location.hash('ChrDocumentConfirmationTab');
            $anchorScroll();

            $scope.DivFile = false;
            $scope.IsVisible = false;
            $scope.Visible = true;
            $scope.valueExternal = false;
            $scope.valueInternal = false;

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            var url = 'api/IdasTrnSanctionDoc/ScanDocConversationInternal';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.docconversationlistInternal = resp.data.MdlDocConversation;
                    $scope.valueInternal = true;
                } else {
                    $scope.valueInternal = false;

                }
            });
            var url = 'api/IdasTrnSanctionDoc/ScanDocConversationExternal';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.docconversationlistExternal = resp.data.MdlDocConversation;
                    $scope.valueExternal = true;
                } else {
                    $scope.valueExternal = false;
                }
            });
            $scope.typeofcopy = 'Scan Copy';
            var url = 'api/IdasTrnSanctionDoc/GetDocDetailsView';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.sanction_gid = resp.data.sanction_gid;
                $scope.document_gid = resp.data.document_gid;
                $scope.document_code = resp.data.document_code;
                $scope.document_name = resp.data.document_name;
                $scope.document_date = resp.data.scandocument_date;
                $scope.documentrecord_id = resp.data.documentrecord_id;
                $scope.scanfinal_remarks = resp.data.scanfinal_remarks;
                $scope.maker_status = resp.data.maker_status;
                $scope.types_of_copy = resp.data.types_of_copy;

                $scope.checker_status = resp.data.checker_status;

                $scope.txtfinalremarks = resp.data.finalremarks;

                if (resp.data.finalremarks == 'Others') {
                    $scope.other_remarks = true;
                } else {
                    $scope.other_remarks = false;
                }
            });

            var url = 'api/IdasTrnSanctionDoc/GetDocComments';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.doc_comments = resp.data.doc_comments;

            });

            // Document Date Updation
            $scope.update = function () {
                var params = {
                    sanctiondocument_gid: sanctiondocument_gid,
                    document_date: $scope.document_date
                }

                var url = 'api/IdasTrnSanctionDoc/PostScanDocDate';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert(resp.data.message, 'success')
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
            // Type of Document Updation
            $scope.onchangecopy = function (types_of_copy) {
                var params = {
                    sanctiondocument_gid: sanctiondocument_gid,
                    type_copy: $scope.types_of_copy
                }
                var url = "api/IdasTrnDocConversation/PostTypeOfCopy";
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }

                });
            }

            $scope.raiseResponse = function (id, textArea) {
                var params = {
                    docconversation_gid: id,
                    rm_response: textArea
                }

                var url = 'api/IdasTrnSanctionDoc/DocRmResponse';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Conversation Added Successfully..!!', 'success')
                        $scope.content = " ";
                        var url = 'api/IdasTrnSanctionDoc/ScanDocConversationExternal';
                        var params = {
                            sanctiondocument_gid: sanctiondocument_gid
                        };
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                $scope.docconversationlistExternal = resp.data.MdlDocConversation;
                                $scope.valueExternal = true;
                            } else {
                                $scope.valueExternal = false;

                            }
                        });
                        var url = 'api/IdasTrnSanctionDoc/ScanDocConversationInternal';
                        var params = {
                            sanctiondocument_gid: sanctiondocument_gid
                        };
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                $scope.docconversationlistInternal = resp.data.MdlDocConversation;
                                $scope.valueInternal = true;
                            } else {
                                $scope.valueInternal = false;

                            }
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred!')
                    }
                });
            }
            $scope.btncopy = function (data) {

                $scope.content = data;
                $location.hash('down');
                $anchorScroll();

            }

            $scope.confirmQuery = function (val, noquery_flag) {

                SweetAlert.swal({
                    title: 'Are you sure?',
                    text: 'Do you want to confirm this query to checker ?',

                    showCancelButton: true,
                    confirmButtonColor: '#DD6B55',
                    confirmButtonText: 'Yes, Confirmed it!',
                    closeOnConfirm: false
                }, function (isConfirm) {
                    if (isConfirm) {
                        lockUI();
                        var params = {
                            docconversation_gid: val,
                            rm_response: 'Query Confirmed.'
                        }
                        if (noquery_flag == 'Y') {
                            var url = 'api/IdasTrnSanctionDoc/DocNoQueryRmResponse';
                            SocketService.getparams(url, params).then(function (resp) {

                                if (resp.data.status == true) {
                                    SweetAlert.swal('Document Confirmed Successfully!');
                                    unlockUI();
                                    var url = 'api/IdasTrnSanctionDoc/ScanDocConversationInternal';
                                    var params = {
                                        sanctiondocument_gid: sanctiondocument_gid
                                    };
                                    lockUI();
                                    SocketService.getparams(url, params).then(function (resp) {
                                        unlockUI();
                                        if (resp.data.status == true) {
                                            $scope.docconversationlistInternal = resp.data.MdlDocConversation;
                                            $scope.valueInternal = true;
                                        } else {
                                            $scope.valueInternal = false;

                                        }
                                    });
                                    activate();
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    unlockUI();
                                }
                            });
                        }
                        else {
                            var url = "api/IdasTrnSanctionDoc/DocRmResponse";
                            SocketService.post(url, params).then(function (resp) {

                                if (resp.data.status == true) {
                                    SweetAlert.swal('Query Confirmed Successfully!');
                                    unlockUI();
                                    var url = 'api/IdasTrnSanctionDoc/ScanDocConversationInternal';
                                    var params = {
                                        sanctiondocument_gid: sanctiondocument_gid
                                    };
                                    lockUI();
                                    SocketService.getparams(url, params).then(function (resp) {
                                        unlockUI();
                                        if (resp.data.status == true) {
                                            $scope.docconversationlistInternal = resp.data.MdlDocConversation;
                                            $scope.valueInternal = true;
                                        } else {
                                            $scope.valueInternal = false;

                                        }
                                    });
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    unlockUI();
                                }
                            });
                        }



                    }

                });
            }

            $scope.forwardQuery = function (val) {
                var params = {
                    docconversation_gid: val
                }
                SweetAlert.swal({
                    title: 'Are you sure?',
                    text: 'Do you want to forward this query to RM ?',

                    showCancelButton: true,
                    confirmButtonColor: '#DD6B55',
                    confirmButtonText: 'Yes, Forward it!',
                    closeOnConfirm: false
                }, function (isConfirm) {
                    if (isConfirm) {
                        lockUI();
                        var url = "api/idasTrnMakerCheckerDtls/PostForwardedQuery";
                        SocketService.getparams(url, params).then(function (resp) {

                            if (resp.data.status == true) {
                                SweetAlert.swal('Forwarded Successfully!');
                                unlockUI();
                                var url = 'api/IdasTrnSanctionDoc/ScanDocConversationInternal';
                                var params = {
                                    sanctiondocument_gid: sanctiondocument_gid
                                };
                                lockUI();
                                SocketService.getparams(url, params).then(function (resp) {
                                    unlockUI();
                                    if (resp.data.status == true) {
                                        $scope.docconversationlistInternal = resp.data.MdlDocConversation;
                                        $scope.valueInternal = true;
                                    } else {
                                        $scope.valueInternal = false;

                                    }
                                });
                                var url = 'api/IdasTrnSanctionDoc/ScanDocConversationExternal';
                                lockUI();
                                SocketService.getparams(url, params).then(function (resp) {
                                    unlockUI();
                                    if (resp.data.status == true) {
                                        $scope.docconversationlistExternal = resp.data.MdlDocConversation;
                                        $scope.valueExternal = true;
                                    } else {
                                        $scope.valueExternal = false;
                                    }
                                });
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                            }
                        });

                    }

                });
            }

            $scope.UploadDocCancel = function (conversationdocument_gid) {
                var params = {
                    conversationdocument_gid: conversationdocument_gid
                }
                var url = 'api/IdasTrnSanctionDoc/deleteconversedoc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Document deleted Successfully..!!', 'success')

                        var url = 'api/IdasTrnSanctionDoc/GetConverseDoc';

                        SocketService.get(url).then(function (resp) {

                            $scope.uploaddocument = resp.data.uploaddocument;

                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred')

                    }

                });
            }
            $scope.ChkrVerify = function () {
                var url = 'api/IdasTrnSanctionDoc/DocumentConfirmation';
                var params = {
                    sanctiondocument_gid: sanctiondocument_gid,
                    confirmation_type: 'Checker'
                };

                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();

                        localStorage.setItem('conversation_count', '1');
                        $scope.ShowChrDocumentConfirmation = false;
                        $location.hash('DocumentVerifiedBy');
                        $anchorScroll();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    //  activate();
                });
                activate();
            }

            $scope.raiseQuery = function () {

                var params = {
                    sanctiondocument_gid: sanctiondocument_gid,
                    sanction_gid: $scope.sanction_gid,
                    document_gid: $scope.document_gid,
                    cad_query: $scope.content,
                    document_name: $scope.document_name,
                    document_title: $scope.txtdocument_title,
                    type_of_conversation: 'External'
                }

                var url = 'api/IdasTrnSanctionDoc/RaiseConversation';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Conversation Added Successfully..!!', 'success')
                        $scope.content = '';
                        var url = 'api/IdasTrnSanctionDoc/ScanDocConversationExternal';
                        var params = {
                            sanctiondocument_gid: sanctiondocument_gid
                        };
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                $scope.docconversationlistExternal = resp.data.MdlDocConversation;
                                $scope.valueExternal = true;
                            } else {
                                $scope.valueExternal = false;

                            }
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message)
                    }
                });


            }
            $scope.FileShow = function () {
                if ($scope.DivFile == true) {
                    $scope.DivFile = false;
                }
                else {
                    $scope.DivFile = true;
                }
            }
            $scope.uploadallocation = function (val, val1, name) {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

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
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/IdasTrnSanctionDoc/ConversationDocUpload';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $("#addupload").val('');
                    $scope.txtdocument_title = '';
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Document Uploaded Successfully..!!', 'success')

                        var url = 'api/IdasTrnSanctionDoc/GetConverseDoc';

                        SocketService.get(url).then(function (resp) {

                            $scope.uploaddocument = resp.data.uploaddocument;

                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('File Format Not Supported!')

                    }

                });

            }

            $scope.onchangeremarks = function (txtfinalremarks) {
                if (txtfinalremarks == 'Others') {
                    $scope.other_remarks = true;
                } else {
                    $scope.other_remarks = false;
                }
            }


            $scope.btncopyinternal = function (index, value) {
                $scope.docconversationlistInternal[index].reply = value;
            }

            $scope.btnShow = function (id, reply) {
                $scope.IsVisible = true;
                $scope.Visible = false;

            }
            $scope.btnHide = function () {
                $scope.IsVisible = false;
                $scope.Visible = true;
            }
            $scope.PopupDownload = function (docconversation_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/mailconversation.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                    $scope.downloads = function (val1, val2) {

                        //var phyPath = val1;
                        //console.log(phyPath);
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

                    var url = "api/IdasTrnDocConversation/GetUploadDoc";
                    var params = {
                        docconversation_gid: docconversation_gid
                    };
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.viewDocumentList = resp.data.uploaddocument;

                    });

                    var url = 'api/IdasTrnSanctionDoc/GetConverseDoc';

                    SocketService.get(url).then(function (resp) {

                        $scope.uploaddocument = resp.data.uploaddocument;

                    });
                }
            }
            // Final Remarks Updation
            $scope.updateFinalRemarks = function () {
                if ($scope.txtfinalremarks == 'Others' && ($scope.scanfinal_remarks == '' || $scope.scanfinal_remarks == undefined || $scope.scanfinal_remarks == null)) {
                    Notify.alert('Kindly Enter Remarks', 'warning')
                }
                else {
                    var params = {
                        sanctiondocument_gid: sanctiondocument_gid,
                        scanfinal_remarks: $scope.scanfinal_remarks,
                        finalremarks: $scope.txtfinalremarks
                    }

                    var url = 'api/IdasTrnSanctionDoc/DocScanFinalRemarks';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, 'success')
                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, 'warning'),
                             $scope.txtfinalremarks = '',
                             $scope.scanfinal_remarks = ''
                        }

                    });
                }
            }
        }

        $scope.onselectedlsastatus = function (rdb_lsastatus) {
            var params = {
                sanction_gid: sanction_gid,
                lsa_status: $scope.rdb_lsastatus
            }
            var url = "api/IdasTrnPhyDoc/PostLSAStatus";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    activate();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    activate();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }

        $scope.checkallDocument = function (selected) {
            angular.forEach($scope.documentation_list, function (val) {
                val.checked = selected;
            });
        }

        $scope.DocumentConfirm = function () {
            var documentation_list = [];
            var sanctiondocument_gid;
            angular.forEach($scope.documentation_list, function (val) {

                if (val.checked == true) {
                    sanctiondocument_gid = val.sanctiondocument_gid;
                    documentation_list.push(sanctiondocument_gid);
                }
            });
            if (sanctiondocument_gid == undefined) {
                Notify.alert('Select Atleast One Record!', 'warning')
            }
            else {
                var modalInstance = $modal.open({
                    templateUrl: '/DocumentConfirmationModalContent.html',
                    controller: ModalInstanceCtrl,
                    size: 'md',
                    backdrop: 'static',
                    keyboard: false
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    $scope.futuredatecheck = function (val) {
                        var params = {
                            date: val.toDateString()
                        }
                        var url = 'api/IdasTrnPhyDoc/FutureDateCheck';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == false) {
                                $scope.scandocument_date = '';
                                $modalInstance.close('closed');
                                Notify.alert(resp.data.message, 'warning')
                            }
                        });
                    }

                    $scope.onchangeremarks = function (txtfinalremarks) {
                        if (txtfinalremarks == 'Others') {
                            $scope.other_remarks = true;
                            $scope.scanfinal_remarks = '';
                        } else {
                            $scope.other_remarks = false;
                            $scope.scanfinal_remarks = '';
                        }
                    }

                    $scope.documentconfirmationSubmitMkr = function () {
                        if ($scope.txtfinalremarks == 'Others' && ($scope.scanfinal_remarks == '' || $scope.scanfinal_remarks == undefined || $scope.scanfinal_remarks == null)) {
                            $modalInstance.close('closed');
                            Notify.alert('Kindly Enter Remarks', 'warning')
                        }
                        else {
                            var params = {
                                sanctiondocument_gid: documentation_list,
                                type_copy: $scope.types_of_copy,
                                document_date: $scope.scandocument_date,
                                finalremarks: $scope.txtfinalremarks,
                                scanfinal_remarks: $scope.scanfinal_remarks,
                                confirmation_type: 'Maker'
                            }
                            if (sanctiondocument_gid != undefined) {
                                var url = 'api/IdasTrnSanctionDoc/MkrChrBulkDocVerification';
                                lockUI()
                                SocketService.post(url, params).then(function (resp) {
                                    if (resp.data.status == true) {

                                        Notify.alert(resp.data.message, {
                                            status: 'success',
                                            pos: 'top-center',
                                            timeout: 3000
                                        });
                                        $modalInstance.close('closed');
                                        unlockUI();
                                        activate();
                                        $scope.checkallDocument = '';
                                    }
                                    else {
                                        unlockUI();
                                        Notify.alert(resp.data.message, {
                                            status: 'warning',
                                            pos: 'top-center',
                                            timeout: 3000
                                        });
                                        $modalInstance.close('closed');
                                        unlockUI();
                                    }
                                });
                            }
                            else {
                                $modalInstance.close('closed');
                                Notify.alert('Select Atleast One Record!', 'warning')
                            }
                        }
                    }

                    $scope.documentconfirmationSubmitChkr = function () {
                        if ($scope.txtfinalremarks == 'Others' && ($scope.scanfinal_remarks == '' || $scope.scanfinal_remarks == undefined || $scope.scanfinal_remarks == null)) {
                            $modalInstance.close('closed');
                            Notify.alert('Kindly Enter Remarks', 'warning')
                        }
                        else {
                            var params = {
                                sanctiondocument_gid: documentation_list,
                                type_copy: $scope.types_of_copy,
                                document_date: $scope.scandocument_date,
                                finalremarks: $scope.txtfinalremarks,
                                scanfinal_remarks: $scope.scanfinal_remarks,
                                confirmation_type: 'Checker'
                            }
                            if (sanctiondocument_gid != undefined) {
                                var url = 'api/IdasTrnSanctionDoc/MkrChrBulkDocVerification';
                                lockUI()
                                SocketService.post(url, params).then(function (resp) {
                                    if (resp.data.status == true) {

                                        Notify.alert(resp.data.message, {
                                            status: 'success',
                                            pos: 'top-center',
                                            timeout: 3000
                                        });
                                        $modalInstance.close('closed');
                                        unlockUI();
                                        activate();
                                        $scope.checkallDocument = '';
                                    }
                                    else {
                                        unlockUI();
                                        Notify.alert(resp.data.message, {
                                            status: 'warning',
                                            pos: 'top-center',
                                            timeout: 3000
                                        });
                                        $modalInstance.close('closed');
                                        unlockUI();
                                    }
                                });
                            }
                            else {
                                $modalInstance.close('closed');
                                Notify.alert('Select Atleast One Record!', 'warning')
                            }
                        }
                    }
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                }
            }
        }
    }
})();
