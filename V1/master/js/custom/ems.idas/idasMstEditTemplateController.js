(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMstEditTemplateController', idasMstEditTemplateController);

    idasMstEditTemplateController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function idasMstEditTemplateController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {

        $scope.title = 'idasMstEditTemplateController';
        var vm = this;
        $scope.template_gid = $location.search().template_gid;
        activate();

        function activate() {

            
            var url = "api/idasMstTemplate/GetTemplateType";
            SocketService.get(url).then(function (resp) {
                $scope.cbotemplatetype_list = resp.data.templatetype_list;

            });

            lockUI();
            var url = "api/idasMstTemplate/GetTemplateDtl"
            var param = {
                template_gid: $scope.template_gid
            };

            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();

                $scope.template_name = resp.data.template_name;
                $scope.templatetype_gid = resp.data.templatetype_gid;
                $scope.templatetype_name = resp.data.templatetype_name;
                $scope.content = resp.data.template_content;
                
            });

        };
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
        $scope.updateTemplate = function () {

            var templatetype_name = $('#templatetype_name :selected').text();

            var params = {
                template_gid: $scope.template_gid,
                template_name: $scope.template_name,
                templatetype_name: templatetype_name,
                template_content: $scope.content

            }


            var url = 'api/idasMstTemplate/IdasUpdateTemplate';
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    $state.go('app.idasMstTemplateSummary');
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }

            });
        }

        $scope.back = function () {
            $state.go('app.idasMstTemplateSummary');

        }
    }
})();