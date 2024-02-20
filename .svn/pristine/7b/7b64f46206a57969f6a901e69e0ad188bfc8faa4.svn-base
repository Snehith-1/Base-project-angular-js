(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnWorkItemMail', iasnTrnWorkItemMail);

    iasnTrnWorkItemMail.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$timeout', '$window'];

    function iasnTrnWorkItemMail($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $timeout, $window) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnWorkItemMail';
        var lsdecision;
        $scope.lsShowBCC = true;
        $scope.lsShowCC = true;
        activate();

        function activate() {
            var url = 'api/IasnTrnWorkItem/Mailtempdelete';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/IasnTrnEmailSignature/GetEmailSignature';
            SocketService.get(url).then(function (resp) {

                $scope.pushbackcontent = resp.data.emailsignature;

            });

        }

   
        $scope.sendmail = function () {

            if ($scope.pushbackcontent == undefined) {
                Notify.alert('Write the body of the content', 'success');
                return;
            }

            var params = {
                email_subject: $scope.email_subject,
                frommail_id: $scope.fromMail,
                tomail_id: $scope.toMail,
                ccmail_id: $scope.ccMail,
                bccmail_id: $scope.bcc_mail,
                mailcontent: $scope.pushbackcontent
            }
            var url = 'api/IasnTrnWorkItem/ComposeMail';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go("app.iasnWomWorkOrderSummary");
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go("app.iasnWomWorkOrderSummary");
                }
            });
        }

        $scope.uploadattachment = function () {
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                var frm = new FormData();
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i]);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;
                }
                var url = 'api/IasnTrnWorkItem/ComposeMailAttachment';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $("#file").val('');
                    if (resp.data.status == true) {
                        var url = 'api/IasnTrnWorkItem/GetComposeMailAttachment';

                        SocketService.get(url).then(function (resp) {

                            $scope.uploaddocument = resp.data.MdlDocDetails;

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
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }

                });
            }
            else
            {
                alert('Please select a file.')
            }
        }
        

        $scope.UploadDocCancel = function (id) {
            var params = {
                composemailattachment_gid: id
            }
            var url = 'api/IasnTrnWorkItem/DeleteComposeMailAttachment';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = 'api/IasnTrnWorkItem/GetComposeMailAttachment';

                    SocketService.get(url).then(function (resp) {

                        $scope.uploaddocument = resp.data.MdlDocDetails;

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

        $scope.downloads = function (val1, val2) {

            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split(".")
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }
    }
})();