(function () {
    'use strict';

    angular
        .module('angle')
        .controller('IdasTrnPhyDocVerify', IdasTrnPhyDocVerify);

    IdasTrnPhyDocVerify.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$anchorScroll', 'DownloaddocumentService','cmnfunctionService'];

    function IdasTrnPhyDocVerify($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $anchorScroll, DownloaddocumentService,cmnfunctionService) {
        $scope.title = 'IdasTrnPhyDocVerify';
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
                $scope.batch_status = resp.data.batch_status;
                $scope.status_ofBAL=resp.data.status_ofBAL;
                $scope.lsa_status = resp.data.lsa_status;
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

            var url = "api/IdasTrnPhyDoc/GetPhyUnVerifiedCount";
            var params = {
                sanction_gid: sanction_gid
            };
            SocketService.getparams(url, params).then(function (resp) {
                $scope.phydocunverified_count = resp.data.phydocunverified_count;
              
            });


            var url = 'api/IdasTrnSanctionDoc/GetCommonDoc';
            var params = {
                sanction_gid: sanction_gid
            };
            SocketService.getparams(url, params).then(function (resp) {

                $scope.commondocument = resp.data.uploaddocument;

            });

        }

        $scope.checkallDocument = function (selected) {
            angular.forEach($scope.documentation_list, function (val) {
                val.checked = selected;
            });
        }
        
        $scope.DocumentVerify = function () {
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
                    templateUrl: '/DocumentVerificationModalContent.html',
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
                                $scope.phydocument_date = '';
                                $modalInstance.close('closed');
                                Notify.alert(resp.data.message, 'warning')
                            }
                        });
                    }

                    $scope.documentverificationSubmit = function () {

                        var params = {
                            sanctiondocument_gid: documentation_list,
                            type_copy: $scope.types_of_copy,
                            document_date: $scope.phydocument_date,
                            phyfinal_remarks: $scope.remarks,
                        }

                        if (sanctiondocument_gid != undefined) {
                            var url = 'api/IdasTrnPhyDoc/PostBulkDocVerification';
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
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                }
            }
        }

        $scope.exportattach = function (path, attchment_name) {
            var phyPath = path;
            var relPath = phyPath.split("EMS/");
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
        $scope.BatchConfirm = function () {
            if( $scope.status_ofBAL=='Yes'){
                var modalInstance = $modal.open({
                    templateUrl: '/buyerlist.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
                    $scope.batch_close = function () {
                        $modalInstance.close('closed');
                    };
                    $scope.batch_ok=function(){
                        var url = 'api/IdasTrnPhyDoc/DocBatch';
                        var params = {
                            sanction_gid: sanction_gid,
                            sanctionref_no: $scope.sanctionrefno,
                            customer_gid:customer_gid,
                            customer_urn:$scope.Customerurn,
                            customer_name:$scope.customerName,
                        };
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                             
                                $modalInstance.close('closed');
                                Notify.alert(resp.data.message, 'success')
                               
                            }
                            else {
                               
                                Notify.alert(resp.data.message)
                            }
                            activate();
                        });
                    }
                    var url = 'api/IdasMstSanction/GetBuyerinfoEdit';
                    var params = {
                        sanction_gid: sanction_gid
                      };
                      SocketService.getparams(url,params).then(function (resp) {
                        $scope.buyer_list = resp.data.buyer_list;
    
                    });
    
                }
                
               
    
            }
            else{
                SweetAlert.swal({
                    title: 'Are you sure?',
                    text: 'Do You Want To Create the Batch ?',
    
                    showCancelButton: true,
                    confirmButtonColor: '#DD6B55',
                    confirmButtonText: 'Yes, Batch It!',
                    closeOnConfirm: false
                }, function (isConfirm) {
                    if (isConfirm) {
                        lockUI();
                        var params = {
                            sanction_gid: sanction_gid,
                            sanctionref_no: $scope.sanctionrefno,
                            customer_gid:customer_gid,
                            customer_urn:$scope.Customerurn,
                            customer_name:$scope.customerName,
                        };
                        var url = 'api/IdasTrnPhyDoc/DocBatch';
                        SocketService.post(url, params).then(function (resp) {
    
                            if (resp.data.status == true) {
                                activate();
                                SweetAlert.swal('Batch Created Successfully!');
                                unlockUI();
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

                var params = {
                    sanction_gid: sanction_gid,
                    type_of_copy: 'Physical Copy'
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
                        type_of_copy:'Physical Copy'
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
                         }

                    });
                }
            }
        }

        $scope.export = function () {
            var params = {
                sanction_gid: sanction_gid,
                type_of_copy: 'Physical Copy'
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

        $scope.openpanel = function () {

            $scope.show = false;
            $scope.options = true;
        }
        $scope.cancel = function () {

            $scope.show = true;

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

        $scope.Verify = function (sanctiondocument_gid) {
            var url = 'api/IdasTrnPhyDoc/PhyDocVerify';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    Notify.alert(resp.data.message, 'success')
                    activate();
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
                activate();
            });
        }

        $scope.docConMkr = function (sanctiondocument_gid, conversation_count, phydoc_status) {
            localStorage.setItem('sanctiondocument_gid', sanctiondocument_gid);
            localStorage.setItem('conversation_count', conversation_count);
            localStorage.setItem('phydoc_status', phydoc_status);
            $state.go('app.idasTrnPhyDocConversation');
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
            frm.append('types_of_doc', 'Physical Copy');
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

        $scope.phydocback = function ()
        {
           if( $scope.batch_status=='Pending')
           {
            $location.url('app/idasTrnPhyDocSummary?lstab=pending');
           // $state.go('app.idasTrnPhyDocSummary');
           }else{
            $location.url('app/idasTrnPhyDocSummary?lstab=created');
          //  $state.go('app.idasTrnPhyDocSummary');
           }
           
        }

    }
})();
