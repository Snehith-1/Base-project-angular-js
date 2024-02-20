(function () {
    'use strict';

    angular
        .module('angle')
        .controller('helpdashboardController', helpdashboardController);

    helpdashboardController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'SweetAlert', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function helpdashboardController($rootScope, $scope, $sce, $state,SweetAlert, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'helpdashboardController';

        activate();

        function activate() {
           
            lockUI();

            var url = 'api/HlpMstHelp/screenshottempdelete';
            SocketService.get(url).then(function (resp) {

            });

            var url = 'api/HlpMstHelp/Getcompanylogo';

            SocketService.get(url).then(function (resp) {
                $scope.company_code = resp.data.company_code;
                $scope.company_logo = resp.data.company_uilogo_path;
            });


            var url = 'api/HlpMstHelp/Getsinglemodule';

            SocketService.get(url).then(function (resp) {

                $scope.module_gid = resp.data.module_gid;
                $scope.moduleName = resp.data.module_name;
                modulename($scope.module_gid);
            });
            var url = 'api/HlpMstHelp/Getmodule';

            SocketService.get(url).then(function (resp) {

                $scope.moduleList = resp.data.modulesummary;
               
            });
            unlockUI();
        }

        $scope.uploadphoto = function (val, val1, name) {
            lockUI();
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            $scope.uploadfrm = frm;
            var url = 'api/HlpMstHelp/attachmentUpload';

            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
             
                
                var url = "api/HlpMstHelp/getattachPhoto";
                SocketService.get(url).then(function (resp) {
                    $scope.attachphoto = resp.data.attachphoto;
                });
                if (resp.data.status == true) {
                   
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                unlockUI();
            });
        }

        function modulename(module_gid) {

            lockUI();
            
            var params = {
                module_gid: module_gid
            };
            var url = 'api/HlpMstHelp/Getfaq';

            SocketService.getparams(url, params).then(function (resp) {

                $scope.FaqList = resp.data.faqsummary;

            });
            var url = 'api/HlpMstHelp/Getreplyofqueries';

            SocketService.getparams(url, params).then(function (resp) {

                $scope.replyqueryList = resp.data.replyquerysummary;

            });

            var url = 'api/HlpMstHelp/Getqueries';

            SocketService.getparams(url, params).then(function (resp) {

                $scope.queryList = resp.data.querysummary;

            });

            var url = 'api/HlpMstHelp/getpage';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.pagename_list = resp.data.pagename_list;
            });
            
            unlockUI();
        }

        $scope.module = function (module_gid)
        {
           
            lockUI();
            var url = 'api/HlpMstHelp/Getsinglemodulename';
            var params = {
                module_gid: module_gid
            };

            SocketService.getparams(url, params).then(function (resp) {
              
                $scope.moduleName = resp.data.module_name;
              
            });
            
            var params = {
                module_gid: module_gid
            };
            var url = 'api/HlpMstHelp/Getfaq';
            SocketService.getparams(url, params).then(function (resp) {
                
                $scope.FaqList = resp.data.faqsummary;
               
            });

            var url = 'api/HlpMstHelp/Getqueries';

            SocketService.getparams(url, params).then(function (resp) {

                $scope.queryList = resp.data.querysummary;

            });

            var url = 'api/HlpMstHelp/Getreplyofqueries';

            SocketService.getparams(url, params).then(function (resp) {

                $scope.replyqueryList = resp.data.replyquerysummary;

            });
            
            var url = 'api/HlpMstHelp/getpage';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.pagename_list = resp.data.pagename_list;
            });
            
            unlockUI();
        }

        $scope.trustAsHtml = function (string) {
            return $sce.trustAsHtml(string);
        };

        $scope.querySubmit = function ()
        {
            if (($scope.query_title == '') || ($scope.query_title == undefined)) {
                Notify.alert('Kindly Enter Query Title', 'warning')
            }
            else {
                var params = {

                    page_name: $scope.pagename.page_name,
                    page_gid: $scope.pagename.page_gid,
                    txt_querytitle: $scope.query_title,
                    txtquerydescription: $scope.query_description,

                }
                
                var url = 'api/HlpMstHelp/queryCreate';
                lockUI()
                SocketService.post(url, params).then(function (resp) {

                    if (resp.data.status == true) {
                        var url = "api/HlpMstHelp/getattachPhoto";
                        SocketService.get(url).then(function (resp) {
                            $scope.attachphoto = resp.data.attachphoto;
                        });
                        var params = {
                            module_gid: resp.data.moduleparent_name
                        }
                        var url = 'api/HlpMstHelp/Getqueries';

                        SocketService.getparams(url, params).then(function (resp) {

                            $scope.queryList = resp.data.querysummary;

                        });
                        unlockUI();
                        $scope.query_title = "";
                        $scope.query_description = "";
                        $scope.pagename = "";                        
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error While Raising the Query')
                    }
                });
                }
        }

        $scope.photodelete = function (val) {
            var params = { id: val };
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Screenshot ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/HlpMstHelp/attachphotoUploadcancel';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            var url = "api/HlpMstHelp/getattachPhoto";
                            SocketService.get(url).then(function (resp) {
                                $scope.attachphoto = resp.data.attachphoto;
                            });
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert('Internal Error Occurred!', {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        SweetAlert.swal('Deleted Successfully!');
                    });

                }

            });
           
        }


    $scope.deletequery = function(val1,val2)
    {
        var params = { raisequery_gid: val1 };
        $scope.module_gid = val2;
        SweetAlert.swal({
            title: 'Are you sure?',
            text: 'Do You Want To Delete the Query ?',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Yes, delete it!',
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                var url = 'api/HlpMstHelp/querycancel';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        var params = {
                            module_gid: $scope.module_gid
                        }
                        var url = 'api/HlpMstHelp/Getqueries';

                        SocketService.getparams(url, params).then(function (resp) {

                            $scope.queryList = resp.data.querysummary;

                        });
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        SweetAlert.swal('Deleted Successfully!');
                    }
                    else {
                        Notify.alert('Internal Error Occurred!', {
                            status: 'Warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });

            }

        });

       
    }

    }
})();
