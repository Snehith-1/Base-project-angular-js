(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMstAddTemplateController', idasMstAddTemplateController);

    idasMstAddTemplateController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function idasMstAddTemplateController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
    
        $scope.title = 'idasMstAddTemplateController';
        var vm = this;

        activate();

        function activate() {

            var url = "api/idasMstTemplate/GetTemplateType";
            SocketService.get(url).then(function (resp) {
                $scope.cbotemplatetype_list = resp.data.templatetype_list;

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

        $scope.submitTemplate = function () {

            var params = {
                template_name: $scope.templatename,
                templatetype_name: $scope.cbotemplatetype.templatetype_name,
                template_content: $scope.content

            }


            var url = 'api/idasMstTemplate/IdasTemplateSubmit';
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