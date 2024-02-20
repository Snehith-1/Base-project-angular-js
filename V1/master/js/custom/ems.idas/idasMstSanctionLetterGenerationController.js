(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMstSanctionLetterGenerationController', idasMstSanctionLetterGenerationController);

    idasMstSanctionLetterGenerationController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function idasMstSanctionLetterGenerationController($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, $sce, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */  var vm = this;
        vm.title = 'idasMstSanctionLetterGenerationController';
        var sanction_gid = $location.search().sanction_gid;
        activate();

        function activate() {
            lockUI();
            var url = window.location.href;
            var relPath = url.split("lstab=");
            $scope.relpath1 = relPath[1];
          
            var url = 'api/IdasMstSanction/SanctionDtlsEdit';
            var params = {
                sanction_gid: sanction_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionrefnoEdit = resp.data.sanction_refno;
                $scope.SanctionDateEdit = resp.data.sanctionDate;
                $scope.sanction_date = resp.data.sanction_date;
                $scope.SanctionAmountEdit = resp.data.sanction_amount;
                $scope.customerNameEdit = resp.data.customername;
            });
               
            var url = 'api/IdasMstSanction/GetTemplate_list';
            SocketService.get(url).then(function (resp) {
                $scope.template_list = resp.data.template_list;
            });
            var url = 'api/IdasMstSanction/GetTemplateDetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionletter_status = resp.data.sanctionletter_status;
                $scope.template_name = resp.data.template_name;
                $scope.content = resp.data.template_content;
                $scope.sanctionletter_flag = resp.data.sanctionletter_flag;
                $scope.checkerpushback_remarks = resp.data.checkerpushback_remarks;
                unlockUI();
                if(resp.data.sanctionletter_status == 'Generated')
                {
                    $scope.sanction_template = true;
                    $scope.sanction_template_bind = true;
                }
                else {
                    $scope.sanction_template = false;
                    $scope.sanction_template_bind = false;
                }
            });

            var url = 'api/IdasMstSanction/SanctionLetterSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiontocheckerlist = resp.data.sanctiondetails;
            });

            var url = 'api/IdasMstSanction/SanctionDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.uploaddocument = resp.data.UploadDocumentList;
            });
        }

        $scope.sanctionback = function (relpath1) {
            $location.url('app/idasMstSanctionSummary?lstab=' + relpath1);
        }

        // Template Updation
        $scope.sanctiontemplatesubmit = function () {
            lockUI();

            if ($scope.cbotemplate.template_name == 'Sanction - Simplified Norms Single Facility') {
                var param = {
                    sanction_gid: sanction_gid,
                    template_gid: $scope.cbotemplate.template_gid,
                    template_name: $scope.cbotemplate.template_name,
                    lstab: $scope.relpath1,
                };
                var url = 'api/IdasMstSanction/SanctionContent';
                SocketService.post(url, param).then(function (resp) {
                    $scope.content = resp.data.template_content;
                    unlockUI();
                    $scope.sanction_template = true;
                    $scope.sanction_template_bind = true;
                    $scope.template_name = resp.data.template_name
                });

            }

            else if ($scope.cbotemplate.template_name == 'Sanction - Simplified Norms 2 Facility') {

                var param = {
                    sanction_gid: sanction_gid,
                    template_gid: $scope.cbotemplate.template_gid,
                    template_name: $scope.cbotemplate.template_name,
                    lstab: $scope.relpath1,
                };
                var url = 'api/IdasMstSanction/Sanction2Facility';
                SocketService.post(url, param).then(function (resp) {
                    $scope.content = resp.data.template_content;
                    unlockUI();
                    $scope.sanction_template = true;
                    $scope.sanction_template_bind = true;
                    $scope.template_name = resp.data.template_name
                });

            }

            else if ($scope.cbotemplate.template_name == 'Sanction - Multiple Facility') {

                var param = {
                    sanction_gid: sanction_gid,
                    template_gid: $scope.cbotemplate.template_gid,
                    template_name: $scope.cbotemplate.template_name,
                    lstab: $scope.relpath1,
                };
                var url = 'api/IdasMstSanction/SanctionMultipleFacility';
                SocketService.post(url, param).then(function (resp) {
                    $scope.content = resp.data.template_content;
                    unlockUI();
                    $scope.sanction_template = true;
                    $scope.sanction_template_bind = true;
                    $scope.template_name = resp.data.template_name
                });

            }

            else if ($scope.cbotemplate.template_name == 'Sanction - Interchangability') {

                var param = {
                    sanction_gid: sanction_gid,
                    template_gid: $scope.cbotemplate.template_gid,
                    template_name: $scope.cbotemplate.template_name,
                    lstab: $scope.relpath1,
                };
                var url = 'api/IdasMstSanction/SanctionContent';
                SocketService.post(url, param).then(function (resp) {
                    $scope.content = resp.data.template_content;
                    unlockUI();
                    $scope.sanction_template = true;
                    $scope.sanction_template_bind = true;
                    $scope.template_name = resp.data.template_name
                });

            }

            else if ($scope.cbotemplate.template_name == 'Sanction - DBS Colending') {

                var param = {
                    sanction_gid: sanction_gid,
                    template_gid: $scope.cbotemplate.template_gid,
                    template_name: $scope.cbotemplate.template_name,
                    lstab: $scope.relpath1,
                };
                var url = 'api/IdasMstSanction/DBSColending';
                SocketService.post(url, param).then(function (resp) {
                    $scope.content = resp.data.template_content;
                    unlockUI();
                    $scope.sanction_template = true;
                    $scope.sanction_template_bind = true;
                    $scope.template_name = resp.data.template_name
                });

            }

            else if ($scope.cbotemplate.template_name == 'Sanction - Stand by line of credit') {

                var param = {
                    sanction_gid: sanction_gid,
                    template_gid: $scope.cbotemplate.template_gid,
                    template_name: $scope.cbotemplate.template_name,
                    lstab: $scope.relpath1,
                };
                var url = 'api/IdasMstSanction/SanctionContent';
                SocketService.post(url, param).then(function (resp) {
                    $scope.content = resp.data.template_content;
                    unlockUI();
                    $scope.sanction_template = true;
                    $scope.sanction_template_bind = true;
                    $scope.template_name = resp.data.template_name
                });

            }
            else {
                Notify.alert("Error occurred", 'warning');
            }
            $scope.cancel = function () {
                $scope.sanction_template = false;
                $scope.sanction_template_bind = false;
            }
        }

        // Sanction Letter Submit Event
        $scope.sanctionletterSubmit = function () {
            lockUI();
            var param = {
                sanction_gid: sanction_gid,
                template_content: $scope.content
            };
            var url = 'api/IdasMstSanction/SanctionLetterSubmit';
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    $location.hash('sanctionlettertopview');
                    $anchorScroll();
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

        // Sanction Letter View
        $scope.sanctionletterview = function (relpath1) {
            $location.url('app/idasMstSanctionLetterWordView?sanction_gid=' +sanction_gid +'&lstab=' + relpath1);
        }

        // Sanction Letter Moved to Checker
        $scope.proceedtochecker = function () {
            lockUI();
            var param = {
                sanction_gid: sanction_gid,
                template_content: $scope.content
            };
            var url = 'api/IdasMstSanction/PostProceedToChecker';
            SocketService.post(url, param).then(function (resp) {
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
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }

        // Sanction Letter View
        $scope.sanctiontocheckerview = function (sanctionapprovallog_gid, customer2sanction_gid, relpath1) {
            $location.url('app/idasMstSanctionLetterWordView?sanctionapprovallog_gid=' + sanctionapprovallog_gid + '&sanction_gid=' + customer2sanction_gid + '&lstab=' + relpath1);
        }
        // Upload Document
        $scope.uploadattachment = function () {
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                var frm = new FormData();
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i]);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fname, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                }
                frm.append('sanction_gid', sanction_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/IdasMstSanction/SanctionDocAttachment';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $("#file").val('');
                    if (resp.data.status == true) {
                        var params = {
                            sanction_gid: sanction_gid
                        };
                        var url = 'api/IdasMstSanction/SanctionDocumentList';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.uploaddocument = resp.data.UploadDocumentList;
                        });
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
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
            else {
                alert('Please select a file.')
            }
        }
        // Download Document
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
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
        }
        // Delete Document
        $scope.UploadDocCancel = function (sanctiondoc_gid) {
            var params = {
                sanctiondoc_gid: sanctiondoc_gid,
                sanction_gid: sanction_gid
            }
            var url = 'api/IdasMstSanction/SanctionDocumentDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
               
                if (resp.data.status == true) {
                    var params = {
                        sanction_gid: sanction_gid
                    };
                    var url = 'api/IdasMstSanction/SanctionDocumentList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.uploaddocument = resp.data.UploadDocumentList;
                    });

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
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.titleOptions = {
            placeholderText: 'Add a Title',
            charCounterCount: false,
            toolbarInline: true,
            events: {
                'contentChanged': function (e, editor) {
                    console.log('content changed', $scope.titleOptions.froalaEditor.html.get());
                },
                'initialized': function (editor) {
                    console.log('initialized', this);
                }
            }
        };
        $scope.initialize = function (initControls) {
            $scope.initControls = initControls;
            $scope.deleteAll = function () {
                initControls.getEditor().html.set('34434');
            };
        };
        $scope.imgModel = { src: 'image.jpg' };
        $scope.buttonModel = { innerHTML: 'Click Me' };
        $scope.inputModel = { placeholder: 'I am an input!' };
        $scope.inputOptions = {
            angularIgnoreAttrs: ['class', 'ng-model', 'id', 'froala']
        }
        $scope.initializeLink = function (linkInitControls) {
            $scope.linkInitControls = linkInitControls;
        };
        $scope.linkModel = { href: 'https://www.froala.com/wysiwyg-editor' }
    }
})();
