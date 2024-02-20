(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnComposeMail360', iasnTrnComposeMail360);

    iasnTrnComposeMail360.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function iasnTrnComposeMail360($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnComposeMail360';

        activate();

        function activate() {
            $scope.composemail_gid = localStorage.getItem('composemail_gid');
            var url = 'api/IasnTrnWorkItem/ComposeMailview';

            var params = {
                lscomposemail_gid: localStorage.getItem('composemail_gid')

            };
         
            SocketService.getparams(url, params).then(function (resp) {
                $scope.frommail_id = resp.data.frommail_id;
                $scope.email_subject = resp.data.email_subject;
                $scope.mailcontent = resp.data.mailcontent;
                $scope.ccmail_id = resp.data.ccmail_id;
                $scope.bccmail_id = resp.data.bccmail_id;
                $scope.tomail_id = resp.data.tomail_id;
                $scope.attach_list = resp.data.MdlAttachmentList;
                $scope.zone_name = resp.data.zone_name;
                $scope.email_date = resp.data.email_date;
                $scope.email_status = resp.data.email_status;
                $scope.composemail_gid = resp.data.composemail_gid;
            });

            var url = 'api/IasnTrnAuditLog/PostComposeAuditView';

            var params = {
                composemail_gid: localStorage.getItem("composemail_gid")
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });

            var params = {
                composemail_gid: localStorage.getItem("composemail_gid")
            };

            var url = "api/IasnTrnWorkItem/ComposeReferenceMail";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.referenceMail = resp.data.MdlReferenceMail;
                    // $scope.attch_list=resp.data.MdlAttachmentList;  
                }
                else {

                }
            });
        }

        $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

        $scope.logdetails = function () {

            if ($scope.IsLogShow == true) {
                $scope.IsLogShow = false;
            }
            else {
                $scope.IsLogShow = true;

                var url = 'api/IasnTrnAuditLog/ComposeAuditLog';

                var params = {
                    composemail_gid: localStorage.getItem("composemail_gid")
                };
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.auditlog_list = resp.data.MdlAuditLog;
                    }
                    else {

                    }
                });
            }
        }
        $scope.logClose = function () {
            $scope.IsLogShow = false;
        }

        $scope.export = function (path, attchment_name) {


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

        $scope.back = function () {
            $state.go('app.iasnWomWorkOrderSummary');
        }

        $scope.signature = function () {
            var modalInstance = $modal.open({
                templateUrl: '/signatureContent.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/IasnTrnEmailSignature/GetEmailSignature';
                SocketService.get(url).then(function (resp) {

                    $scope.EmailSignature = resp.data.emailsignature;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.close = function () {
                    $modalInstance.close('closed');
                }



                $scope.submit = function () {
                    lockUI();
                    var params = {
                        emailsignature: $scope.EmailSignature
                    }

                    var url = "api/IasnTrnEmailSignature/PostEmailSignature";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            $modalInstance.close('closed');

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
            }
        }

        $scope.forward = function (val, val1, val2, val3, val4) {
            $scope.ccMail = $scope.cc_mail;
            localStorage.setItem('composemail_gid', val);
            localStorage.setItem('toMail', val2);
            localStorage.setItem('ccMail', val3);
            localStorage.setItem('bccMail', val4);
            localStorage.setItem('email_subject', val1);
            localStorage.setItem('decision', 'Forward');
            localStorage.setItem('lspage', 'composemail');
            $state.go('app.iasnTrnForwardMail');
        }

        $scope.archivalWI = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/archivalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {

                $scope.onclickspecific = function () {
                    $scope.PnlSpecific = true;
                    $scope.customer = "";
                    $scope.cbosanctionrefno = "";
                }
                $scope.onclickcustomer = function () {
                    $scope.PnlSpecific = false;
                    $scope.customer = "";

                }

                $scope.complete = function (string) {

                    if (string.length >= 3) {
                        $scope.message = "";
                        var url = 'api/customer/ExploreCustomer';
                        var params = {
                            customername: string
                        }
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                $scope.message = "";
                                $scope.customer_list = resp.data.Customers;
                            }
                            else {
                                $scope.customer = "";
                                $scope.message = "No Records";
                            }


                        });
                    }
                    else {
                        $scope.customer_list = null;
                        $scope.message = "Type atleast three character";
                    }
                }

                $scope.fillTextbox = function (customer_gid, customer_name) {

                    $scope.customer = customer_name;
                    $scope.customer_gid = customer_gid;
                    $scope.customer_list = null;

                    var params = {
                        customer_gid: customer_gid
                    }


                    var url = 'api/loan/customer_getheads';

                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.sanctiondtl = resp.data.sanctiondtl;

                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.ArchivalSubmit = function () {
                    var sanctionref_no = '';
                    var sanction_gid = '';

                    if ($scope.archival.types_of_archival == 'Specific') {
                        if ($scope.cbosanctionrefno == undefined) {
                            modalInstance.close('closed');
                            Notify.alert('Select the Sanction Ref No.', 'warning');
                            return;
                        }
                        else {
                            sanctionref_no = $('#sanction :selected').text();
                            sanction_gid = $scope.cbosanctionrefno.sanction_Gid;
                        }
                    }


                    var params = {
                        composemail_gid: val,
                        archival_type: $scope.archival.types_of_archival,
                        remarks: $scope.archival.Remarks,
                        customer_gid: $scope.customer_gid,
                        customer_name: $scope.customer,
                        sanctionref_no: sanctionref_no,
                        sanction_gid: sanction_gid,
                        status: "Archival"
                    }
                    var url = 'api/IasnTrnWorkItem/ComposeMailDecision';
                    lockUI()
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')

                        }
                        else {

                            modalInstance.close('closed');
                            Notify.alert(resp.data.message)
                        }
                        activate();
                        $state.go("app.iasnWomWorkOrderSummary");
                    });

                }
            }
        }
    }
})();
