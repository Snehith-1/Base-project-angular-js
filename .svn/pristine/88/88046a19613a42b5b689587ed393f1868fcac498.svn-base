(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnResponseDoc', idasTrnResponseDoc);

    idasTrnResponseDoc.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$anchorScroll', 'DownloaddocumentService','cmnfunctionService'];

    function idasTrnResponseDoc($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $anchorScroll, DownloaddocumentService,cmnfunctionService) {
        $scope.title = 'idasTrnResponseDoc';
        var sanction_gid;
        var customer_gid;
        activate();

        function activate() {
            $scope.DivFile = false;
            $scope.show = true;
            $scope.options = false;

            $location.hash('down');
            $anchorScroll();
            

            sanction_gid = localStorage.getItem('sanction_gid');
            var url = 'api/IdasTrnSanctionDoc/SanctionDtlsView';
            var params = {
                sanction_gid: sanction_gid
            };

            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionrefno = resp.data.sanctionrefno;
                $scope.SanctionDate = resp.data.SanctionDate;
                $scope.SanctionAmount= resp.data.SanctionAmount;
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

            });

            var url = "api/IdasTrnSanctionDoc/ScanDocSummary";
            var params = {
                sanction_gid: sanction_gid
            };
            SocketService.getparams(url, params).then(function (resp) {
                $scope.documentation_list = resp.data.MdlScannDocSummary;
                console.log(resp.data);
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

        $scope.deletedocument = function (val) {
            var params = {
                commondocument_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to delete this query to Document ?',

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
                                status: 'danger',
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
            frm.append('sanction_gid', sanction_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            var url = 'api/IdasTrnSanctionDoc/CommonDocUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                $("#commonupload").val('');
                $scope.txtdocument_title = '';
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                    var url = 'api/IdasTrnSanctionDoc/GetCommonDoc';
                    var params = {
                        sanction_gid: sanction_gid
                    };
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.commondocument = resp.data.uploaddocument;

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
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();

            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.gotoback = function () {
            $state.go('app.idasTrnRmResponseSummary');
        }
        $scope.cancel = function () {

            $scope.show = true;

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
            frm.append('sanction_gid', sanction_gid);
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

                var url = "api/idasTrnMakerCheckerDtls/GetCreditMailId";
                SocketService.get(url).then(function (resp) {
                    $scope.creditmail_id = resp.data.creditmail_id;

                });

                var params = {
                    sanction_gid: sanction_gid,
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
                        sanction_gid: sanction_gid
                    }
                    var url = 'api/IdasTrnSanctionDoc/ScanDocConExport';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            DownloaddocumentService.Downloaddocument(resp.data.attachment_cloudpath,resp.data.attachment_name);
                            // var phyPath = resp.data.attachment_cloudpath;
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
            }
        }

        $scope.export = function () {
            var params = {
                sanction_gid: sanction_gid
            }
            var url = 'api/IdasTrnSanctionDoc/ScanDocConExport';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.attachment_cloudpath,resp.data.attachment_name);
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
        $scope.openpanel = function () {

            $scope.show = false;
            $scope.options = true;
        }
        $scope.rmresponse = function (sanctiondocument_gid)
        {
            var url = "api/idasTrnMakerCheckerDtls/CadQuieryRMViwed";
            var params = {
                sanctiondocument_gid: sanctiondocument_gid,
                types_of_conversation: 'External'
            };
            SocketService.post(url, params).then(function (resp) {
                $scope.senddata = resp.data.sendmail;

            });
            localStorage.setItem('sanctiondocument_gid', sanctiondocument_gid);
            $state.go('app.idasTrnRmResponse');
        }
        $scope.cancel = function () {

            $scope.show = true;

        }
       
    }
})();
