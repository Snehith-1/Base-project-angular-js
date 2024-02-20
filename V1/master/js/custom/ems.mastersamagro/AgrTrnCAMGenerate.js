(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCAMGenerateController', AgrTrnCAMGenerateController);

    AgrTrnCAMGenerateController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','DownloaddocumentService'];

    function AgrTrnCAMGenerateController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route,DownloaddocumentService) {

        $scope.title = 'AgrTrnCAMGenerateController';
        var vm = this;

        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.appcreditapproval_gid = $location.search().appcreditapproval_gid;
        var appcreditapproval_gid = $scope.appcreditapproval_gid;
        $scope.lstab = $location.search().lstab;
        var lstab = $scope.lstab;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.product_gid = $location.search().product_gid;
        var product_gid = $scope.product_gid;
        $scope.variety_gid = $location.search().variety_gid;
        var variety_gid = $scope.variety_gid;
        $scope.shortclosing_flag = $location.search().shortclosing_flag;
        var shortclosing_flag = $scope.shortclosing_flag;

        activate();
        // $scope.application_gid = $location.search().application_gid;
        // var application_gid = $scope.application_gid;
        if (localStorage.getItem('RefreshTemplate') && localStorage.getItem('RefreshTemplate') != 'Y') {
            location.reload();
            localStorage.setItem('RefreshTemplate', 'Y');
            return false;
        }
        function activate() {
            var url = "api/AgrTrnCAMGeneration/GetCAMTemplate"
            lockUI();
            var param = {
                application_gid: $location.search().application_gid
            };
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.content = resp.data.template_content;
                document.getElementById('test1').innerHTML += $scope.content;

            });

            var url = "api/AgrTrnCAMGeneration/GetApp2CAM"
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();

                $scope.cam_content = resp.data.template_content;
                $scope.lspath = resp.data.lspath;
                $scope.lsname = resp.data.lsname;
            });
        }

        $scope.camdocdownload = function (val1, val2) {
            // var phyPath = val1;
            // var relPath = phyPath.split("EMS");
            // var relpath1 = relPath[1].replace("\\", "/");
            // var hosts = window.location.host;
            // var prefix = location.protocol + "//";
            // var str = prefix.concat(hosts, relpath1);
            // var link = document.createElement("a");
            // var name = val2.split(".")
            // link.download = val2;
            // var uri = str;
            // link.href = uri;
            // link.click();

            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = name[0];
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.back = function () {
            // $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid);
            $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage='+ lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid + '&shortclosing_flag=' + shortclosing_flag);
        }
        $scope.WordGenerate = function () {
            if ($scope.cam_content != '' && $scope.cam_content != null) {
                $scope.content = $scope.cam_content;
            }
            var params = {
                application_gid: $location.search().application_gid,
                content: $scope.content
            }
            var url = 'api/AgrTrnCAMGeneration/WordGenerate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var phyPath = resp.data.lspath;
                    var filename1 = resp.data.lsname;
                    var phyPath = phyPath.replace("//", "/");
                    var phyPath = phyPath.replace("\\", "/");
                    var relPath = phyPath.split("EMS/");
                    var relpath1 = relPath[1].replace("\\", "/");

                    var url = 'api/azurestorage/FileUploadDocument';
                    var params = {
                        file_path: relpath1
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            DownloaddocumentService.Downloaddocument(relpath1, filename1);
                            Notify.alert('CAM Generated Successfully !', 'success')
                            activate();
                            unlockUI();
                        }
                        else {
                            unlockUI();
                            Notify.alert('Error Occurred While Export PDF !', 'warning');
                            activate();
                        }
                    });
                }

                //unlockUI();
                //if (resp.data.status == true) {
                //    var phyPath = resp.data.lspath;
                //    var relPath = phyPath.split("EMS");
                //    var relpath1 = relPath[1].replace("\\", "/");
                //    var hosts = window.location.host;
                //    var prefix = location.protocol + "//";
                //    var str = prefix.concat(hosts, relpath1);
                //    var link = document.createElement("a");
                //    var name = resp.data.lsname.split('.');
                //    link.download = name[0];
                //    var uri = str;
                //    link.href = uri;
                //    link.click();
                //    Notify.alert('CAM Generated Successfully !', 'success')

                //    activate();
                //}
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Downloading !', 'warning')
                }
            });
        }
        $scope.WordSave = function () {
            if ($scope.cam_content != '' && $scope.cam_content != null) {
                $scope.content = $scope.cam_content;
            }
            var params = {
                application_gid: $location.search().application_gid,
                content: $scope.content
            }
            var url = 'api/AgrTrnCAMGeneration/PostWordSave';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
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
                    activate();

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