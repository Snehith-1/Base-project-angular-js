(function () {
    'use strict';

    angular
        .module('angle')
        .controller('composeMail', composeMail);

        composeMail.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies','$timeout','$window'];

    function composeMail($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies,$timeout,$window) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'composeMail';
        var lsdecision;
       $scope.lsShowBCC=true;
       $scope.lsShowCC=true;
        activate();

        function activate() {
         $scope.email_gid=localStorage.getItem('email_gid');
         $scope.toMail=localStorage.getItem('toMail');
         $scope.ccMail=localStorage.getItem('ccMail');
         $scope.bccMail=localStorage.getItem('bccMail');
         $scope.email_subject=localStorage.getItem('email_subject');
         $scope.message_id=localStorage.getItem('message_id');
         $scope.reference_id=localStorage.getItem('reference_id');
         $scope.rmemployee_gid=localStorage.getItem('rmemployee_gid');
         $scope.rmemployee_name=localStorage.getItem('rmemployee_name');
         $scope.decision=localStorage.getItem('decision');
         $scope.lspage=localStorage.getItem('lspage');
         $scope.originalmail_Subject = localStorage.getItem('originalmail_Subject');
       
         var url = 'api/IasnTrnEmailSignature/GetEmailSignature';
                SocketService.get(url).then(function (resp) {
                    
                    $scope.pushbackcontent=resp.data.emailsignature;   
                       
                });
            
             }
             $scope.uploadattachment = function () {
                 var fi = document.getElementById('addupload');
                 if (fi.files.length > 0) {
                     var frm = new FormData();
                     for (var i = 0; i <= fi.files.length - 1; i++) {

                         frm.append(fi.files[i].name, fi.files[i]);
                         frm.append('project_flag', "Default");
                         $scope.uploadfrm = frm;
                     }
                     var url = 'api/IasnTrnWorkItem/MailAttchment';
                     lockUI();
                     SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                         $("#addupload").val('');
                         $scope.txtdocument_title = '';
                         console.log(resp.data);
                         if (resp.data.status == true) {
                             unlockUI();
                             Notify.alert('Document Uploaded Successfully..!!', 'success')

                             var url = 'api/IasnTrnWorkItem/MailAttchment';

                             SocketService.get(url).then(function (resp) {

                                 $scope.uploaddocument = resp.data.MdlDocDetails;

                             });
                         }
                         else {
                             unlockUI();
                             Notify.alert('File Format Not Supported!')
                         }

                     });
                 }
                 else {
                     alert("Please select a file.");
                 }
            }
         
            $scope.updateDesicion=function(){
            
                if($scope.pushbackcontent==undefined){
                    Notify.alert('Write the body of the content','success');
                    return;
                }
              
                var params={
                    email_gid:$scope.email_gid,
                    decision:$scope.decision,
                    employee_gid:$scope.rmemployee_gid,
                    employee_name:$scope.rmemployee_name,
                    remarks:'',
                    mailcontent:$scope.pushbackcontent,
                    subject:$scope.email_subject,
                    tomail_id:$scope.toMail,
                    ccmail_id:$scope.ccMail,
                    bccmail_id:$scope.bcc_mail,
                    message_id:$scope.message_id,
                    reference_id:$scope.reference_id
                }
                console.log(params)
                var url='api/IasnTrnWorkItem/PostDecision';
                lockUI();
                SocketService.post(url,params).then(function (resp) {
                    unlockUI();
                    if(resp.data.status==true){
                       
                        Notify.alert(resp.data.message,'success')
                                    
                        if($scope.lspage == "workitem")
                        {
                           
                            $state.go("app.iasnTrnWorkItemSummary");
                        }
                        else if($scope.lspage == "myworkitem")
                        {
                           
                            $state.go("app.iasnTrnMyWorkItemSummary");
                        }
                        else if($scope.lspage == "myconsolidateworkitem")
                        {
                           
                            $state.go("app.iasnConsolidatedWorkItem");
                        }
                     
                    }
                    else{
                                     
                        Notify.alert(resp.data.message,'warning')                       
                     
                        if($scope.lspage =="workitem")
                        {                           
                            $state.go("app.iasnTrnWorkItemSummary")
                        }
                        else if($scope.lspage =="myworkitem")
                        {                          
                            $state.go("app.iasnTrnMyWorkItemSummary");
                        }
                        else if($scope.lspage == "myconsolidateworkitem")
                        {
                           
                            $state.go("app.iasnConsolidatedWorkItem");
                        }
                    }
                });
    
    
            }
           

            $scope.UploadDocCancel = function (id) {
                var params = {
                    mailattachment_gid: id
                }
                var url = 'api/IasnTrnWorkItem/DeleteAttchment';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Document deleted Successfully..!!', 'success')
    
                        var url = 'api/IasnTrnWorkItem/MailAttchment';
    
                        SocketService.get(url).then(function (resp) {
    
                            $scope.uploaddocument = resp.data.MdlDocDetails;
    
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred')
    
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

            $scope.back = function() {
               
                if($scope.lspage =="workitem")
                {
                    $state.go('app.iasnTrnWorkItem360');
                }
                else if($scope.lspage =="myworkitem")
                {
                    $state.go('app.iasnTrnMyWorkItem360');
                }
                else if($scope.lspage == "myconsolidateworkitem")
                {
                   
                    $state.go("app.isanconsolidatedview");
                }
            } 

            
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnConsolidatedWorkItem', iasnConsolidatedWorkItem);

    iasnConsolidatedWorkItem.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function iasnConsolidatedWorkItem($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnConsolidatedWorkItem';

        activate();

        function activate() {
            lockUI();
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummary')
            }
            $scope.page = localStorage.getItem('page');

            var url = 'api/IasnTrnWorkItem/ConsolidatedWorkItem';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });
          
        }

        
        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };
        // Action Work Item 360
        $scope.WorkItem360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            $state.go("app.isanconsolidatedview");
        }

        $scope.view = function (val)
        {
            $location.url('app/isanconsolidatedview?lsemail_gid=' + val)
        }

        $scope.recproof_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }


        $scope.export = function () {
            lockUI();
            var url = 'api/IasnTrnWorkItem/GetConsolidateExcel';

            SocketService.get(url).then(function (resp) {

                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.excel_name);
                    //var phyPath = resp.data.excel_path;
                    //var name = resp.data.excel_name.split('.');
                    //recproof_downloads(phyPath, name);

                  /*/  var phyPath = resp.data.excel_path;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = resp.data.excel_name.split('.');
                    link.download = name[0];
                    var uri = str;
                    link.href = uri;
                    link.click();*/
               
            });
        }
       }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnDashboardController', iasnDashboardController);

        iasnDashboardController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'Colors', 'ChartData', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$timeout', 'SweetAlert', '$cookies'];

    function iasnDashboardController($rootScope, $scope, $state, AuthenticationService, Colors, ChartData, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $timeout, SweetAlert, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnDashboardController';

        activate();

        function activate() {
               
            var url = 'api/IasnTrnWorkItem/WorkItemCounts';
            SocketService.get(url).then(function (resp) {
                $scope.count_workitempending = resp.data.count_workitempending;
                $scope.count_workitemassigned=resp.data.count_workitemassigned;
                $scope.count_pushback = resp.data.count_pushback;
                $scope.count_forward=resp.data.count_forward;
                $scope.count_close = resp.data.count_close;
                $scope.count_archival=resp.data.count_archival;
                $scope.count_workitemtotal = resp.data.count_workitemtotal; 
                
            });
              var user_gid = localStorage.getItem('user_gid');
              var url = 'api/user/privilegelevel3';
              SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                var zonalmapping = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("ISNMSTZRM");
                var workitem = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("ISNWOMWO");
                var myworkitem = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("ISNWOMMWO");
                var archival = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("ISNWOMARC");
                var report = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("ISNRPTCSR");
                    if (zonalmapping != -1) {
                        $scope.zonal_show = 'Y';
                    }
                    else {
                        $scope.zonal_show = 'N';
                    }
                    if (workitem != -1) {
                        $scope.workitem_show = 'Y';
                    }
                    else {
                        $scope.workitem_show = 'N';
                    }
                    if (myworkitem != -1) {
                        $scope.myworkitem_show = 'Y';
                    }
                    else {
                        $scope.myworkitem_show = 'N';
                    }
                    if (archival != -1) {
                        $scope.archival_show = 'Y';
                    }
                    else {
                        $scope.archival_show = 'N';
                    }
                    if (report != -1) {
                        $scope.report_show = 'Y';
                    }
                    else {
                        $scope.report_show = 'N';
                    }
              });

        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstcheckeraddController', iasnMstcheckeraddController);

        iasnMstcheckeraddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnMstcheckeraddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnMstcheckeraddController';

        activate();

        function activate() { }

        $scope.addchecker = function () {
            $state.go('app.isanMstcheckeraddinfo');
        }

        $scope.back = function () {
            $state.go('app.isanMstTeamManagement');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstcheckeraddinfoController', iasnMstcheckeraddinfoController);

        iasnMstcheckeraddinfoController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnMstcheckeraddinfoController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnMstcheckeraddinfoController';

        activate();

        function activate() { }

        $scope.checkerback = function () {
            $state.go('app.isanMstcheckeradd');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstemployeeaddController', iasnMstemployeeaddController);

        iasnMstemployeeaddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function iasnMstemployeeaddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnMstemployeeaddController';

        activate();

        function activate() { }

        $scope.addemployee = function () {
            $state.go('app.isanMstemployeeaddinfo');
        }

        $scope.back = function () {
            $state.go('app.isanMstTeamManagement');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstemployeeaddinfoController', iasnMstemployeeaddinfoController);

        iasnMstemployeeaddinfoController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function iasnMstemployeeaddinfoController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnMstemployeeaddinfoController';

        activate();

        function activate() {
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
                $scope.employee_code = resp.data.criticallity;
                $scope.employee_mailid = resp.data.comments;
            });
        }

        $scope.onselectedchangeemployee = function (employee) {
            $scope.employee_gid = localStorage.setItem('onchangeemployee_gid', employee);
            var params = {
                employee_gid: $scope.employeegid.employee_gid

            }

        }

        $scope.employeeback = function () {
            $state.go('app.isanMstemployeeadd');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstTeamAdd', iasnMstTeamAdd);

        iasnMstTeamAdd.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnMstTeamAdd($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnMstTeamAdd';

        activate();

        function activate() {
          
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
         }

         $scope.Submit=function(){
             var params={
                team_name:$scope.TeamName,
                description:$scope.description,
                zonal_name:$scope.zone,
                team_mailid:$scope.TeamMail,
                MdlRmList:$scope.rmlist,
                MdlCheckerList:$scope.checkerlist
             }

             var url="api/IasnMstTeam/CreateTeam";
             lockUI();
             SocketService.post(url,params).then(function (resp) {
                unlockUI();
               if(resp.data.status==true){
                   $state.go('app.iasnMstTeamManagement');
                Notify.alert(resp.data.message,'success');
               }
               else{
                Notify.alert(resp.data.message,'warning');
               }
            });
         }

         $scope.Back=function(){
             $state.go('app.iasnMstTeamManagement');
         }

        

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstTeamManagement', iasnMstTeamManagement);

        iasnMstTeamManagement.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnMstTeamManagement($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnMstTeamManagement';

        activate();

        function activate() {
            $scope.totalDisplayed=100;
            $scope.total = 0;

            var url = "api/IasnMstTeam/TeamSummary";
            SocketService.get(url).then(function (resp) {
                unlockUI();
               
                $scope.teammgmt_list = resp.data.MdlTeamSummary;
                $scope.total = $scope.teammgmt_list.length;
            });

         }

         $scope.loadMore= function (pagecount) {
          
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();
        
            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {
               
                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if($scope.total<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };

      
        // Add Team Code Ends

        $scope.addTeam = function () {
            
            $state.go('app.iasnMstTeamAdd');
        }

        $scope.EditTeam = function (val) {
            localStorage.setItem('team_gid', val);

            $state.go('app.iasnMstTeamEdit');
        }

    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstZonalMapping', iasnMstZonalMapping);

        iasnMstZonalMapping.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnMstZonalMapping($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'iasnMstZonalMapping';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/IasnMstZone/ZoneSummary";
            SocketService.get(url).then(function (resp) {
                $scope.ZoneSummary = resp.data.MdlZoneSummary;
                unlockUI();
                if ($scope.ZoneSummary == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.ZoneSummary.length;
                    if ($scope.ZoneSummary.length < 100) {
                        $scope.totalDisplayed = $scope.ZoneSummary.length;
                    }
                }
                                
            });
        }

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };


        $scope.addZone = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/addZone.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {

                var url = "api/IasnMstZone/Employee";
                SocketService.get(url).then(function (resp) {
                   $scope.employee_list=resp.data.employee_list;
                });

                $scope.rdb_acks = '';

                $scope.Add = function () {
                    var params = {
                        zone_name: $scope.zoneName,
                        employee_gid: $scope.rmlist,
                        employee_name: $('#rmlist :selected').text(),
                        acknowledgement_flag: $scope.rdb_acks
                    }
                    lockUI();
                    var url = "api/IasnMstZone/PostRMName";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            modalInstance.close('closed');
                            activate();
                        }
                        var params = {
                            zone_name: $scope.zoneName
                        }
                        var url = 'api/IasnMstZone/RMStatusSummary';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.rmstatus_list = resp.data.MdlRMStatusSummary;
                            if ($scope.rmstatus_list != null) {
                                $scope.rmlist_status = true;
                            }
                            else {
                                $scope.rmlist_status = false;
                            }
                        });
                        var url = "api/IasnMstZone/Employee";
                        SocketService.get(url).then(function (resp) {
                            $scope.employee_list = resp.data.employee_list;
                        });
                        $scope.rmlist = '';
                        $scope.rdb_acks = '';
                    });
                }
                $scope.Submit = function () {
                    var params = {
                        zone_name: $scope.zoneName
                    }
                    lockUI();
                    var url = "api/IasnMstZone/CreateZone";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            modalInstance.close('closed');
                            activate();
                        }
                    });
                }

                $scope.deleteRM = function (val3) {
                    var zone_name = $scope.zoneName;
                    var params = {
                        employee_gid: val3,
                        zone_name : zone_name
                    };
                    var url = 'api/IasnMstZone/RM_Delete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            var url = 'api/IasnMstZone/RMStatusSummary';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.rmstatus_list = resp.data.MdlRMStatusSummary;
                                if ($scope.rmstatus_list != null) {
                                    $scope.rmlist_status = true;
                                }
                                else {
                                    $scope.rmlist_status = false;
                                }
                            });
                            var url = "api/IasnMstZone/Employee";
                            SocketService.get(url).then(function (resp) {
                                $scope.employee_list = resp.data.employee_list;
                            });
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                    activate();
                };
            }
        }

        $scope.editZone=function(val,val1)
        {
            var modalInstance = $modal.open({
                templateUrl: '/editZone.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
               
                var params = {
                    zone_gid: val
                }
                var url = 'api/IasnMstZone/EditZone';
                SocketService.getparams(url, params).then(function (resp) {
                   $scope.zoneNameEdit=resp.data.zone_name;
                    $scope.zoneref_code = resp.data.zoneref_no;
                    $scope.rm_listEdit = '';
                    $scope.rdb_acksedit = '';
                });
                var url = "api/IasnMstZone/Employee";
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });
                var params = {
                    zone_name: val1
                }
                var url = 'api/IasnMstZone/RMStatusSummary';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rmstatus_list = resp.data.MdlRMStatusSummary;
                });
               

                $scope.Update = function () {
                    lockUI();
                    var params = {
                        zone_gid: val,
                        zone_name: val1,
                        employee_gid: $scope.rm_listEdit,
                        employee_name: $('#rm_listEdit :selected').text(),
                        acknowledgement_flag: $scope.rdb_acksedit                      
                    }
                    var url = "api/IasnMstZone/UpdateZone";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                           
                        }
                        var params = {
                            zone_name: $scope.zoneNameEdit
                        }
                        var url = 'api/IasnMstZone/RMStatusSummary';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.rmstatus_list = resp.data.MdlRMStatusSummary;
                        });
                        var url = "api/IasnMstZone/Employee";
                        SocketService.get(url).then(function (resp) {
                            $scope.employee_list = resp.data.employee_list;
                        });
                        $scope.rm_listEdit = '';
                        $scope.rdb_acksedit = '';
                    });
              
                }

                $scope.deleteRM = function (val3) {
                    var zone_name = val1
                    var params = {
                        employee_gid: val3,
                        zone_name: zone_name
                    };
                    var url = 'api/IasnMstZone/RM_Delete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            var url = 'api/IasnMstZone/RMStatusSummary';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.rmstatus_list = resp.data.MdlRMStatusSummary;
                            });
                            var url = "api/IasnMstZone/Employee";
                            SocketService.get(url).then(function (resp) {
                                $scope.employee_list = resp.data.employee_list;
                            });
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
        
        $scope.showPopover = function (zone_gid, zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showpopupModal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            lockUI();
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    zone_gid: zone_gid
                }
                var url = 'api/IasnMstZone/EditZone';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.zone_name = zone_name;
                    $scope.MdlRmList = resp.data.MdlRmList;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.deleteZone = function (val) {
            var params = {
                zone_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Zone ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/IasnMstZone/Zone_Delete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                        }
                        else {
                            alert(resp.data.message, {
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
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnAllotedSummary', iasnTrnAllotedSummary);

    iasnTrnAllotedSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnTrnAllotedSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnAllotedSummary';

        activate();

        function activate() {
            lockUI();
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummary')
            }
            $scope.page = localStorage.getItem('page');
            var url = 'api/IasnTrnWorkItem/WorkItemCounts';
            SocketService.get(url).then(function (resp) {
                $scope.count_workitempending = resp.data.count_workitempending;
                $scope.count_workitemassigned = resp.data.count_workitemassigned;
                $scope.count_pushback = resp.data.count_pushback;
                $scope.count_forward = resp.data.count_forward;
                $scope.count_close = resp.data.count_close;
                $scope.count_archival = resp.data.count_archival;
                $scope.count_composemail = resp.data.count_composemail;


            });
            var url = 'api/IasnTrnWorkItem/WorkItemPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });

            var url = 'api/IasnTrnWorkItem/WorkItemSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemAllotted_List = resp.data.MdlWorkItem;
                unlockUI();
                if ($scope.WorkItemAllotted_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemAllotted_List.length;
                    if ($scope.WorkItemAllotted_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemAllotted_List.length;
                    }
                }
            });
        }

        $scope.EmployeeProfile = function (emp_gid) {
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params = {
                employee_gid: emp_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.user_code = resp.data.user_code;
                    $scope.user_name = resp.data.user_name;
                    $scope.user_photo = resp.data.user_photo;
                    $scope.user_designation = resp.data.user_designation;
                    $scope.user_department = resp.data.user_department;
                    $scope.user_mobileno = resp.data.user_mobileno;
                }
                else {
                    $scope.user_code = "-";
                    $scope.user_name = "-";
                    $scope.user_photo = "N";
                    $scope.user_designation = "-";
                    $scope.user_department = "-";
                }
            });

        }
        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };
        // Action Work Item 360
        $scope.WorkItem360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', $scope.page)
            }
            localStorage.setItem('page', 'Allotted')
            $state.go("app.iasnTrnWorkItem360");
        }

        // Action Work Item Allotted 360
        $scope.WorkItemAllotted360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', $scope.page)
            }

            $state.go("app.iasnTrnWorkItemAllotted360");
        }

       

        $scope.WorkItem = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });
        }

        $scope.Pushback = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemPushbackSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPushback_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPushback_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPushback_List.length;
                    if ($scope.WorkItemPushback_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPushback_List.length;
                    }
                }

            });
        }

        $scope.Forward = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemForwardSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemForward_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemForward_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemForward_List.length;
                    if ($scope.WorkItemForward_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemForward_List.length;
                    }
                }

            });
        }

        $scope.CloseTab = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemCloseSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemClose_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemClose_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemClose_List.length;
                    if ($scope.WorkItemClose_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemClose_List.length;
                    }
                }

            });
        }

       
        $scope.AssignZone = function (email_gid, workitemref_no, email_from, email_subject, zone_gid, zone_name) {

            var modalInstance = $modal.open({
                templateUrl: '/assignZoneContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });


            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;

                $scope.rdb_acks = "N";
                var url = "api/IasnMstZone/ZoneSummary";
                SocketService.get(url).then(function (resp) {

                    $scope.zone_list = resp.data.MdlZoneSummary;

                });
                //if(zone_gid==undefined || zone_gid==""){
                //    $scope.zone_flag="N"

                //}
                //else{
                //    $scope.zone_name=zone_gid;
                //    $scope.lblzonename=zone_name;
                //    $scope.zone_flag="Y"
                //}

                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;

                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.AssignToUpdate = function () {

                   
                    var params = {
                        email_gid: email_gid,
                        zone_gid: $scope.zone_name,
                        zone_name: $('#zone_name :selected').text(),
                        zone_flag: $scope.zone_flag,
                        acknowledgement_flag: $scope.rdb_acks,
                    }

                    var url = "api/IasnTrnWorkItem/AssignZone";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });

                }
            }
        }

        $scope.TransferWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name, assign_to) {

            var modalInstance = $modal.open({
                templateUrl: '/transferWIContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });


            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;
                $scope.zone_name = zone_name;
                $scope.checkeremployee_name = assign_to;

                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;

                });

                var params = {
                    lsemail_gid: email_gid
                }
                var url = 'api/IasnTrnWorkItem/TransferLog';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        $scope.showtransfer = true;
                    }
                    else {
                        $scope.showtransfer = false;
                    }


                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.transferWIUpdate = function () {

                    if ($scope.transfer_to == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        email_gid: email_gid,
                        employee_gid: $scope.transfer_to,
                        employee_name: $('#transfer_to :selected').text(),
                        zone_gid: '',
                        zone_name: '',
                        zone_flag: 'Y'
                    }

                    var url = "api/IasnTrnWorkItem/AssignTo";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });

                }
            }
        }

        $scope.CloseWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/closeWIContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;

                $scope.CloseWIUpdate = function () {

                    var params = {
                        email_gid: email_gid,
                        decision: 'Close',
                        employee_gid: '',
                        employee_name: '',
                        remarks: $scope.close_remarks,
                        close_acknowledge: $scope.Acknowledge_mail_trigger,
                        mailcontent: 'Close',
                        customer_gid: '',
                        customer_name: '',
                        subject: '',
                        tomail_id: '',
                        ccmail_id: '',
                        bccmail_id: ''
                    }


                    var url = 'api/IasnTrnWorkItem/PostDecision';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, 'success')
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning')
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
        // Add Work Item Code Ends

        // Merge Work Item Code Starts
        $scope.mergeworkitem = function (email_gid, subject, ref_no) {
            localStorage.setItem('email_gid', email_gid);
            localStorage.setItem('email_subject', subject);
            localStorage.setItem('workitemref_no', ref_no);

            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', 'workitemsummarypage')
            }

            $state.go('app.iasnWomMergeWorkItem');
        }
        // Merge Work Item Code Ends
        $scope.Alloted = function () {
            $state.go('app.iasnTrnAllotedSummary');
        }
        $scope.Pushback = function () {
            $state.go('app.iasnTrnPushbackSummary');
        }
        $scope.Forward = function () {
            $state.go('app.iasnTrnForwardSummary');
        }
        $scope.Close = function () {
            $state.go('app.iasnTrnCloseSummary');
        }
        $scope.WorkItem = function () {
            $state.go('app.iasnTrnWorkItemSummary');
        }
        $scope.ComposeMail = function () {
            $state.go('app.iasnWomWorkOrderSummary');
        }

        $scope.Allottedcount_dtls = function () {
            var modalInstance = $modal.open({
                templateUrl: '/AllottedCountdtl.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
                 var url = 'api/IasnTrnWorkItem/AssignedCountList';
               lockUI();
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.assignedcount_list = resp.data.MdlAssignedList;
    
               });  
    
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

               
            }
          
        }

        $scope.holdworkitem = function (email_gid, workitemref_no, email_from, email_subject) {
            var modalInstance = $modal.open({
                templateUrl: '/holdworkitempopup.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false,
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.email_gid = email_gid;
                $scope.workitemref_no = workitemref_no;
                $scope.from = email_from;
                $scope.email_subject = email_subject;

                var params = {
                    lsemail_gid: email_gid,
                    assigned_flag: 'Y',
                }
                var url = 'api/IasnTrnWorkItem/HoldLogDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.holdlog_list = resp.data.MdlholdLog;
                        //$scope.showholdworkitem = true;
                    }
                    //else {
                    //    $scope.showholdworkitem = false;
                    //}
                });

                $scope.HoldWISubmit = function () {
                    var params = {
                        email_gid: email_gid,
                        workitemhold_reason: $scope.hold_remarks,
                        assigned_flag: 'Y',
                    }
                    var url = "api/IasnTrnWorkItem/HoldWorkItem";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            })
                            $modalInstance.close('closed');
                        }
                    });
                }

                $scope.close = function () {
                    $modalInstance.close('closed');
                };
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnArchivalSummary', iasnTrnArchivalSummary);

        iasnTrnArchivalSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnTrnArchivalSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnArchivalSummary';

        activate();

        function activate() {
            $scope.PnlSpecific = false;
            $scope.archival={}
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total=100;
            $scope.totalDisplayed=0;  
            $scope.page = localStorage.getItem('page');

            var url = 'api/IasnTrnWorkItem/ArchivalCounts';
            SocketService.get(url).then(function (resp) {
                $scope.workitem_count = resp.data.workitem_count;
                $scope.archivalcustomer_count=resp.data.archivalcustomer_count;
                $scope.archivalspecific_count = resp.data.archivalspecific_count;
                   
                
            });


            var url = 'api/IasnTrnWorkItem/WorkItemArchivalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItem_List = resp.data.MdlWorkItem;
                //if ($scope.WorkItem_List == null) {
                //    $scope.total = 0;
                //    $scope.totalDisplayed = 0;
                //}
                //else {
                //    $scope.total = $scope.WorkItem_List.length;
                //    if ($scope.WorkItem_List.length < 100) {
                //        $scope.totalDisplayed = $scope.WorkItem_List.length;
                //    }
                //}
            });
        }

        $scope.workitem = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemArchivalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItem_List = resp.data.MdlWorkItem;
                //if ($scope.WorkItem_List == null) {
                //    $scope.total = 0;
                //    $scope.totalDisplayed = 0;
                //}
                //else {
                //    $scope.total = $scope.WorkItem_List.length;
                //    if ($scope.WorkItem_List.length < 100) {
                //        $scope.totalDisplayed = $scope.WorkItem_List.length;
                //    }
                //}
            });
        }

        $scope.archivalcustomer = function () {

            var url = 'api/IasnTrnWorkItem/CustomerArchival';
            SocketService.get(url).then(function (resp) {
                $scope.ArchivalCustomer_list = resp.data.MdlArchivalCustomer;
                //if ($scope.ArchivalCustomer_list == null) {
                //    $scope.total = 0;
                //    $scope.totalDisplayed = 0;
                //}
                //else {
                //    $scope.total = $scope.ArchivalCustomer_list.length;
                //    if ($scope.ArchivalCustomer_list.length < 100) {
                //        $scope.totalDisplayed = $scope.ArchivalCustomer_list.length;
                //    }
                //}
                
            });

        }
        $scope.onclickspecific=function(){
            $scope.PnlSpecific = true;
            $scope.archival.customer = '';
            $scope.archival.cbosanctionrefno = '';
        }
        $scope.onclickcustomer = function () {
            $scope.PnlSpecific = false;
            $scope.archival.customer = '';
            
        }
        $scope.archivalspecific = function () {
            var url = 'api/IasnTrnWorkItem/SpecificArchival';
            SocketService.get(url).then(function (resp) {
                $scope.SpecificArchival_List = resp.data.MdlArchivalCustomer;
                //if ($scope.SpecificArchival_List == null) {
                //    $scope.total = 0;
                //    $scope.totalDisplayed = 0;
                //}
                //else {
                //    $scope.total = $scope.SpecificArchival_List.length;
                //    if ($scope.SpecificArchival_List.length < 100) {
                //        $scope.totalDisplayed = $scope.SpecificArchival_List.length;
                //    }
                //}
            });
        }

        $scope.createArchival = function () {
            var email_gid;
            angular.forEach($scope.WorkItem_List, function (val) {

                if (val.checked == true) {
                    email_gid = val.email_gid;

                   
                }
            });
            if (email_gid == undefined)
                {
                Notify.alert('Select Atleast One Record!')
            }
            else {
                $scope.IsCreate = true;
                $scope.archival.types_of_archival="Customer"
            }
        }

        $scope.complete=function(string){
            
            if(string.length >=3){
             $scope.message="";
             var url = 'api/customer/ExploreCustomer';
             var params={
                  customername:string 
              }
              SocketService.getparams(url,params).then(function (resp) {
                  if(resp.data.status==true){
                     $scope.message="";
                     $scope.customer_list = resp.data.Customers;
                  }
                  else{
                      $scope.customer="";
                     $scope.message="No Records";
                  }
                 
                 
          });
    }
    else{
     $scope.customer_list=null;
        $scope.message="Type atleast three character";
    }
         }

       $scope.fillTextbox=function(customer_gid,customer_name){
   
       $scope.archival.customer=customer_name;
       $scope. customer_gid=customer_gid;
       $scope.customer_list=null;

       var params = {
        customer_gid: customer_gid
    }

   
    var url = 'api/loan/customer_getheads';

    SocketService.getparams(url, params).then(function (resp) {
      
        $scope.sanctiondtl = resp.data.sanctiondtl;
       
    });
        }

        $scope.close=function(){
            $scope.IsCreate = false;
        }

        $scope.ArchivalSubmit=function(){
            var WorkItem_List = [];
            var email_gid;
            var sanctionref_no='';
            var sanction_gid='';
            angular.forEach($scope.WorkItem_List, function (val) {

                if (val.checked == true) {
                    email_gid = val.email_gid;
                    
                    WorkItem_List.push(email_gid);

                }
            });
            if($scope.archival.types_of_archival=='Specific'){
                if($scope.archival.cbosanctionrefno == undefined){
                  
                    Notify.alert('Select the Sanction Ref No.','warning');
                    return;
                  }
                  else{
                    sanctionref_no=$scope.archival.cbosanctionrefno.sanctionrefno;
                    sanction_gid=$scope.archival.cbosanctionrefno.sanction_Gid;
                }
            }
            
           
            var params={
                email_gid: WorkItem_List,
                archival_type:$scope.archival.types_of_archival,
                remarks: $scope.archival.Remarks,
                customer_gid:$scope.customer_gid,
                customer_name:$scope.archival.customer,
                sanctionref_no:sanctionref_no,
                sanction_gid:sanction_gid
            }

            if (email_gid != undefined) {
                var url = 'api/IasnTrnWorkItem/PostArchival';
                lockUI()
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI()
                        Notify.alert(resp.data.message, 'success')



                    }
                    else {
                        unlockUI();

                        Notify.alert(resp.data.message)
                    }
                  
                    activate();


                });
            }
            else {
                Notify.alert('Select Atleast One Record!')
            }
        }
        $scope.checkall = function (selected) {
            angular.forEach($scope.WorkItem_List, function (val) {  
                
                    val.checked = selected;
            });
        }

      
        //$scope.loadMore = function (pagecount) {
        //    if (pagecount == undefined) {
        //        Notify.alert("Enter the Total Summary Count", "warning");
        //        return;
        //    }
        //    lockUI();

        //    var Number = parseInt(pagecount);
        //    // new code start
        //    if ($scope.total != 0) {

        //        if (pagecount < $scope.total) {
        //            $scope.totalDisplayed += Number;
        //            if ($scope.total < $scope.totalDisplayed) {
        //                $scope.totalDisplayed = $scope.total;
        //                Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
        //            }
        //            unlockUI();
        //        }
        //        else {
        //            unlockUI();
        //            Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
        //            return;
        //        }
        //    }
        //    // new code end
        //    unlockUI();
        //};
       
        $scope.WorkItem360 = function (val) {
            localStorage.setItem('email_gid', val)
            if ($scope.page ==undefined)
            {
                localStorage.setItem('page','archival')
            }
            else{
                localStorage.setItem('page','archival')
            }
            localStorage.setItem('page' , 'Archival')
            $state.go("app.iasnTrnWorkItem360");
        }

        $scope.WorkItemSummary = function (val1, val2,val3,val4) {
            localStorage.setItem('customer_gid', val1)
            localStorage.setItem('type', val2)
            // localStorage.setItem("CustomerName",val3)
            // localStorage.setItem("SanctionRefNo",val4)
            $state.go('app.iasnTrnCustomerWrkSummary');
        }
      
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnCloseSummary', iasnTrnCloseSummary);

    iasnTrnCloseSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnTrnCloseSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnCloseSummary';

        activate();

        function activate() {
            lockUI();
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummary')
            }
            $scope.page = localStorage.getItem('page');
            var url = 'api/IasnTrnWorkItem/WorkItemCounts';
            SocketService.get(url).then(function (resp) {
                $scope.count_workitempending = resp.data.count_workitempending;
                $scope.count_workitemassigned = resp.data.count_workitemassigned;
                $scope.count_pushback = resp.data.count_pushback;
                $scope.count_forward = resp.data.count_forward;
                $scope.count_close = resp.data.count_close;
                $scope.count_archival = resp.data.count_archival;
                $scope.count_composemail = resp.data.count_composemail;

            });
                      
            var url = 'api/IasnTrnWorkItem/WorkItemCloseSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemClose_List = resp.data.MdlWorkItem;
                unlockUI();
                if ($scope.WorkItemClose_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemClose_List.length;
                    if ($scope.WorkItemClose_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemClose_List.length;
                    }
                }

            });
        }

        $scope.EmployeeProfile = function (emp_gid) {
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params = {
                employee_gid: emp_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.user_code = resp.data.user_code;
                    $scope.user_name = resp.data.user_name;
                    $scope.user_photo = resp.data.user_photo;
                    $scope.user_designation = resp.data.user_designation;
                    $scope.user_department = resp.data.user_department;
                    $scope.user_mobileno = resp.data.user_mobileno;
                }
                else {
                    $scope.user_code = "-";
                    $scope.user_name = "-";
                    $scope.user_photo = "N";
                    $scope.user_designation = "-";
                    $scope.user_department = "-";
                }
            });

        }
        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };
        // Action Work Item 360
        $scope.WorkItem360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', $scope.page)
            }
            localStorage.setItem('page', 'Close')
            $state.go("app.iasnTrnWorkItem360");
        }

        // Action Work Item Allotted 360
        $scope.WorkItemAllotted360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', $scope.page)
            }

            $state.go("app.iasnTrnWorkItemAllotted360");
        }



        $scope.WorkItem = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });
        }

        $scope.Pushback = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemPushbackSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPushback_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPushback_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPushback_List.length;
                    if ($scope.WorkItemPushback_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPushback_List.length;
                    }
                }

            });
        }



        $scope.AssignZone = function (email_gid, workitemref_no, email_from, email_subject, zone_gid, zone_name) {

            var modalInstance = $modal.open({
                templateUrl: '/assignZoneContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });


            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;

                $scope.rdb_acks = "N";
                var url = "api/IasnMstZone/ZoneSummary";
                SocketService.get(url).then(function (resp) {

                    $scope.zone_list = resp.data.MdlZoneSummary;

                });
                //if(zone_gid==undefined || zone_gid==""){
                //    $scope.zone_flag="N"

                //}
                //else{
                //    $scope.zone_name=zone_gid;
                //    $scope.lblzonename=zone_name;
                //    $scope.zone_flag="Y"
                //}

                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;

                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.AssignToUpdate = function () {


                    var params = {
                        email_gid: email_gid,
                        zone_gid: $scope.zone_name,
                        zone_name: $('#zone_name :selected').text(),
                        zone_flag: $scope.zone_flag,
                        acknowledgement_flag: $scope.rdb_acks,
                    }

                    var url = "api/IasnTrnWorkItem/AssignZone";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });

                }
            }
        }

        $scope.TransferWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name, assign_to) {

            var modalInstance = $modal.open({
                templateUrl: '/transferWIContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });


            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;
                $scope.zone_name = zone_name;
                $scope.checkeremployee_name = assign_to;

                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;

                });

                var params = {
                    lsemail_gid: email_gid
                }
                var url = 'api/IasnTrnWorkItem/TransferLog';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        $scope.showtransfer = true;
                    }
                    else {
                        $scope.showtransfer = false;
                    }


                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.transferWIUpdate = function () {

                    if ($scope.transfer_to == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        email_gid: email_gid,
                        employee_gid: $scope.transfer_to,
                        employee_name: $('#transfer_to :selected').text(),
                        zone_gid: '',
                        zone_name: '',
                        zone_flag: 'Y'
                    }

                    var url = "api/IasnTrnWorkItem/AssignTo";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });

                }
            }
        }

        $scope.CloseWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/closeWIContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;

                $scope.CloseWIUpdate = function () {

                    var params = {
                        email_gid: email_gid,
                        decision: 'Close',
                        employee_gid: '',
                        employee_name: '',
                        remarks: $scope.close_remarks,
                        close_acknowledge: $scope.Acknowledge_mail_trigger,
                        mailcontent: 'Close',
                        customer_gid: '',
                        customer_name: '',
                        subject: '',
                        tomail_id: '',
                        ccmail_id: '',
                        bccmail_id: ''
                    }


                    var url = 'api/IasnTrnWorkItem/PostDecision';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, 'success')
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning')
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
        // Add Work Item Code Ends

        // Merge Work Item Code Starts
        $scope.mergeworkitem = function (email_gid, subject, ref_no) {
            localStorage.setItem('email_gid', email_gid);
            localStorage.setItem('email_subject', subject);
            localStorage.setItem('workitemref_no', ref_no);

            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', 'workitemsummarypage')
            }

            $state.go('app.iasnWomMergeWorkItem');
        }
        // Merge Work Item Code Ends
        $scope.Alloted = function () {
            $state.go('app.iasnTrnAllotedSummary');
        }
        $scope.Pushback = function () {
            $state.go('app.iasnTrnPushbackSummary');
        }
        $scope.Forward = function () {
            $state.go('app.iasnTrnForwardSummary');
        }
        $scope.Close = function () {
            $state.go('app.iasnTrnCloseSummary');
        }
        $scope.WorkItem = function () {
            $state.go('app.iasnTrnWorkItemSummary');
        }
        $scope.ComposeMail = function () {
            $state.go('app.iasnWomWorkOrderSummary');
        }
    }
})();

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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnConsolidatedReport', iasnTrnConsolidatedReport);

    iasnTrnConsolidatedReport.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function iasnTrnConsolidatedReport($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnConsolidatedReport';

        activate();

        function activate() {
            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            lockUI();
            var url = 'api/IasnTrnWorkItem/GetConsolidatedReport';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.WorkItem_List = resp.data.MdlWorkItem;
            });
        }

        $scope.view = function (val) {
            $location.url('app/isanconsolidatedview?lsemail_gid=' + val + '&lstab=ConsolidatedReport')
        }

        $scope.export = function () {
            if ($scope.emailfrom_date == undefined || $scope.emailfrom_date == "") {
                Notify.alert("Kindly Select From and To date", 'warning');
            }
            else if ($scope.emailto_date == undefined || $scope.emailto_date == "") {
                Notify.alert("Kindly Select From and To date", 'warning');
            }
            else {
                lockUI();
                var emailfrom_date1 = $scope.emailfrom_date;

                var emailfrom_date = new Date(emailfrom_date1.getTime() - (emailfrom_date1.getTimezoneOffset() * 60000)).toISOString().split("T")[0];
                var emailto_date1 = $scope.emailto_date;

                var emailto_date = new Date(emailto_date1.getTime() - (emailto_date1.getTimezoneOffset() * 60000)).toISOString().split("T")[0];

                var url = 'api/IasnTrnWorkItem/GetConsolidatedReportExcel';
                var param = {
                    emailfrom_date: emailfrom_date,
                    emailto_date: emailto_date
                }
                SocketService.getparams(url, param).then(function (resp) {
///if (resp.data.status == true) {
                        unlockUI();
                        DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.excel_name);
                       /* var phyPath = resp.data.excel_path;
                        var relPath = phyPath.split("EMS");
                        var relpath1 = relPath[1].replace("\\", "/");
                        var hosts = window.location.host;
                        var prefix = location.protocol + "//";
                        var str = prefix.concat(hosts, relpath1);
                        var link = document.createElement("a");
                        var name = resp.data.excel_name.split('.');
                        link.download = name[0];
                        var uri = str;
                        link.href = uri;
                        link.click();*/
                    
                 
                });
            }

        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnCustomerWrkSummary', iasnTrnCustomerWrkSummary);

    iasnTrnCustomerWrkSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce'];

    function iasnTrnCustomerWrkSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        var lscustomer_gid;
        var lstype;
        var lsCustomerName;
        var SanctionRefNo;
        vm.title = 'iasnTrnCustomerWrkSummary';

        activate();

        function activate() {
            lscustomer_gid = localStorage.getItem('customer_gid')
            lstype = localStorage.getItem('type')
            // lsCustomerName= localStorage.getItem('CustomerName')
            // SanctionRefNo = localStorage.getItem('SanctionRefNo')
            
            var params=
                {
                    customer_gid:lscustomer_gid,
                    archival_type: lstype
                }
            var url = 'api/IasnTrnWorkItem/WorkItemArchivalSummary';
            SocketService.post(url, params).then(function (resp) {
                $scope.TaggedWI_List = resp.data.MdlWorkItem;
                $scope.workitemref_no = resp.data.workitemref_no;
                $scope.updatedby_on = resp.data.updatedby_on;            

            });
         

        }
        $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
        $scope.back = function()
        {
            $state.go('app.iasnTrnArchivalSummary')
        }

        $scope.EmployeeProfile = function (emp_gid) {
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params = {
                employee_gid: emp_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.user_code = resp.data.user_code;
                    $scope.user_name = resp.data.user_name;
                    $scope.user_photo = resp.data.user_photo;
                    $scope.user_designation = resp.data.user_designation;
                    $scope.user_department = resp.data.user_department;
                    $scope.user_mobileno =resp.data.user_mobileno;
                }
                else {
                    $scope.user_code = "-";
                    $scope.user_name = "-";
                    $scope.user_photo = "N";
                    $scope.user_designation = "-";
                    $scope.user_department = "-";
                }
            });

        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnForwardMail', iasnTrnForwardMail);

    iasnTrnForwardMail.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$timeout', '$window','DownloaddocumentService'];

    function iasnTrnForwardMail($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $timeout, $window, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnForwardMail';
        var lsdecision;
        $scope.lsShowBCC = true;
        $scope.lsShowCC = true;
        activate();

        function activate() {
            $scope.lspage = localStorage.getItem('lspage');
            $scope.composemail_gid = localStorage.getItem('composemail_gid');
            $scope.toMail = localStorage.getItem('toMail');
            $scope.ccMail = localStorage.getItem('ccMail');
            $scope.bccMail = localStorage.getItem('bccMail');
            $scope.email_subject = localStorage.getItem('email_subject');

            var url = 'api/IasnTrnWorkItem/Mailtempdelete';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/IasnTrnEmailSignature/GetEmailSignature';
            SocketService.get(url).then(function (resp) {

                $scope.pushbackcontent = resp.data.emailsignature;

            });
        }
      
        $scope.updateDesicion = function () {

            if ($scope.pushbackcontent == undefined) {
                Notify.alert('Write the body of the content', 'success');
                return;
            }

            var params = {
                composemail_gid: $scope.composemail_gid,
                mailcontent: $scope.pushbackcontent,
                email_subject: $scope.email_subject,
                tomail_id: $scope.toMail,
                ccmail_id: $scope.ccMail,
                bccmail_id: $scope.bcc_mail,
                status:"Forward"
            }
            console.log(params);
            var url = 'api/IasnTrnWorkItem/ComposeMailDecision';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, 'success')
                    if ($scope.lspage == "Archival") {
                        $state.go("app.iasnTrnArchivalSummary");
                    }
                    else if ($scope.lspage == "composemailsummary") {
                        $state.go("app.iasnWomWorkOrderSummary");
                    }
                    else if ($scope.lspage == "composemail") {
                        $state.go("app.iasnTrnComposeMail360");
                    }
                }
                else {

                    Notify.alert(resp.data.message, 'warning')
                    if ($scope.lspage == "composemail") {
                        $state.go("app.iasnTrnComposeMail360");
                    }
                    else if($scope.lspage == "composemailsummary") {
                        $state.go("app.iasnWomWorkOrderSummary");
                    }
                    else if ($scope.lspage == "Archival") {
                        $state.go("app.iasnTrnArchivalSummary");
                    }
                }
            });
        }

        $scope.uploadattachment = function () {
            var fi = document.getElementById('addupload');
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

                    $("#addupload").val('');
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

        //$scope.downloads = function (val1, val2) {

        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    var name = val2.split(".")
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.back = function () {
            if ($scope.lspage == "composemail")
            {
                $state.go("app.iasnTrnComposeMail360");
            }
            else if ($scope.lspage == "Archival")
            {
                $state.go("app.iasnTrnArchivalSummary");
            }
            else
            {
                $state.go("app.iasnWomWorkOrderSummary");
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnForwardSummary', iasnTrnForwardSummary);

    iasnTrnForwardSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnTrnForwardSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnForwardSummary';

        activate();
        lockUI();
        function activate() {
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummary')
            }
            $scope.page = localStorage.getItem('page');
            var url = 'api/IasnTrnWorkItem/WorkItemCounts';
            SocketService.get(url).then(function (resp) {
                $scope.count_workitempending = resp.data.count_workitempending;
                $scope.count_workitemassigned = resp.data.count_workitemassigned;
                $scope.count_pushback = resp.data.count_pushback;
                $scope.count_forward = resp.data.count_forward;
                $scope.count_close = resp.data.count_close;
                $scope.count_archival = resp.data.count_archival;
                $scope.count_composemail = resp.data.count_composemail;

            });
            var url = 'api/IasnTrnWorkItem/WorkItemPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });

            var url = 'api/IasnTrnWorkItem/WorkItemSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemAllotted_List = resp.data.MdlWorkItem;

                if ($scope.WorkItemAllotted_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemAllotted_List.length;
                    if ($scope.WorkItemAllotted_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemAllotted_List.length;
                    }
                }
            });
            var url = 'api/IasnTrnWorkItem/WorkItemForwardSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.WorkItemForward_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemForward_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemForward_List.length;
                    if ($scope.WorkItemForward_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemForward_List.length;
                    }
                }

            });
        }

        $scope.EmployeeProfile = function (emp_gid) {
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params = {
                employee_gid: emp_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.user_code = resp.data.user_code;
                    $scope.user_name = resp.data.user_name;
                    $scope.user_photo = resp.data.user_photo;
                    $scope.user_designation = resp.data.user_designation;
                    $scope.user_department = resp.data.user_department;
                    $scope.user_mobileno = resp.data.user_mobileno;
                }
                else {
                    $scope.user_code = "-";
                    $scope.user_name = "-";
                    $scope.user_photo = "N";
                    $scope.user_designation = "-";
                    $scope.user_department = "-";
                }
            });

        }
        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };
        // Action Work Item 360
        $scope.WorkItem360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', $scope.page)
            }
            localStorage.setItem('page', 'Forward')
            $state.go("app.iasnTrnWorkItem360");
        }

        // Action Work Item Allotted 360
        $scope.WorkItemAllotted360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', $scope.page)
            }

            $state.go("app.iasnTrnWorkItemAllotted360");
        }



        $scope.WorkItem = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });
        }

        $scope.Pushback = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemPushbackSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPushback_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPushback_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPushback_List.length;
                    if ($scope.WorkItemPushback_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPushback_List.length;
                    }
                }

            });
        }

       
        $scope.CloseTab = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemCloseSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemClose_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemClose_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemClose_List.length;
                    if ($scope.WorkItemClose_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemClose_List.length;
                    }
                }

            });
        }


        $scope.AssignZone = function (email_gid, workitemref_no, email_from, email_subject, zone_gid, zone_name) {

            var modalInstance = $modal.open({
                templateUrl: '/assignZoneContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });


            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;

                $scope.rdb_acks = "N";
                var url = "api/IasnMstZone/ZoneSummary";
                SocketService.get(url).then(function (resp) {

                    $scope.zone_list = resp.data.MdlZoneSummary;

                });
                //if(zone_gid==undefined || zone_gid==""){
                //    $scope.zone_flag="N"

                //}
                //else{
                //    $scope.zone_name=zone_gid;
                //    $scope.lblzonename=zone_name;
                //    $scope.zone_flag="Y"
                //}

                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;

                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.AssignToUpdate = function () {


                    var params = {
                        email_gid: email_gid,
                        zone_gid: $scope.zone_name,
                        zone_name: $('#zone_name :selected').text(),
                        zone_flag: $scope.zone_flag,
                        acknowledgement_flag: $scope.rdb_acks,
                    }

                    var url = "api/IasnTrnWorkItem/AssignZone";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });

                }
            }
        }

        $scope.TransferWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name, assign_to) {

            var modalInstance = $modal.open({
                templateUrl: '/transferWIContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });


            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;
                $scope.zone_name = zone_name;
                $scope.checkeremployee_name = assign_to;

                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;

                });

                var params = {
                    lsemail_gid: email_gid
                }
                var url = 'api/IasnTrnWorkItem/TransferLog';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        $scope.showtransfer = true;
                    }
                    else {
                        $scope.showtransfer = false;
                    }


                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.transferWIUpdate = function () {

                    if ($scope.transfer_to == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        email_gid: email_gid,
                        employee_gid: $scope.transfer_to,
                        employee_name: $('#transfer_to :selected').text(),
                        zone_gid: '',
                        zone_name: '',
                        zone_flag: 'Y'
                    }

                    var url = "api/IasnTrnWorkItem/AssignTo";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });

                }
            }
        }

        $scope.CloseWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/closeWIContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;

                $scope.CloseWIUpdate = function () {

                    var params = {
                        email_gid: email_gid,
                        decision: 'Close',
                        employee_gid: '',
                        employee_name: '',
                        remarks: $scope.close_remarks,
                        close_acknowledge: $scope.Acknowledge_mail_trigger,
                        mailcontent: 'Close',
                        customer_gid: '',
                        customer_name: '',
                        subject: '',
                        tomail_id: '',
                        ccmail_id: '',
                        bccmail_id: ''
                    }


                    var url = 'api/IasnTrnWorkItem/PostDecision';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, 'success')
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning')
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
        // Add Work Item Code Ends

        // Merge Work Item Code Starts
        $scope.mergeworkitem = function (email_gid, subject, ref_no) {
            localStorage.setItem('email_gid', email_gid);
            localStorage.setItem('email_subject', subject);
            localStorage.setItem('workitemref_no', ref_no);

            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', 'workitemsummarypage')
            }

            $state.go('app.iasnWomMergeWorkItem');
        }
        // Merge Work Item Code Ends
        $scope.Alloted = function () {
            $state.go('app.iasnTrnAllotedSummary');
        }
        $scope.Pushback = function () {
            $state.go('app.iasnTrnPushbackSummary');
        }
        $scope.Forward = function () {
            $state.go('app.iasnTrnForwardSummary');
        }
        $scope.Close = function () {
            $state.go('app.iasnTrnCloseSummary');
        }
        $scope.WorkItem = function () {
            $state.go('app.iasnTrnWorkItemSummary');
        }
        $scope.ComposeMail = function () {
            $state.go('app.iasnWomWorkOrderSummary');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnMyWorkItem360', iasnTrnMyWorkItem360);

        iasnTrnMyWorkItem360.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce'];

    function iasnTrnMyWorkItem360($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,$sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnMyWorkItem360';
        activate();

        function activate() {
            $scope.PnlSpecific = false;
            $scope.IsVisibleteam=false;
            $scope.IsVisibleemployee=false;
            $scope.pushback=false;
            $scope.forward=false;
            $scope.all=false;
            $scope.archival=false;
            $scope.typeE = "";
            $scope.logdetails = false;
            $scope.email_gid = localStorage.getItem('email_gid');
            $scope.page = localStorage.getItem('page');

            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
               
            });

            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });

            var url = 'api/IasnTrnWorkItem/WorkItemView';
            var params={
                lsemail_gid:localStorage.getItem("email_gid")
            };
         
            SocketService.getparams(url,params).then(function (resp) {
                $scope.workitemref_no = resp.data.workitemref_no;
                $scope.email_from = resp.data.email_from;
                $scope.email_date=resp.data.email_date;
                $scope.email_subject=resp.data.email_subject;
                $scope.email_content=resp.data.email_content;
                $scope.created_date=resp.data.created_date;
                $scope.cc_mail=resp.data.cc;
                $scope.bcc_mail=resp.data.bcc;
                $scope.to_mail=resp.data.email_to;
                $scope.zone_name=resp.data.zone_name;
                $scope.team_code=resp.data.team_code;
                $scope.team_name=resp.data.team_name;
                $scope.team_mailid=resp.data.team_mailid;
                $scope.description=resp.data.description;
                $scope.rmemployee_gid=resp.data.rmemployee_gid;
                $scope.rmemployee_name=resp.data.rmemployee_name;   
                $scope.rmemployee_mailid=resp.data.email_address;
                $scope.checkeremployee_name=resp.data.checkeremployee_name;
                $scope.attch_list=resp.data.MdlAttachmentList;
                $scope.status=resp.data.status;
                $scope.archivalremarks = resp.data.archivalremarks;
                $scope.Mail_Trigger = resp.data.Mail_Trigger;
                $scope.assigned_remarks = resp.data.assigned_remarks;
                $scope.originalmail_Subject = resp.data.originalmail_Subject;
                $scope.hold_flag = resp.data.hold_flag;
                $scope.workitemhold_reason = resp.data.workitemhold_reason;
                $scope.customer_name = resp.data.customer_name;
                $scope.customer_type = resp.data.customer_type;

                if ($scope.archivalremarks==''|| $scope.archivalremarks== null )
                {
                    $scope.archiverem= false;
                }
                else{
                    $scope.archiverem=true; 
                }
                $scope.closedremarks = resp.data.closedremarks;
                if ($scope.closedremarks==''|| $scope.closedremarks== null )
                {
                    $scope.closerem= false;
                }
                else{
                    $scope.closerem= true; 
                }
                $scope.allottedby_on=resp.data.allottedby_on;
                $scope.aging=resp.data.aging;
                $scope.status=resp.data.status;
                $scope.updatedby_on=resp.data.updatedby_on;
                $scope.message_id=resp.data.message_id;
                $scope.reference_id=resp.data.reference_id;
               
                if(resp.data.employee_gid !=null){
                   
                    $scope.assign_to=resp.data.employee_gid;
                   
                }
               

            });
            var params={
                email_gid:localStorage.getItem("email_gid")
            };
         
            var url="api/IasnTrnWorkItem/ReferenceMail";
            SocketService.getparams(url,params).then(function (resp) {
                if(resp.data.status==true){
                    $scope.referenceMail=resp.data.MdlReferenceMail;
                }
                else{

                }
            });

            // var params={
            //     email_gid:localStorage.getItem("email_gid")
            // };
         
            // var url="api/IasnTrnWorkItem/DecisionHistoryMail";
            // SocketService.getparams(url,params).then(function (resp) {
            //     if(resp.data.status==true){
            //         $scope.decisionHistoryMail=resp.data.MdlDecisionhistory;
            //     }
            //     else{

            //     }
            // });

        }
        $scope.logdetails=function(){

            if($scope.IsLogShow==true){
                $scope.IsLogShow=false; 
            }
            else{
                $scope.IsLogShow=true;
                
                var url = 'api/IasnTrnWorkItem/TransferLog';
        
                var params={
                    lsemail_gid:localStorage.getItem("email_gid")
                };
            
                SocketService.getparams(url,params).then(function (resp) {
                    if(resp.data.status==true){
                       
                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        if( $scope.transferlog_list  == null)
                        {
                            $scope.transfershow = true;
                        }
                        else{
                            $scope.transfershow = false;
                        }
                    }
                    else{
                       
                    }
                    
                });
        
                var url = 'api/IasnTrnAuditLog/AuditLog';
        
                var params={
                    email_gid:localStorage.getItem("email_gid")
                };            
                SocketService.getparams(url,params).then(function (resp) {
                    if(resp.data.status==true){
                        $scope.auditlog_list = resp.data.MdlAuditLog;
                    }
                    else{
                       
                    }
                    
                });
        
               
            }
            
        }
         $scope.export = function (path,attchment_name) {
         
           
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

$scope.uploadattachment = function (val,val1,name) {
    var item = {
        name: val[0].name,
        file: val[0]
    };
    var frm = new FormData();
    frm.append('fileupload', item.file);
    frm.append('file_name', item.name);
    frm.append('project_flag', "Default");
   
    

    $scope.uploadfrm = frm;
    var url = 'api/IasnTrnWorkItem/MailAttchment';
    lockUI();
    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

        $("#addupload").val('');
        $scope.txtdocument_title = '';
        console.log(resp.data);
        if (resp.data.status == true) {
            unlockUI();
            Notify.alert('Document Uploaded Successfully..!!', 'success')
           
            var url = 'api/IasnTrnWorkItem/MailAttchment';

            SocketService.get(url).then(function (resp) {
               
                $scope.uploaddocument = resp.data.MdlDocDetails;
              
            });
        }
        else {
            unlockUI();
            Notify.alert('File Format Not Supported!')

        }

    });

}

$scope.transferto_change=function(val){
    SweetAlert.swal({
        title: 'Are you sure?',
        text: 'Do you want to transfer the work item?',

        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Yes,Transfer it!',
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            lockUI();
            var params={
                email_gid:localStorage.getItem("email_gid"),
                employee_gid:val,
                employee_name:$('#transfer_to :selected').text()
            }
            var url="api/IasnTrnWorkItem/AssignTo";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
                    SweetAlert.swal('Work Item Transfered Successfully!');
                    
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
        else{
            SweetAlert.swal('Error Occured');
        }

    });
}

$scope.pushback=function(){
   
    localStorage.setItem('email_gid',localStorage.getItem("email_gid"));
    localStorage.setItem('toMail',$scope.rmemployee_mailid);
    localStorage.setItem('ccMail', $scope.cc_mail);
    localStorage.setItem('bccMail', $scope.bcc_mail);
    localStorage.setItem('email_subject',$scope.email_subject);
    localStorage.setItem('message_id',$scope.message_id);
    localStorage.setItem('reference_id',$scope.reference_id);
    localStorage.setItem('rmemployee_gid', $scope.rmemployee_gid);
    localStorage.setItem('rmemployee_name', $scope.rmemployee_name);
    localStorage.setItem('decision', 'Pushback');
    localStorage.setItem('originalmail_Subject', $scope.originalmail_Subject);
    localStorage.setItem('lspage','myworkitem');
    $state.go('app.composeMail');
  
}

$scope.forward=function(){
    $scope.ccMail=$scope.cc_mail;
  
   localStorage.setItem('email_gid',localStorage.getItem("email_gid"));
   localStorage.setItem('toMail',$scope.rmemployee_mailid);
   localStorage.setItem('ccMail', $scope.ccMail);
   localStorage.setItem('bccMail', $scope.bcc_mail);
   localStorage.setItem('email_subject',$scope.email_subject);
   localStorage.setItem('message_id',$scope.message_id);
   localStorage.setItem('reference_id',$scope.reference_id);
   localStorage.setItem('rmemployee_gid', $scope.rmemployee_gid);
    localStorage.setItem('rmemployee_name', $scope.rmemployee_name);
    localStorage.setItem('decision', 'Forward');
    localStorage.setItem('originalmail_Subject', $scope.originalmail_Subject);
    localStorage.setItem('lspage','myworkitem');
    $state.go('app.composeMail');

 
}


$scope.onchangecopy=function(val){
    SweetAlert.swal({
        title: 'Are you sure?',
        text: 'Do you want to move the work item to yors bin?',

        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Yes,Move it!',
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            lockUI();
            var params={
                email_gid:localStorage.getItem("email_gid"),
                employee_gid:null,
                employee_name:null
            }
            var url="api/IasnTrnWorkItem/AssignTo";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
                    SweetAlert.swal('Work Item Moved to Your Bin Successfully!');
                    
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
        else{
            SweetAlert.swal('Error Occured');
        }

    });
}
        $scope.assignto_change=function(val){
            var params={
                email_gid:localStorage.getItem("email_gid"),
                employee_gid:val,
                employee_name:$('#assign_to :selected').text()
            }
           
            var url="api/IasnTrnWorkItem/AssignTo";
            SocketService.post(url,params).then(function (resp) {
                if(resp.data.status=true){
                    Notify.alert(resp.data.message,'success');
                }
                else{
                    Notify.alert(resp.data.message,'warning');
                }
            });
               
        }
        $scope.updateDesicion=function(){
            var emp_gid;
            var emp_name;
            var customer_gid;
            var customer_name;
            if($scope.txtremarks==undefined){
                Notify.alert('Enter the Remarks','warning');
                return;
            }
            if($scope.decision=='Pushback'){
           
                emp_gid=$scope.rmemployee_gid;
                emp_name=$scope.rmemployee_name;
                
            }

            if($scope.decision=='Forward'){
                if($scope.forward_to==undefined){
                    Notify.alert('Select the forward to person','warning');
                    return;
                }
                emp_gid=$scope.forward_to;
                emp_name=$('#forward_to :selected').text();
            }

            if($scope.decision=='Archival'){
                if($scope.customer==undefined){
                    Notify.alert('Select the Customer','warning');
                    return;
                }
                emp_gid='';
                emp_name='';
                $scope.mailcontent='No Content';
            }

            if($scope.decision=='Close'){
                emp_gid='';
                emp_name='';
                $scope.mailcontent='No Content';
            }
            if($scope.customer==undefined){
                customer_gid='';
                customer_name='';
            }
            else{
                customer_gid=$scope.customer,
                customer_name=$('#customer :selected').text()
            }

            var params={
                email_gid:email_gid,
                decision:'Close',
                employee_gid:'',
                employee_name:'',
                remarks:$scope.txtremarks,
                mailcontent:'test',
                customer_gid:customer_gid,
                customer_name:customer_name,
                subject:$scope.pushback_subject,
                tomail_id:$scope.tomail_pushback,
                ccmail_id:$scope.cc_pushback,
                bccmail_id:$scope.bcc_pushback
            }

            var url='api/IasnTrnWorkItem/PostDecision';
            lockUI();
            SocketService.post(url,params).then(function (resp) {
                unlockUI();
                if(resp.data.status==true){
                    Notify.alert(resp.data.message,'success')
                 activate();
                }
                else{
                    Notify.alert(resp.data.message,'warning')
                }
            });


        }

        $scope.UploadDocCancel = function (id) {
            var params = {
                mailattachment_gid: id
            }
            var url = 'api/IasnTrnWorkItem/DeleteAttchment';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    
                    Notify.alert('Document deleted Successfully..!!', 'success')

                    var url = 'api/IasnTrnWorkItem/MailAttchment';

                    SocketService.get(url).then(function (resp) {

                        $scope.uploaddocument = resp.data.MdlDocDetails;

                    });
                }
                else {
                    Notify.alert('Error Occurred')

                }

            });
        }
         $scope.decisionchange=function(val){
              $scope.all=true;
             if(val=="Pushback"){
                $scope.pushback=true;
                $scope.forward=false;
               
                $scope.archival=false;
                $scope.IsVisibleemployee = false;

                $scope.cc_pushback=$scope.to_mail+";"+$scope.cc_mail;
                $scope.pushback_subject="Pushback : "+$scope.email_subject;
                $scope.lsShowPushbackCC=true;

                $scope.tomail_pushback=$scope.rmemployee_mailid;
              
             }
             if(val=="Forward"){
                $scope.pushback=false;
                $scope.forward=true; 
                $scope.archival=false;
                $scope.IsVisibleemployee = true;
                $scope.lsShowPushbackCC=true;
                $scope.tomail_pushback="";
                $scope.cc_pushback=$scope.to_mail+";"+$scope.cc_mail;
                $scope.pushback_subject="Forward : "+$scope.email_subject;
            }
            if(val=="Close"){
                $scope.pushback=false;
                $scope.forward=false; 
                $scope.archival=false;
                $scope.IsVisibleemployee = false;
            }
           
           
            if(val=="Archival"){
                $scope.pushback=false;
                $scope.forward=false;
                $scope.archival=true;
                $scope.IsVisibleemployee = false;
            }
         }

         $scope.forwardtochange=function(val){
          
            var url="api/IasnTrnWorkItem/EmployeeEmailID";
            var params={
                employee_gid:val
            }
            SocketService.getparams(url,params).then(function (resp) {
                
                    $scope.tomail_pushback=resp.data.employee_emailid;
             
            });
         }
        
     
         $scope.back = function () {
            if ($scope.page == 'Workitem')
            {
                $state.go("app.iasnTrnMyWorkItemSummary");
            }
            else if ($scope.page == 'Pushback')
            {
                $state.go("app.iasnTrnMyWorkItemPushback");
            }
            else if ($scope.page == 'Forward')
            {
                $state.go("app.iasnTrnMyWorkItemForward");
            }
            else if ($scope.page == 'Close')
            {
                $state.go("app.iasnTrnMyWorkItemClose");
            }
         }

         $scope.CloseWorkItem = function () {
            var modalInstance = $modal.open({
                templateUrl: '/closeWIContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                // $scope.workitemref_no=workitemref_no;
                // $scope.subject=email_subject;
                // $scope.from=email_from;
              
                $scope.CloseWIUpdate=function(){
                    
                    var params={
                        email_gid:localStorage.getItem("email_gid"),
                        decision:'Close',
                        employee_gid:'',
                        employee_name:'',
                        remarks:$scope.close_remarks,
                        mailcontent:'Close',
                        close_acknowledge:$scope.Acknowledge_mail_trigger,
                        customer_gid:'',
                        customer_name:'',
                        subject:'',
                        tomail_id:'',
                        ccmail_id:'',
                        bccmail_id:''
                    }
                   

                    var url='api/IasnTrnWorkItem/PostDecision';
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
                        unlockUI();
                        if(resp.data.status==true){
                            Notify.alert(resp.data.message,'success')                                  
                            modalInstance.close('closed');
                        }
                        else{
                            Notify.alert(resp.data.message,'warning')
                            modalInstance.close('closed');
                        }
                      
                        $state.go("app.iasnTrnMyWorkItemSummary");
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
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
                    var params={
                        emailsignature:$scope.EmailSignature
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
                            unlockUI();
                        }
                    });
                }
            }
        }

        $scope.archivalWI = function () {
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

               $scope.complete=function(string){
                
                    if(string.length >=3){
                     $scope.message="";
                     var url = 'api/customer/ExploreCustomer';
                     var params={
                          customername:string 
                      }
                      SocketService.getparams(url,params).then(function (resp) {
                          if(resp.data.status==true){
                             $scope.message="";
                             $scope.customer_list = resp.data.Customers;
                          }
                          else{
                              $scope.customer="";
                             $scope.message="No Records";
                          }
                         
                         
                  });
            }
            else{
             $scope.customer_list=null;
                $scope.message="Type atleast three character";
            }
                 }
        
               $scope.fillTextbox=function(customer_gid,customer_name){
           
               $scope.customer=customer_name;
               $scope. customer_gid=customer_gid;
               $scope.customer_list=null;

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

                $scope.ArchivalSubmit=function(){
                    var WorkItem_List = [];
                    var email_gid;
                    var sanctionref_no='';
                    var sanction_gid='';
                  
                    email_gid=localStorage.getItem("email_gid")
                    WorkItem_List.push(email_gid);
                    if($scope.archival.types_of_archival=='Specific'){
                      if($scope.cbosanctionrefno == undefined){
                        modalInstance.close('closed');
                        Notify.alert('Select the Sanction Ref No.','warning');
                        return;
                      }
                      else{
                        sanctionref_no=$('#sanction :selected').text();
                        sanction_gid=$scope.cbosanctionrefno.sanction_Gid;
                      }
                    }
                    
                   
                    var params={
                        email_gid: WorkItem_List,
                        archival_type:$scope.archival.types_of_archival,
                        remarks: $scope.archival.Remarks,
                        customer_gid:$scope.customer_gid,
                        customer_name:$scope.customer,
                        sanctionref_no:sanctionref_no,
                        sanction_gid:sanction_gid
                    }
                
                        var url = 'api/IasnTrnWorkItem/PostArchival';
                        lockUI()
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI()
                            if (resp.data.status == true) {                               
                                modalInstance.close('closed');
                                Notify.alert(resp.data.message, 'success')  
                            }
                            else {                               
                                modalInstance.close('closed');
                                Notify.alert(resp.data.message)
                            }                       
                            $state.go("app.iasnTrnMyWorkItemSummary");
                        });
                   
                }
            }
        }
      
         $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };


         $scope.mergeworkitem = function (email_gid, subject, ref_no) {
             localStorage.setItem('email_gid', email_gid);
             localStorage.setItem('email_subject', subject);
             localStorage.setItem('workitemref_no', ref_no);
             if ($scope.page == undefined) {
                 localStorage.setItem('page', $scope.page);
             }
             else {
                 localStorage.setItem('page', 'myworkitem');
             }

             $state.go('app.iasnWomMergeWorkItem');
         }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnMyWorkItemClose', iasnTrnMyWorkItemClose);

        iasnTrnMyWorkItemClose.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnTrnMyWorkItemClose($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnMyWorkItemClose';

        activate();
        lockUI();
        function activate() {
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total=100;
            $scope.totalDisplayed=0;
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'iasnTrnMyWorkItemSummary')
            }
            $scope.page = localStorage.getItem('page'); 
            var url = 'api/IasnTrnMyWorkItem/MyWorkItemCounts';
            SocketService.get(url).then(function (resp) {
                $scope.count_myworkitempending = resp.data.count_myworkitempending;
                $scope.count_myworkitempushback = resp.data.count_myworkitempushback;
                $scope.count_myworkitemforward = resp.data.count_myworkitemforward;
                $scope.count_myworkitemclose = resp.data.count_myworkitemclose;

            });
            var url = 'api/IasnTrnMyWorkItem/Close';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.close_list = resp.data.MdlWorkItem;
                if ($scope.close_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.close_list.length;
                    if ($scope.close_list.length < 100) {
                        $scope.totalDisplayed = $scope.close_list.length;
                    }
                }

            });
         
        }
        $scope.EmployeeProfile=function(emp_gid){
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params={
                employee_gid:emp_gid
            }
            SocketService.getparams(url,params).then(function (resp) {
               if(resp.data.status==true){
                $scope.user_code=resp.data.user_code;
                $scope.user_name=resp.data.user_name;
                $scope.user_photo=resp.data.user_photo;
                $scope.user_designation=resp.data.user_designation;
                $scope.user_department=resp.data.user_department;
                $scope.user_mobileno=resp.data.user_mobileno;
               }
               else{
                $scope.user_code="-";
                $scope.user_name="-";
                $scope.user_photo="N";
                $scope.user_designation="-";
                $scope.user_department="-";
               }
            });

        }
      
       /*  $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        }; */

        // Action Work Item 360
        $scope.WorkItem360=function(val){
            localStorage.setItem('email_gid',val);
            var params={
                email_gid:val
            }
            var url = 'api/IasnTrnMyWorkItem/MailSeen';
            SocketService.getparams(url,params).then(function (resp) {
            });
            if ($scope.page ==undefined)
            {
                localStorage.setItem('page','MyWorkItemSummary')
            }
            localStorage.setItem('page', 'Close')
            $state.go("app.iasnTrnMyWorkItem360");
        }

        $scope.mergeworkitem = function (email_gid,subject,ref_no) {
            localStorage.setItem('email_gid',email_gid);
            localStorage.setItem('email_subject',subject);
            localStorage.setItem('workitemref_no',ref_no);         
            if ($scope.page ==undefined)
            {
                localStorage.setItem('page','MyWorkItemSummarypage')
            }
             else 
             {
                localStorage.setItem('page','MyWorkItemSummarypage')
             }
            $state.go('app.iasnWomMergeWorkItem');
        }

        $scope.TransferWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name, checkeremployee_name) {
           
            var modalInstance = $modal.open({
                templateUrl: '/transferWIContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
           
           
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no=workitemref_no;
                $scope.subject=email_subject;
                $scope.from=email_from;
                $scope.zone_name=zone_name;
                $scope.checkeremployee_name = checkeremployee_name;
             
                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;
                   
                });
                var params={
                    lsemail_gid:email_gid
                }
                var url = 'api/IasnTrnWorkItem/TransferLog';
                SocketService.getparams(url,params).then(function (resp) {
                    if(resp.data.status==true){
                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        $scope.showtransfer=true;
                    }
                    else{
                        $scope.showtransfer=false;
                    }
                   
                   
                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.transferWIUpdate=function(){
                    
                    if($scope.transfer_to==undefined){
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person','warning');
                        return;
                    }

                    var params={
                        email_gid:email_gid,
                        employee_gid:$scope.transfer_to,
                        employee_name:$('#transfer_to :selected').text(),
                        zone_gid:'',
                        zone_name:'',
                        zone_flag:'Y'
                    }
                
                    var url="api/IasnTrnWorkItem/AssignTo";
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
                        unlockUI();
                        if(resp.data.status=true){
                            Notify.alert(resp.data.message,'success');
                        }
                        else{
                            Notify.alert(resp.data.message,'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                    
                }
            }
        }

        $scope.CloseWorkItem = function (email_gid,workitemref_no,email_from,email_subject,zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/closeWIContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no=workitemref_no;
                $scope.subject=email_subject;
                $scope.from=email_from;
              
                $scope.CloseWIUpdate=function(){
                    
                    var params={
                        email_gid:email_gid,
                        decision:'Close',
                        employee_gid:'',
                        employee_name:'',
                        remarks:$scope.close_remarks,
                        close_acknowledge:$scope.Acknowledge_mail_trigger,
                        mailcontent:'Close',
                        customer_gid:'',
                        customer_name:'',
                        subject:'',
                        tomail_id:'',
                        ccmail_id:'',
                        bccmail_id:''
                    }
                   

                    var url='api/IasnTrnWorkItem/PostDecision';
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
                        unlockUI();
                        if(resp.data.status==true){
                            Notify.alert(resp.data.message,'success')      
                                                   
                        }
                        else{
                            Notify.alert(resp.data.message,'warning')
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
        $scope.Pushback = function () {
            $state.go('app.iasnTrnMyWorkItemPushback');
        }
        $scope.Forward = function () {
            $state.go('app.iasnTrnMyWorkItemForward');
        }
        $scope.Close = function () {
            $state.go('app.iasnTrnMyWorkItemClose');
        }
        $scope.WorkItem = function () {
            $state.go('app.iasnTrnMyWorkItemSummary');
        }
      
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnMyWorkItemForward', iasnTrnMyWorkItemForward);

        iasnTrnMyWorkItemForward.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnTrnMyWorkItemForward($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnMyWorkItemForward';

        activate();
        lockUI();
        function activate() {
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total=100;
            $scope.totalDisplayed=0;
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'iasnTrnMyWorkItemSummary')
            }
            $scope.page = localStorage.getItem('page'); 
            var url = 'api/IasnTrnMyWorkItem/MyWorkItemCounts';
            SocketService.get(url).then(function (resp) {
                $scope.count_myworkitempending = resp.data.count_myworkitempending;
                $scope.count_myworkitempushback = resp.data.count_myworkitempushback;
                $scope.count_myworkitemforward = resp.data.count_myworkitemforward;
                $scope.count_myworkitemclose = resp.data.count_myworkitemclose;

            });
            var url = 'api/IasnTrnMyWorkItem/Forward';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.forward_list = resp.data.MdlWorkItem;
                if ($scope.forward_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.forward_list.length;
                    if ($scope.forward_list.length < 100) {
                        $scope.totalDisplayed = $scope.forward_list.length;
                    }
                }

            });
         
        }
        $scope.EmployeeProfile=function(emp_gid){
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params={
                employee_gid:emp_gid
            }
            SocketService.getparams(url,params).then(function (resp) {
               if(resp.data.status==true){
                $scope.user_code=resp.data.user_code;
                $scope.user_name=resp.data.user_name;
                $scope.user_photo=resp.data.user_photo;
                $scope.user_designation=resp.data.user_designation;
                $scope.user_department=resp.data.user_department;
                $scope.user_mobileno=resp.data.user_mobileno;
               }
               else{
                $scope.user_code="-";
                $scope.user_name="-";
                $scope.user_photo="N";
                $scope.user_designation="-";
                $scope.user_department="-";
               }
            });

        }
        
             
       /*  $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        }; */

        // Action Work Item 360
        $scope.WorkItem360=function(val){
            localStorage.setItem('email_gid',val);
            var params={
                email_gid:val
            }
            var url = 'api/IasnTrnMyWorkItem/MailSeen';
            SocketService.getparams(url,params).then(function (resp) {
            });
            if ($scope.page ==undefined)
            {
                localStorage.setItem('page','MyWorkItemSummary')
            }
            localStorage.setItem('page', 'Forward')
            $state.go("app.iasnTrnMyWorkItem360");
        }

        $scope.mergeworkitem = function (email_gid,subject,ref_no) {
            localStorage.setItem('email_gid',email_gid);
            localStorage.setItem('email_subject',subject);
            localStorage.setItem('workitemref_no',ref_no);         
            if ($scope.page ==undefined)
            {
                localStorage.setItem('page','MyWorkItemSummarypage')
            }
             else 
             {
                localStorage.setItem('page','MyWorkItemSummarypage')
             }
            $state.go('app.iasnWomMergeWorkItem');
        }

        $scope.TransferWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name, checkeremployee_name) {
           
            var modalInstance = $modal.open({
                templateUrl: '/transferWIContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
           
           
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no=workitemref_no;
                $scope.subject=email_subject;
                $scope.from=email_from;
                $scope.zone_name=zone_name;
                $scope.checkeremployee_name = checkeremployee_name;
             
                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;
                   
                });
                var params={
                    lsemail_gid:email_gid
                }
                var url = 'api/IasnTrnWorkItem/TransferLog';
                SocketService.getparams(url,params).then(function (resp) {
                    if(resp.data.status==true){
                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        $scope.showtransfer=true;
                    }
                    else{
                        $scope.showtransfer=false;
                    }
                   
                   
                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.transferWIUpdate=function(){
                    
                    if($scope.transfer_to==undefined){
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person','warning');
                        return;
                    }

                    var params={
                        email_gid:email_gid,
                        employee_gid:$scope.transfer_to,
                        employee_name:$('#transfer_to :selected').text(),
                        zone_gid:'',
                        zone_name:'',
                        zone_flag:'Y'
                    }
                
                    var url="api/IasnTrnWorkItem/AssignTo";
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
                        unlockUI();
                        if(resp.data.status=true){
                            Notify.alert(resp.data.message,'success');
                        }
                        else{
                            Notify.alert(resp.data.message,'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                    
                }
            }
        }

        $scope.CloseWorkItem = function (email_gid,workitemref_no,email_from,email_subject,zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/closeWIContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no=workitemref_no;
                $scope.subject=email_subject;
                $scope.from=email_from;
              
                $scope.CloseWIUpdate=function(){
                    
                    var params={
                        email_gid:email_gid,
                        decision:'Close',
                        employee_gid:'',
                        employee_name:'',
                        remarks:$scope.close_remarks,
                        close_acknowledge:$scope.Acknowledge_mail_trigger,
                        mailcontent:'Close',
                        customer_gid:'',
                        customer_name:'',
                        subject:'',
                        tomail_id:'',
                        ccmail_id:'',
                        bccmail_id:''
                    }
                   

                    var url='api/IasnTrnWorkItem/PostDecision';
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
                        unlockUI();
                        if(resp.data.status==true){
                            Notify.alert(resp.data.message,'success')      
                                                   
                        }
                        else{
                            Notify.alert(resp.data.message,'warning')
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
        $scope.Pushback = function () {
            $state.go('app.iasnTrnMyWorkItemPushback');
        }
        $scope.Forward = function () {
            $state.go('app.iasnTrnMyWorkItemForward');
        }
        $scope.Close = function () {
            $state.go('app.iasnTrnMyWorkItemClose');
        }
        $scope.WorkItem = function () {
            $state.go('app.iasnTrnMyWorkItemSummary');
        }
      
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnMyWorkItemPushback', iasnTrnMyWorkItemPushback);

        iasnTrnMyWorkItemPushback.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnTrnMyWorkItemPushback($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnMyWorkItemPushback';

        activate();
        lockUI();
        function activate() {
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total=100;
            $scope.totalDisplayed=0;
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'iasnTrnMyWorkItemSummary')
            }
            $scope.page = localStorage.getItem('page'); 
            var url = 'api/IasnTrnMyWorkItem/MyWorkItemCounts';
            SocketService.get(url).then(function (resp) {
                $scope.count_myworkitempending = resp.data.count_myworkitempending;
                $scope.count_myworkitempushback = resp.data.count_myworkitempushback;
                $scope.count_myworkitemforward = resp.data.count_myworkitemforward;
                $scope.count_myworkitemclose = resp.data.count_myworkitemclose;

            });
            var url = 'api/IasnTrnMyWorkItem/Pushback';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.pushback_list = resp.data.MdlWorkItem;
                if ($scope.pushback_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.pushback_list.length;
                    if ($scope.pushback_list.length < 100) {
                        $scope.totalDisplayed = $scope.pushback_list.length;
                    }
                }

            });
         
        }
        $scope.EmployeeProfile=function(emp_gid){
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params={
                employee_gid:emp_gid
            }
            SocketService.getparams(url,params).then(function (resp) {
               if(resp.data.status==true){
                $scope.user_code=resp.data.user_code;
                $scope.user_name=resp.data.user_name;
                $scope.user_photo=resp.data.user_photo;
                $scope.user_designation=resp.data.user_designation;
                $scope.user_department=resp.data.user_department;
                $scope.user_mobileno=resp.data.user_mobileno;
               }
               else{
                $scope.user_code="-";
                $scope.user_name="-";
                $scope.user_photo="N";
                $scope.user_designation="-";
                $scope.user_department="-";
               }
            });

        }
        
        $scope.Close=function(){
            var url = 'api/IasnTrnMyWorkItem/Close';
            SocketService.get(url).then(function (resp) {
                $scope.close_list = resp.data.MdlWorkItem;
                if ($scope.close_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.close_list.length;
                    if ($scope.close_list.length < 100) {
                        $scope.totalDisplayed = $scope.close_list.length;
                    }
                }

            });
        }
      
       /*  $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        }; */

        // Action Work Item 360
        $scope.WorkItem360=function(val){
            localStorage.setItem('email_gid',val);
            var params={
                email_gid:val
            }
            var url = 'api/IasnTrnMyWorkItem/MailSeen';
            SocketService.getparams(url,params).then(function (resp) {
            });
            if ($scope.page ==undefined)
            {
                localStorage.setItem('page','MyWorkItemSummary')
            }
            localStorage.setItem('page', 'Pushback')
            $state.go("app.iasnTrnMyWorkItem360");
        }

        $scope.mergeworkitem = function (email_gid,subject,ref_no) {
            localStorage.setItem('email_gid',email_gid);
            localStorage.setItem('email_subject',subject);
            localStorage.setItem('workitemref_no',ref_no);         
            if ($scope.page ==undefined)
            {
                localStorage.setItem('page','MyWorkItemSummarypage')
            }
             else 
             {
                localStorage.setItem('page','MyWorkItemSummarypage')
             }
            $state.go('app.iasnWomMergeWorkItem');
        }

        $scope.TransferWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name, checkeremployee_name) {
           
            var modalInstance = $modal.open({
                templateUrl: '/transferWIContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
           
           
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no=workitemref_no;
                $scope.subject=email_subject;
                $scope.from=email_from;
                $scope.zone_name=zone_name;
                $scope.checkeremployee_name = checkeremployee_name;
             
                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;
                   
                });
                var params={
                    lsemail_gid:email_gid
                }
                var url = 'api/IasnTrnWorkItem/TransferLog';
                SocketService.getparams(url,params).then(function (resp) {
                    if(resp.data.status==true){
                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        $scope.showtransfer=true;
                    }
                    else{
                        $scope.showtransfer=false;
                    }
                   
                   
                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.transferWIUpdate=function(){
                    
                    if($scope.transfer_to==undefined){
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person','warning');
                        return;
                    }

                    var params={
                        email_gid:email_gid,
                        employee_gid:$scope.transfer_to,
                        employee_name:$('#transfer_to :selected').text(),
                        zone_gid:'',
                        zone_name:'',
                        zone_flag:'Y'
                    }
                
                    var url="api/IasnTrnWorkItem/AssignTo";
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
                        unlockUI();
                        if(resp.data.status=true){
                            Notify.alert(resp.data.message,'success');
                        }
                        else{
                            Notify.alert(resp.data.message,'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                    
                }
            }
        }

        $scope.CloseWorkItem = function (email_gid,workitemref_no,email_from,email_subject,zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/closeWIContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no=workitemref_no;
                $scope.subject=email_subject;
                $scope.from=email_from;
              
                $scope.CloseWIUpdate=function(){
                    
                    var params={
                        email_gid:email_gid,
                        decision:'Close',
                        employee_gid:'',
                        employee_name:'',
                        remarks:$scope.close_remarks,
                        close_acknowledge:$scope.Acknowledge_mail_trigger,
                        mailcontent:'Close',
                        customer_gid:'',
                        customer_name:'',
                        subject:'',
                        tomail_id:'',
                        ccmail_id:'',
                        bccmail_id:''
                    }
                   

                    var url='api/IasnTrnWorkItem/PostDecision';
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
                        unlockUI();
                        if(resp.data.status==true){
                            Notify.alert(resp.data.message,'success')      
                                                   
                        }
                        else{
                            Notify.alert(resp.data.message,'warning')
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }

        $scope.Pushback = function () {
            $state.go('app.iasnTrnMyWorkItemPushback');
        }
        $scope.Forward = function () {
            $state.go('app.iasnTrnMyWorkItemForward');
        }
        $scope.Close = function () {
            $state.go('app.iasnTrnMyWorkItemClose');
        }
        $scope.WorkItem = function () {
            $state.go('app.iasnTrnMyWorkItemSummary');
        }
      
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnMyWorkItemSummary', iasnTrnMyWorkItemSummary);

        iasnTrnMyWorkItemSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnTrnMyWorkItemSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnMyWorkItemSummary';

        activate();
        lockUI();
        function activate() {
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total=100;
            $scope.totalDisplayed=0;
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'iasnTrnMyWorkItemSummary')
            }
           
            $scope.page = localStorage.getItem('page');    
            var url = 'api/IasnTrnMyWorkItem/MyWorkItemCounts';
            SocketService.get(url).then(function (resp) {
                $scope.count_myworkitempending = resp.data.count_myworkitempending;
                $scope.count_myworkitempushback = resp.data.count_myworkitempushback;
                $scope.count_myworkitemforward = resp.data.count_myworkitemforward;
                $scope.count_myworkitemclose = resp.data.count_myworkitemclose;

            });

            var url = 'api/IasnTrnMyWorkItem/Pending';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.Pending_list = resp.data.MdlWorkItem;
          
                if ($scope.Pending_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.Pending_list.length;
                    if ($scope.Pending_list.length < 100) {
                        $scope.totalDisplayed = $scope.Pending_list.length;
                    }
                }

            });

           
        }
        $scope.EmployeeProfile=function(emp_gid){
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params={
                employee_gid:emp_gid
            }
            SocketService.getparams(url,params).then(function (resp) {
               if(resp.data.status==true){
                $scope.user_code=resp.data.user_code;
                $scope.user_name=resp.data.user_name;
                $scope.user_photo=resp.data.user_photo;
                $scope.user_designation=resp.data.user_designation;
                $scope.user_department=resp.data.user_department;
                $scope.user_mobileno=resp.data.user_mobileno;
               }
               else{
                $scope.user_code="-";
                $scope.user_name="-";
                $scope.user_photo="N";
                $scope.user_designation="-";
                $scope.user_department="-";
               }
            });

        }
        
             
      /*   $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        }; */
        
        // Action Work Item 360
        $scope.WorkItem360=function(val){
            localStorage.setItem('email_gid',val);
            var params={
                email_gid:val
            }
            var url = 'api/IasnTrnMyWorkItem/MailSeen';
            SocketService.getparams(url,params).then(function (resp) {
            });
            if ($scope.page ==undefined)
            {
                localStorage.setItem('page','MyWorkItemSummary')
            }
            localStorage.setItem('page', 'Workitem')
            $state.go("app.iasnTrnMyWorkItem360");
        }

        $scope.mergeworkitem = function (email_gid,subject,ref_no) {
            localStorage.setItem('email_gid',email_gid);
            localStorage.setItem('email_subject',subject);
            localStorage.setItem('workitemref_no',ref_no);         
            if ($scope.page ==undefined)
            {
                localStorage.setItem('page','MyWorkItemSummarypage')
            }
             else 
             {
                localStorage.setItem('page','MyWorkItemSummarypage')
             }
            $state.go('app.iasnWomMergeWorkItem');
        }

        $scope.TransferWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name, checkeremployee_name) {
           
            var modalInstance = $modal.open({
                templateUrl: '/transferWIContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
           
           
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no=workitemref_no;
                $scope.subject=email_subject;
                $scope.from=email_from;
                $scope.zone_name=zone_name;
                $scope.checkeremployee_name = checkeremployee_name;
             
                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;
                   
                });
                var params={
                    lsemail_gid:email_gid
                }
                var url = 'api/IasnTrnWorkItem/TransferLog';
                SocketService.getparams(url,params).then(function (resp) {
                    if(resp.data.status==true){
                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        $scope.showtransfer=true;
                    }
                    else{
                        $scope.showtransfer=false;
                    }
                   
                   
                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.transferWIUpdate=function(){
                    
                    if($scope.transfer_to==undefined){
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person','warning');
                        return;
                    }

                    var params={
                        email_gid:email_gid,
                        employee_gid:$scope.transfer_to,
                        employee_name:$('#transfer_to :selected').text(),
                        zone_gid:'',
                        zone_name:'',
                        zone_flag:'Y'
                    }
                
                    var url="api/IasnTrnWorkItem/AssignTo";
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
                        unlockUI();
                        if(resp.data.status=true){
                            Notify.alert(resp.data.message,'success');
                        }
                        else{
                            Notify.alert(resp.data.message,'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                    
                }
            }
        }

        $scope.CloseWorkItem = function (email_gid,workitemref_no,email_from,email_subject,zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/closeWIContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no=workitemref_no;
                $scope.subject=email_subject;
                $scope.from=email_from;
              
                $scope.CloseWIUpdate=function(){
                    
                    var params={
                        email_gid:email_gid,
                        decision:'Close',
                        employee_gid:'',
                        employee_name:'',
                        remarks:$scope.close_remarks,
                        close_acknowledge:$scope.Acknowledge_mail_trigger,
                        mailcontent:'Close',
                        customer_gid:'',
                        customer_name:'',
                        subject:'',
                        tomail_id:'',
                        ccmail_id:'',
                        bccmail_id:''
                    }
                   

                    var url='api/IasnTrnWorkItem/PostDecision';
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
                        unlockUI();
                        if(resp.data.status==true){
                            Notify.alert(resp.data.message,'success')      
                                                   
                        }
                        else{
                            Notify.alert(resp.data.message,'warning')
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
       
        $scope.Pushback = function () {
            $state.go('app.iasnTrnMyWorkItemPushback');
        }
        $scope.Forward = function () {
            $state.go('app.iasnTrnMyWorkItemForward');
        }
        $scope.Close = function () {
            $state.go('app.iasnTrnMyWorkItemClose');
        }
        $scope.WorkItem = function () {
            $state.go('app.iasnTrnMyWorkItemSummary');
        }
      
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnPushbackSummary', iasnTrnPushbackSummary);

    iasnTrnPushbackSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnTrnPushbackSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnPushbackSummary';

        activate();

        function activate() {
            lockUI();
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummary')
            }
            $scope.page = localStorage.getItem('page');
            var url = 'api/IasnTrnWorkItem/WorkItemCounts';
            SocketService.get(url).then(function (resp) {
                $scope.count_workitempending = resp.data.count_workitempending;
                $scope.count_workitemassigned = resp.data.count_workitemassigned;
                $scope.count_pushback = resp.data.count_pushback;
                $scope.count_forward = resp.data.count_forward;
                $scope.count_close = resp.data.count_close;
                $scope.count_archival = resp.data.count_archival;
                $scope.count_composemail = resp.data.count_composemail;

            });
            var url = 'api/IasnTrnWorkItem/WorkItemPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });

            var url = 'api/IasnTrnWorkItem/WorkItemPushbackSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPushback_List = resp.data.MdlWorkItem;
                unlockUI();
                if ($scope.WorkItemPushback_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPushback_List.length;
                    if ($scope.WorkItemPushback_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPushback_List.length;
                    }
                }

            });
        }

        $scope.EmployeeProfile = function (emp_gid) {
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params = {
                employee_gid: emp_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.user_code = resp.data.user_code;
                    $scope.user_name = resp.data.user_name;
                    $scope.user_photo = resp.data.user_photo;
                    $scope.user_designation = resp.data.user_designation;
                    $scope.user_department = resp.data.user_department;
                    $scope.user_mobileno = resp.data.user_mobileno;
                }
                else {
                    $scope.user_code = "-";
                    $scope.user_name = "-";
                    $scope.user_photo = "N";
                    $scope.user_designation = "-";
                    $scope.user_department = "-";
                }
            });

        }
        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };
        // Action Work Item 360
        $scope.WorkItem360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', $scope.page)
            }
            localStorage.setItem('page', 'Pushback')
            $state.go("app.iasnTrnWorkItem360");
        }

        // Action Work Item Allotted 360
        $scope.WorkItemAllotted360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', $scope.page)
            }

            $state.go("app.iasnTrnWorkItemAllotted360");
        }

        $scope.Allotted = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemAllotted_List = resp.data.MdlWorkItem;

                if ($scope.WorkItemAllotted_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemAllotted_List.length;
                    if ($scope.WorkItemAllotted_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemAllotted_List.length;
                    }
                }
            });

        }




        $scope.WorkItem = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });
        }

        

        $scope.Forward = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemForwardSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemForward_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemForward_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemForward_List.length;
                    if ($scope.WorkItemForward_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemForward_List.length;
                    }
                }

            });
        }

        $scope.CloseTab = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemCloseSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemClose_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemClose_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemClose_List.length;
                    if ($scope.WorkItemClose_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemClose_List.length;
                    }
                }

            });
        }


        $scope.AssignZone = function (email_gid, workitemref_no, email_from, email_subject, zone_gid, zone_name) {

            var modalInstance = $modal.open({
                templateUrl: '/assignZoneContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });


            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;

                $scope.rdb_acks = "N";
                var url = "api/IasnMstZone/ZoneSummary";
                SocketService.get(url).then(function (resp) {

                    $scope.zone_list = resp.data.MdlZoneSummary;

                });
                //if(zone_gid==undefined || zone_gid==""){
                //    $scope.zone_flag="N"

                //}
                //else{
                //    $scope.zone_name=zone_gid;
                //    $scope.lblzonename=zone_name;
                //    $scope.zone_flag="Y"
                //}

                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;

                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.AssignToUpdate = function () {


                    var params = {
                        email_gid: email_gid,
                        zone_gid: $scope.zone_name,
                        zone_name: $('#zone_name :selected').text(),
                        zone_flag: $scope.zone_flag,
                        acknowledgement_flag: $scope.rdb_acks,
                    }

                    var url = "api/IasnTrnWorkItem/AssignZone";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });

                }
            }
        }

        $scope.TransferWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name, assign_to) {

            var modalInstance = $modal.open({
                templateUrl: '/transferWIContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });


            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;
                $scope.zone_name = zone_name;
                $scope.checkeremployee_name = assign_to;

                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;

                });

                var params = {
                    lsemail_gid: email_gid
                }
                var url = 'api/IasnTrnWorkItem/TransferLog';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        $scope.showtransfer = true;
                    }
                    else {
                        $scope.showtransfer = false;
                    }


                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.transferWIUpdate = function () {

                    if ($scope.transfer_to == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        email_gid: email_gid,
                        employee_gid: $scope.transfer_to,
                        employee_name: $('#transfer_to :selected').text(),
                        zone_gid: '',
                        zone_name: '',
                        zone_flag: 'Y'
                    }

                    var url = "api/IasnTrnWorkItem/AssignTo";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });

                }
            }
        }

        $scope.CloseWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/closeWIContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;

                $scope.CloseWIUpdate = function () {

                    var params = {
                        email_gid: email_gid,
                        decision: 'Close',
                        employee_gid: '',
                        employee_name: '',
                        remarks: $scope.close_remarks,
                        close_acknowledge: $scope.Acknowledge_mail_trigger,
                        mailcontent: 'Close',
                        customer_gid: '',
                        customer_name: '',
                        subject: '',
                        tomail_id: '',
                        ccmail_id: '',
                        bccmail_id: ''
                    }


                    var url = 'api/IasnTrnWorkItem/PostDecision';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, 'success')
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning')
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
        // Add Work Item Code Ends

        // Merge Work Item Code Starts
        $scope.mergeworkitem = function (email_gid, subject, ref_no) {
            localStorage.setItem('email_gid', email_gid);
            localStorage.setItem('email_subject', subject);
            localStorage.setItem('workitemref_no', ref_no);

            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', 'workitemsummarypage')
            }

            $state.go('app.iasnWomMergeWorkItem');
        }
        // Merge Work Item Code Ends
        $scope.Alloted = function () {
            $state.go('app.iasnTrnAllotedSummary');
        }
        $scope.Pushback = function () {
            $state.go('app.iasnTrnPushbackSummary');
        }
        $scope.Forward = function () {
            $state.go('app.iasnTrnForwardSummary');
        }
        $scope.Close = function () {
            $state.go('app.iasnTrnCloseSummary');
        }
        $scope.WorkItem = function () {
            $state.go('app.iasnTrnWorkItemSummary');
        }
        $scope.ComposeMail = function () {
            $state.go('app.iasnWomWorkOrderSummary');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnWorkItem360', iasnTrnWorkItem360);

        iasnTrnWorkItem360.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce'];

    function iasnTrnWorkItem360($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,$sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnWorkItem360';
        activate();

        function activate() {
            $scope.PnlSpecific = false;
            $scope.IsVisibleteam=false;
            $scope.IsVisibleemployee=false;
            //$scope.pushback=false;
            //$scope.forward=false;
            $scope.all=false;
            $scope.archival=false;
            $scope.typeE = "";
            $scope.IsLogShow=false;

           $scope.email_gid=localStorage.getItem('email_gid');
           $scope.page = localStorage.getItem('page');
        
            var url = 'api/IasnTrnWorkItem/WorkItemView';
            var params={
                lsemail_gid:localStorage.getItem('email_gid')
            };
         
            SocketService.getparams(url,params).then(function (resp) {
               
                $scope.email_from = resp.data.email_from;
                $scope.email_date=resp.data.email_date;
                $scope.email_subject=resp.data.email_subject;
                $scope.email_content=resp.data.email_content;
                $scope.created_date=resp.data.created_date;
                $scope.cc_mail=resp.data.cc;
                $scope.bcc_mail=resp.data.bcc;
                $scope.to_mail=resp.data.email_to;
                $scope.workitemref_no=resp.data.workitemref_no;
                $scope.zone_gid=resp.data.zone_gid;
                $scope.zone_name=resp.data.zone_name;
                $scope.rmemployee_gid=resp.data.rmemployee_gid;
                $scope.rmemployee_name=resp.data.rmemployee_name;   
                $scope.rmemployee_mailid=resp.data.email_address;
                $scope.checkeremployee_name=resp.data.checkeremployee_name;
                $scope.attch_list=resp.data.MdlAttachmentList;
                $scope.allottedby_on=resp.data.allottedby_on;
                $scope.aging=resp.data.aging;
                $scope.status=resp.data.status;
                $scope.archivalremarks = resp.data.archivalremarks;
                $scope.Mail_Trigger = resp.data.Mail_Trigger;
                $scope.originalmail_Subject = resp.data.originalmail_Subject;
                $scope.hold_flag = resp.data.hold_flag;
                $scope.workitemhold_reason = resp.data.workitemhold_reason;
                $scope.customer = resp.data.customer_name;
                $scope.rdbcustomer_type = resp.data.customer_type;
                $scope.cutomer_type = resp.data.customer_type;
                $scope.customer_name = resp.data.customer_name;

                if (resp.data.status == 'Pending') {
                    $scope.pendingcustomer = true;
                    $scope.othercustomer = false;
                }
                else {
                    $scope.pendingcustomer = false;
                    $scope.othercustomer = true;
                }
                if (resp.data.customer_type == 'New' || resp.data.customer_type == 'Others') {
                    $scope.customerexisting = false;
                    $scope.customernew = true;
                }
                else if (resp.data.customer_type == 'Existing') {
                    $scope.customerexisting = true;
                    $scope.customernew = false;
                }
                else {
                    $scope.customerexisting = false;
                    $scope.customernew = false;
                }

                if ($scope.archivalremarks==''|| $scope.archivalremarks== null )
                {
                    $scope.archiverem= false;
                }
                else{
                    $scope.archiverem=true; 
                }
                $scope.closedremarks = resp.data.closedremarks;
                if ($scope.closedremarks==''|| $scope.closedremarks== null )
                {
                    $scope.closerem= false;
                }
                else{
                    $scope.closerem= true; 
                }
                $scope.updatedby_on=resp.data.updatedby_on;
                $scope.message_id=resp.data.message_id;
                $scope.reference_id=resp.data.reference_id;
               
                if(resp.data.employee_gid !=null){
                   
                    $scope.assign_to=resp.data.employee_gid;
                   
                }
               

            });
            
         
            var params={
                email_gid:localStorage.getItem("email_gid")
            };
         
            var url="api/IasnTrnWorkItem/ReferenceMail";
            SocketService.getparams(url,params).then(function (resp) {
                if(resp.data.status==true){
                    $scope.referenceMail = resp.data.MdlReferenceMail;
                }
                else{

                }
            });

           


            var url = 'api/IasnTrnAuditLog/PostAuditView';

            var params={
                email_gid:localStorage.getItem("email_gid")
            };
            SocketService.getparams(url,params).then(function (resp) {
                if(resp.data.status==true){  
                }
                else{

                }
                
            });

         
        }
      
        $scope.rdbcustomer_new = function (rdbcustomer_type) {
            $scope.customerexisting = false;
            $scope.customernew = true;
            $scope.customer = '';
        }
        $scope.rdbcustomer_existing = function (rdbcustomer_type) {
            $scope.customerexisting = true;
            $scope.customernew = false;
            $scope.customer = '';
        }
        $scope.rdbcustomer_others = function (rdbcustomer_type) {
            $scope.customerexisting = false;
            $scope.customernew = true;
            $scope.customer = '';
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
        }

        $scope.UpdateCustomer = function () {
            if ($scope.rdbcustomer_type == '' || $scope.rdbcustomer_type == null || $scope.rdbcustomer_type == undefined) {
                Notify.alert('Kindly Select Customer Type', 'warning');
            }
            else if ($scope.customer == '' || $scope.customer == null || $scope.customer == undefined) {
                Notify.alert('Kindly Enter Customer Name', 'warning');
            }
            else {
                var params = {
                    customer_name: $scope.customer,
                    customer_gid: $scope.customer_gid,
                    customer_type: $scope.rdbcustomer_type,
                    email_gid: $scope.email_gid,
                }
                var url = "api/IasnTrnWorkItem/UpdateCustomer";
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
        }

        $scope.back = function () {
            $state.go('app/iasnTrnWorkItemSummary');
        }

         $scope.export = function (path,attchment_name) {
         
           
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
         $scope.AssignWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_gid, zone_name, originalmail_Subject) {
           
    var modalInstance = $modal.open({
        templateUrl: '/assignWIContent.html',
        controller: ModalInstanceCtrl,
        size: 'md',
        backdrop: 'static',
        keyboard: false,
    });
   
   
    ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
    function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        $scope.workitemref_no=workitemref_no;
        $scope.subject=email_subject;
        $scope.from = email_from;
        $scope.originalmail_Subject = originalmail_Subject;

        var url = "api/IasnMstZone/ZoneSummary";
        SocketService.get(url).then(function (resp) {
       
            $scope.zone_list = resp.data.MdlZoneSummary;
            
        });
        if(zone_gid==undefined || zone_gid==""){
            $scope.zone_flag="N"
           
        }
        else{
            $scope.ddlzone_name=zone_gid;
            $scope.lblzonename=zone_name;
            $scope.zone_flag="Y"
        }
      
        var url = 'api/IasnTrnWorkItem/IsnEmployee';
        SocketService.get(url).then(function (resp) {
            $scope.employee_list = resp.data.MdlIsnEmployee;
           
        });
        $scope.close = function () {
            modalInstance.close('closed');
        };

        $scope.AssignToUpdate=function(){
            
            if($scope.ddlassign_to==undefined){
                modalInstance.close('closed');
                Notify.alert('Kindly Select the Assign to Person','warning');
                return;
            }

            var params={
                email_gid:email_gid,
                employee_gid:$scope.ddlassign_to.employee_gid,
                employee_name:$('#ddlassign_to :selected').text(),
                zone_gid:$scope.ddlzone_name,
                zone_name:$('#ddlzone_name :selected').text(),
                zone_flag: $scope.zone_flag,
                assign_remarks: $scope.assign_remarks,
            }
           
            var url="api/IasnTrnWorkItem/AssignTo";
            lockUI();
            SocketService.post(url,params).then(function (resp) {
                unlockUI();
                if(resp.data.status=true){
                    modalInstance.close('closed');
                    Notify.alert(resp.data.message,'success');
                    if ($scope.page=='archival')
                    {
                       $state.go("app.iasnTrnArchivalSummary");
                    }
                   else
                   {
                        $state.go("app.iasnTrnWorkItemSummary");
                   }   
                }
                else{
                    modalInstance.close('closed');
                    Notify.alert(resp.data.message,'warning');
                    if ($scope.page=='archival')
                    {
                       $state.go("app.iasnTrnArchivalSummary");
                    }
                   else
                   {
                        $state.go("app.iasnTrnWorkItemSummary");
                   }  
                }
               
            });
           
        }
    }
}

$scope.logdetails=function(){

    if($scope.IsLogShow==true){
        $scope.IsLogShow=false; 
    }
    else{
        $scope.IsLogShow=true;
        
        var url = 'api/IasnTrnWorkItem/TransferLog';

        var params={
            lsemail_gid:localStorage.getItem("email_gid")
        };
    
        SocketService.getparams(url,params).then(function (resp) {
            if(resp.data.status==true){
               
                $scope.transferlog_list = resp.data.MdlTransferLog;
                if( $scope.transferlog_list  == null)
                {
                    $scope.transfershow = true;
                }
                else{
                    $scope.transfershow = false;
                }
            }
            else{
               
            }
            
        });

        var url = 'api/IasnTrnAuditLog/AuditLog';

        var params={
            email_gid:localStorage.getItem("email_gid")
        };            
        SocketService.getparams(url,params).then(function (resp) {
            if(resp.data.status==true){
                $scope.auditlog_list = resp.data.MdlAuditLog;
            }
            else{
               
            }
            
        });

       
    }
    
}
$scope.logClose=function(){
    $scope.IsLogShow=false;
}
$scope.TransferWorkItem = function (email_gid,workitemref_no,email_from,email_subject,zone_gid,zone_name,assign_to) {
   
    var modalInstance = $modal.open({
        templateUrl: '/transferWIContent.html',
        controller: ModalInstanceCtrl,
        size: 'md',
        backdrop: 'static',
        keyboard: false,
    });
   
   
    ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
    function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
       $scope.workitemref_no=workitemref_no;
        $scope.subject=email_subject;
        $scope.from=email_from;
        $scope.zone_name=zone_name;
        $scope.checkeremployee_name=assign_to;
     
        var url = 'api/IasnTrnWorkItem/IsnEmployee';
        SocketService.get(url).then(function (resp) {
            $scope.employee_list = resp.data.MdlIsnEmployee;
           
        });

        var params={
            lsemail_gid:email_gid
        }
        var url = 'api/IasnTrnWorkItem/TransferLog';
        SocketService.getparams(url,params).then(function (resp) {
            if(resp.data.status==true){
                $scope.transferlog_list = resp.data.MdlTransferLog;
                $scope.showtransfer=true;
            }
            else{
                $scope.showtransfer=false;
            }
           
           
        });
        $scope.close = function () {
            modalInstance.close('closed');
        };

        $scope.transferWIUpdate=function(){
            
            if($scope.transfer_to==undefined){
                modalInstance.close('closed');
                Notify.alert('Kindly Select the Assign to Person','warning');
                return;
            }

            var params={
                email_gid:localStorage.getItem("email_gid"),
                employee_gid:$scope.transfer_to,
                employee_name:$('#transfer_to :selected').text(),
                zone_gid:'',
                zone_name:'',
                zone_flag:'Y'
            }
        
            var url="api/IasnTrnWorkItem/AssignTo";
            lockUI();
            SocketService.post(url,params).then(function (resp) {
                unlockUI();
                if(resp.data.status=true){
                    modalInstance.close('closed');
                    Notify.alert(resp.data.message,'success');
                    if ($scope.page=='archival')
                    {
                       $state.go("app.iasnTrnArchivalSummary");
                    }
                   else
                   {
                        $state.go("app.iasnTrnWorkItemSummary");
                   }  
                }
                else{
                    modalInstance.close('closed');
                    Notify.alert(resp.data.message,'warning');
                    if ($scope.page=='archival')
                    {
                       $state.go("app.iasnTrnArchivalSummary");
                    }
                   else
                   {
                        $state.go("app.iasnTrnWorkItemSummary");
                   }  
                }
            });
           
        }
    }
}

$scope.CloseWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name, originalmail_Subject) {
    var modalInstance = $modal.open({
        templateUrl: '/closeWIContent.html',
        controller: ModalInstanceCtrl,
        size: 'md',
        backdrop: 'static',
        keyboard: false,
    });
    ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
    function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        $scope.workitemref_no=workitemref_no;
        $scope.subject=email_subject;
        $scope.from = email_from;
        $scope.originalmail_Subject = originalmail_Subject;
      
        $scope.CloseWIUpdate=function(){
            
            var params={
                email_gid:localStorage.getItem("email_gid"),
                decision:'Close',
                employee_gid:'',
                employee_name:'',
                remarks:$scope.close_remarks,
                close_acknowledge:$scope.Acknowledge_mail_trigger,
                mailcontent:'Close',
                customer_gid:'',
                customer_name:'',
                subject:'',
                tomail_id:'',
                ccmail_id:'',
                bccmail_id:''
            }
           

            var url='api/IasnTrnWorkItem/PostDecision';
            lockUI();
            SocketService.post(url,params).then(function (resp) {
                unlockUI();
                if(resp.data.status==true){
                    modalInstance.close('closed');
                    Notify.alert(resp.data.message,'success')
                    if ($scope.page=='archival')
                    {
                       $state.go("app.iasnTrnArchivalSummary");
                    }
                   else
                   {
                        $state.go("app.iasnTrnWorkItemSummary");
                   }                  
                
                }
                else{
                    modalInstance.close('closed');
                    Notify.alert(resp.data.message,'warning')
                    if ($scope.page=='archival')
                    {
                       $state.go("app.iasnTrnArchivalSummary");
                    }
                   else
                   {
                        $state.go("app.iasnTrnWorkItemSummary");
                   }
                }
               
            });
        }

        $scope.close = function () {
            modalInstance.close('closed');
        };
    }
}
$scope.uploadattachment = function (val,val1,name) {
    var item = {
        name: val[0].name,
        file: val[0]
    };
    var frm = new FormData();
    frm.append('fileupload', item.file);
    frm.append('file_name', item.name);
    frm.append('project_flag', "Default");
   
    

    $scope.uploadfrm = frm;
    var url = 'api/IasnTrnWorkItem/MailAttchment';
    lockUI();
    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
        unlockUI();
        $("#addupload").val('');
        $scope.txtdocument_title = '';
        if (resp.data.status == true) {
          
            Notify.alert('Document Uploaded Successfully..!!', 'success')
           
            var url = 'api/IasnTrnWorkItem/MailAttchment';

            SocketService.get(url).then(function (resp) {
               
                $scope.uploaddocument = resp.data.MdlDocDetails;
              
            });
        }
        else {
           
            Notify.alert('File Format Not Supported!')

        }

    });

}

$scope.transferto_change=function(val){
    SweetAlert.swal({
        title: 'Are you sure?',
        text: 'Do you want to transfer the work item?',

        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Yes,Transfer it!',
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            lockUI();
            var params={
                email_gid:localStorage.getItem("email_gid"),
                employee_gid:val,
                employee_name:$('#transfer_to :selected').text()
            }
            var url="api/IasnTrnWorkItem/AssignTo";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
                    SweetAlert.swal('Work Item Transfered Successfully!');
                   
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
        else{
            SweetAlert.swal('Error Occured');
        }

    });
}

$scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

$scope.onchangecopy=function(val){
    SweetAlert.swal({
        title: 'Are you sure?',
        text: 'Do you want to move the work item to yours bin?',

        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Yes,Move it!',
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            lockUI();
            var params={
                email_gid:localStorage.getItem("email_gid"),
                employee_gid:null,
                employee_name:null
            }
            var url="api/IasnTrnWorkItem/AssignTo";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
                    SweetAlert.swal('Work Item Moved to Your Bin Successfully!');
                   
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
        else{
            SweetAlert.swal('Error Occured');
        }

    });
}
        $scope.assignto_change=function(val){
            var params={
                email_gid:localStorage.getItem("email_gid"),
                employee_gid:val,
                employee_name:$('#assign_to :selected').text()
            }
            lockUI();
            var url="api/IasnTrnWorkItem/AssignTo";
            SocketService.post(url,params).then(function (resp) {
                unlockUI();
                if(resp.data.status=true){
                    Notify.alert(resp.data.message,'success');
                }
                else{
                    Notify.alert(resp.data.message,'warning');
                }
            });
               
        }
        $scope.updateDesicion=function(){
            var emp_gid;
            var emp_name;
            var customer_gid;
            var customer_name;
            if($scope.txtremarks==undefined){
                Notify.alert('Enter the Remarks','warning');
                return;
            }
            if($scope.decision=='Pushback'){
            
                emp_gid=$scope.rmemployee_gid;
                emp_name=$scope.rmemployee_name;
                
            }

            if($scope.decision=='Forward'){
                if($scope.forward_to==undefined){
                    Notify.alert('Select the forward to person','warning');
                    return;
                }
                emp_gid=$scope.forward_to;
                emp_name=$('#forward_to :selected').text();
            }

            if($scope.decision=='Archival'){
                if($scope.customer==undefined){
                    Notify.alert('Select the Customer','warning');
                    return;
                }
                emp_gid='';
                emp_name='';
                $scope.mailcontent='No Content';
            }

            if($scope.decision=='Close'){
                emp_gid='';
                emp_name='';
                $scope.mailcontent='No Content';
            }
            if($scope.customer==undefined){
                customer_gid='';
                customer_name='';
            }
            else{
                customer_gid=$scope.customer,
                customer_name=$('#customer :selected').text()
            }

            var params={
                email_gid:localStorage.getItem("email_gid"),
                decision:$scope.decision,
                employee_gid:emp_gid,
                employee_name:emp_name,
                remarks:$scope.txtremarks,
                mailcontent:'test',
                customer_gid:customer_gid,
                customer_name:customer_name,
                subject:$scope.pushback_subject,
                tomail_id:$scope.tomail_pushback,
                ccmail_id:$scope.cc_pushback,
                bccmail_id:$scope.bcc_pushback
            }
            var url='api/IasnTrnWorkItem/PostDecision';
            lockUI();
            SocketService.post(url,params).then(function (resp) {
                unlockUI();
                if(resp.data.status==true){
                    Notify.alert(resp.data.message,'success')

                    var params={
                        lsemail_gid:localStorage.getItem("email_gid")
                    };

                    var url="api/IasnTrnWorkItem/DecisionHistory";
                    SocketService.getparams(url,params).then(function (resp) {
                        if(resp.data.status==true){
                            $scope.decisionHistoryList=resp.data.MdlDecisionhistory;
                        }
                        else{
        
                        }
                    });
                 activate();
                }
                else{
                    Notify.alert(resp.data.message,'warning')
                }
            });


        }

        $scope.UploadDocCancel = function (id) {
            var params = {
                mailattachment_gid: id
            }
            var url = 'api/IasnTrnWorkItem/DeleteAttchment';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    
                    Notify.alert('Document deleted Successfully..!!', 'success')

                    var url = 'api/IasnTrnWorkItem/MailAttchment';

                    SocketService.get(url).then(function (resp) {

                        $scope.uploaddocument = resp.data.MdlDocDetails;

                    });
                }
                else {
                   
                    Notify.alert('Error Occurred')

                }

            });
        }
         $scope.decisionchange=function(val){
              $scope.all=true;
             if(val=="Pushback"){
                $scope.pushback=true;
                $scope.forward=false;
               
                $scope.archival=false;
                $scope.IsVisibleemployee = false;

                $scope.cc_pushback=$scope.to_mail+";"+$scope.cc_mail;
                $scope.pushback_subject="Pushback : "+$scope.email_subject;
                $scope.lsShowPushbackCC=true;

                $scope.tomail_pushback=$scope.rmemployee_mailid;
              
             }
             if(val=="Forward"){
                $scope.pushback=false;
                $scope.forward=true; 
                $scope.archival=false;
                $scope.IsVisibleemployee = true;
                $scope.lsShowPushbackCC=true;
                $scope.tomail_pushback="";
                $scope.cc_pushback=$scope.to_mail+";"+$scope.cc_mail;
                $scope.pushback_subject="Forward : "+$scope.email_subject;
            }
            if(val=="Close"){
                $scope.pushback=false;
                $scope.forward=false; 
                $scope.archival=false;
                $scope.IsVisibleemployee = false;
            }
           
           
            if(val=="Archival"){
                $scope.pushback=false;
                $scope.forward=false;
                $scope.archival=true;
                $scope.IsVisibleemployee = false;
            }
         }

         $scope.mergeworkitem = function (email_gid,subject,ref_no) {
            localStorage.setItem('email_gid',email_gid);
            localStorage.setItem('email_subject',subject);
            localStorage.setItem('workitemref_no',ref_no);
           if($scope.page == undefined) 
           {
            localStorage.setItem('page',$scope.page );    
           }   
           else if ($scope.page == 'archival')
           {
            localStorage.setItem('page','archival' );  
           }
           else
           {
             localStorage.setItem('page','workitem' );   
           }
           
            $state.go('app.iasnWomMergeWorkItem');
        }
         $scope.forwardtochange=function(val){
          
            var url="api/IasnTrnWorkItem/EmployeeEmailID";
            var params={
                employee_gid:val
            }
            SocketService.getparams(url,params).then(function (resp) {
                
                    $scope.tomail_pushback=resp.data.employee_emailid;
             
            });
         }
        
         $scope.signature = function () {
            var modalInstance = $modal.open({
                templateUrl: '/signatureContent.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false,
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
                    var params={
                        emailsignature:$scope.EmailSignature
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

        $scope.archivalWI = function () {
            var modalInstance = $modal.open({
                templateUrl: '/archivalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false,
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

               $scope.complete=function(string){
                
                    if(string.length >=3){
                     $scope.message="";
                     var url = 'api/customer/ExploreCustomer';
                     var params={
                          customername:string 
                      }
                      SocketService.getparams(url,params).then(function (resp) {
                          if(resp.data.status==true){
                             $scope.message="";
                             $scope.customer_list = resp.data.Customers;
                          }
                          else{
                              $scope.customer="";
                             $scope.message="No Records";
                          }
                         
                         
                  });
            }
            else{
             $scope.customer_list=null;
                $scope.message="Type atleast three character";
            }
                 }
        
               $scope.fillTextbox=function(customer_gid,customer_name){
           
               $scope.customer=customer_name;
               $scope. customer_gid=customer_gid;
               $scope.customer_list=null;

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

                $scope.ArchivalSubmit=function(){
                    var WorkItem_List = [];
                    var email_gid;
                    var sanctionref_no='';
                    var sanction_gid='';
                  
                    email_gid=localStorage.getItem("email_gid")
                    WorkItem_List.push(email_gid);
                    if($scope.archival.types_of_archival=='Specific'){
                      if($scope.cbosanctionrefno == undefined){
                        modalInstance.close('closed');
                        Notify.alert('Select the Sanction Ref No.','warning');
                        return;
                      }
                      else{
                        sanctionref_no=$('#sanction :selected').text();
                        sanction_gid=$scope.cbosanctionrefno.sanction_Gid;
                      }
                    }
                    
                   
                    var params={
                        email_gid: WorkItem_List,
                        archival_type:$scope.archival.types_of_archival,
                        remarks: $scope.archival.Remarks,
                        customer_gid:$scope.customer_gid,
                        customer_name:$scope.customer,
                        sanctionref_no:sanctionref_no,
                        sanction_gid:sanction_gid
                    }
                
                        var url = 'api/IasnTrnWorkItem/PostArchival';
                        lockUI()
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {                              
                                modalInstance.close('closed');
                                Notify.alert(resp.data.message, 'success')  
                                if ($scope.page=='archival')
                                {
                                   $state.go("app.iasnTrnArchivalSummary");
                                }
                               else
                               {
                                    $state.go("app.iasnTrnWorkItemSummary");
                               }
                            }
                            else {
                                
                                modalInstance.close('closed');
                                Notify.alert(resp.data.message)
                                if ($scope.page=='archival')
                                {
                                   $state.go("app.iasnTrnArchivalSummary");
                                }
                               else
                               {
                                    $state.go("app.iasnTrnWorkItemSummary");
                               }
                            }                       
                      
                        });
                   
                }
            }
        }

        $scope.pushback=function(){
            $scope.ccMail=$scope.cc_mail;
           localStorage.setItem('email_gid',localStorage.getItem("email_gid"));
           localStorage.setItem('toMail',$scope.rmemployee_mailid);
           localStorage.setItem('ccMail', $scope.ccMail);
           localStorage.setItem('bccMail', $scope.bcc_mail);
           localStorage.setItem('email_subject',$scope.email_subject);
           localStorage.setItem('message_id',$scope.message_id);
           localStorage.setItem('reference_id',$scope.reference_id);
           localStorage.setItem('rmemployee_gid', $scope.rmemployee_gid);
           localStorage.setItem('rmemployee_name', $scope.rmemployee_name);
           localStorage.setItem('originalmail_Subject', $scope.originalmail_Subject);
           localStorage.setItem('decision', 'Pushback');
           localStorage.setItem('lspage', 'workitem');
           $state.go('app.composeMail');
         
       }
       
       $scope.forward=function(){
           $scope.ccMail=$scope.cc_mail;
          localStorage.setItem('email_gid',localStorage.getItem("email_gid"));
          localStorage.setItem('toMail',$scope.rmemployee_mailid);
          localStorage.setItem('ccMail', $scope.ccMail);
          localStorage.setItem('bccMail', $scope.bcc_mail);
          localStorage.setItem('email_subject',$scope.email_subject);
          localStorage.setItem('message_id',$scope.message_id);
          localStorage.setItem('reference_id',$scope.reference_id);
          localStorage.setItem('rmemployee_gid', $scope.rmemployee_gid);
          localStorage.setItem('rmemployee_name', $scope.rmemployee_name);
          localStorage.setItem('originalmail_Subject', $scope.originalmail_Subject);
           localStorage.setItem('decision', 'Forward');
           localStorage.setItem('lspage', 'workitem');
           $state.go('app.composeMail');
       
        
       }
       $scope.back = function () {
           if ($scope.page == 'Workitem')
           {
               $state.go("app.iasnTrnWorkItemSummary");
           }
           else if($scope.page == 'Allotted')
           {
               $state.go("app.iasnTrnAllotedSummary");
           }
           else if ($scope.page == 'Pushback')
           {
               $state.go("app.iasnTrnPushbackSummary");
           }
           else if ($scope.page == 'Forward')
           {
               $state.go("app.iasnTrnForwardSummary");
           }
           else if ($scope.page == 'Close')
           {
               $state.go("app.iasnTrnCloseSummary");
           }
           else if ($scope.page == 'Archival')
           {
               $state.go("app.iasnTrnArchivalSummary");
           }
         }

       $scope.forwardNew_Mail = function (val, val1, val2, val3, val4) {
           $scope.ccMail = $scope.cc_mail;
           localStorage.setItem('composemail_gid', val);
           localStorage.setItem('toMail', val2);
           localStorage.setItem('ccMail', val3);
           localStorage.setItem('bccMail', val4);
           localStorage.setItem('email_subject', val1);
           localStorage.setItem('decision', 'Forward');
           localStorage.setItem('lspage', 'Archival');
           $state.go('app.iasnTrnForwardMail');
       }

       $scope.archivalNew_Mail = function (val) {
           var modalInstance = $modal.open({
               templateUrl: '/archivalContent.html',
               controller: ModalInstanceCtrl,
               size: 'md',
               backdrop: 'static',
               keyboard: false,
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
                       if ($scope.page == 'Archival') {
                           $state.go("app.iasnTrnArchivalSummary");
                       }
                       else {
                           $state.go("app.iasnWomWorkOrderSummary");
                       }
                   });

               }
           }
       }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnWorkItemAllotted360', iasnTrnWorkItemAllotted360);

    iasnTrnWorkItemAllotted360.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function iasnTrnWorkItemAllotted360($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnWorkItemAllotted360';
        activate();

        function activate() {
            $scope.IsVisibleteam = false;
            $scope.IsVisibleemployee = false;
            $scope.pushback = false;
            $scope.forward = false;
            $scope.all = false;
            $scope.archival = false;
            $scope.typeE = "";
            $scope.IsLogShow = false;

            $scope.email_gid = localStorage.getItem('email_gid');
            $scope.page = localStorage.getItem('page');

            var url = 'api/IasnTrnWorkItem/WorkItemView';
            var params = {
                lsemail_gid: localStorage.getItem('email_gid')
            };

            SocketService.getparams(url, params).then(function (resp) {

                $scope.email_from = resp.data.email_from;
                $scope.email_date = resp.data.email_date;
                $scope.email_subject = resp.data.email_subject;
                $scope.email_content = resp.data.email_content;
                $scope.created_date = resp.data.created_date;
                $scope.cc_mail = resp.data.cc;
                $scope.bcc_mail = resp.data.bcc;
                $scope.to_mail = resp.data.email_to;
                $scope.workitemref_no = resp.data.workitemref_no;
                $scope.zone_gid = resp.data.zone_gid;
                $scope.zone_name = resp.data.zone_name;
                $scope.rmemployee_gid = resp.data.rmemployee_gid;
                $scope.rmemployee_name = resp.data.rmemployee_name;
                $scope.rmemployee_mailid = resp.data.email_address;
                $scope.checkeremployee_name = resp.data.checkeremployee_name;
                $scope.attch_list = resp.data.MdlAttachmentList;
                $scope.allottedby_on = resp.data.allottedby_on;
                $scope.aging = resp.data.aging;
                $scope.status = resp.data.status;
                $scope.archivalremarks = resp.data.archivalremarks;
                $scope.assigned_remarks = resp.data.assigned_remarks;
                $scope.originalmail_Subject = resp.data.originalmail_Subject;
                $scope.hold_flag = resp.data.hold_flag;
                $scope.workitemhold_reason = resp.data.workitemhold_reason;
                $scope.customer_name = resp.data.customer_name;
                $scope.customer_type = resp.data.customer_type;

                if ($scope.archivalremarks == '' || $scope.archivalremarks == null) {
                    $scope.archiverem = false;
                }
                else {
                    $scope.archiverem = true;
                }
                $scope.closedremarks = resp.data.closedremarks;
                if ($scope.closedremarks == '' || $scope.closedremarks == null) {
                    $scope.closerem = false;
                }
                else {
                    $scope.closerem = true;
                }
                $scope.updatedby_on = resp.data.updatedby_on;
                $scope.message_id = resp.data.message_id;
                $scope.reference_id = resp.data.reference_id;

                if (resp.data.employee_gid != null) {

                    $scope.assign_to = resp.data.employee_gid;

                }


            });


            var params = {
                email_gid: localStorage.getItem("email_gid")
            };

            var url = "api/IasnTrnWorkItem/ReferenceMail";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.referenceMail = resp.data.MdlReferenceMail;
                    console.log(resp.data.MdlReferenceMail)
                    // $scope.attch_list=resp.data.MdlAttachmentList;  
                }
                else {

                }
            });




            var url = 'api/IasnTrnAuditLog/PostAuditView';

            var params = {
                email_gid: localStorage.getItem("email_gid")
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


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
        $scope.AssignWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_gid, zone_name, originalmail_Subject) {

            var modalInstance = $modal.open({
                templateUrl: '/assignWIContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });


            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;
                $scope.originalmail_Subject = originalmail_Subject;

                var url = "api/IasnMstZone/ZoneSummary";
                SocketService.get(url).then(function (resp) {

                    $scope.zone_list = resp.data.MdlZoneSummary;

                });
                if (zone_gid == undefined || zone_gid == "") {
                    $scope.zone_flag = "N"

                }
                else {
                    $scope.ddlzone_name = zone_gid;
                    $scope.lblzonename = zone_name;
                    $scope.zone_flag = "Y"
                }

                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;

                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.AssignToUpdate = function () {

                    if ($scope.ddlassign_to == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        email_gid: email_gid,
                        employee_gid: $scope.ddlassign_to.employee_gid,
                        employee_name: $('#ddlassign_to :selected').text(),
                        zone_gid: $scope.ddlzone_name,
                        zone_name: $('#ddlzone_name :selected').text(),
                        zone_flag: $scope.zone_flag
                    }

                    var url = "api/IasnTrnWorkItem/AssignTo";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            if ($scope.page == 'archival') {
                                $state.go("app.iasnTrnArchivalSummary");
                            }
                            else {
                                $state.go("app.iasnWomWorkOrderSummary");
                            }
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            if ($scope.page == 'archival') {
                                $state.go("app.iasnTrnArchivalSummary");
                            }
                            else {
                                $state.go("app.iasnWomWorkOrderSummary");
                            }
                        }

                    });

                }
            }
        }

        $scope.logdetails = function () {

            if ($scope.IsLogShow == true) {
                $scope.IsLogShow = false;
            }
            else {
                $scope.IsLogShow = true;

                var url = 'api/IasnTrnWorkItem/TransferLog';

                var params = {
                    lsemail_gid: localStorage.getItem("email_gid")
                };

                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        if ($scope.transferlog_list == null) {
                            $scope.transfershow = true;
                        }
                        else {
                            $scope.transfershow = false;
                        }
                    }
                    else {

                    }

                });

                var url = 'api/IasnTrnAuditLog/AuditLog';

                var params = {
                    email_gid: localStorage.getItem("email_gid")
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
        $scope.TransferWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_gid, zone_name, assign_to, originalmail_Subject) {

            var modalInstance = $modal.open({
                templateUrl: '/transferWIContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });


            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;
                $scope.zone_name = zone_name;
                $scope.checkeremployee_name = assign_to;
                $scope.originalmail_Subject = originalmail_Subject;

                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;

                });

                var params = {
                    lsemail_gid: email_gid
                }
                var url = 'api/IasnTrnWorkItem/TransferLog';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        $scope.showtransfer = true;
                    }
                    else {
                        $scope.showtransfer = false;
                    }


                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.transferWIUpdate = function () {

                    if ($scope.transfer_to == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        email_gid: localStorage.getItem("email_gid"),
                        employee_gid: $scope.transfer_to,
                        employee_name: $('#transfer_to :selected').text(),
                        zone_gid: '',
                        zone_name: '',
                        zone_flag: 'Y'
                    }

                    var url = "api/IasnTrnWorkItem/AssignTo";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            if ($scope.page == 'archival') {
                                $state.go("app.iasnTrnArchivalSummary");
                            }
                            else {
                                $state.go("app.iasnWomWorkOrderSummary");
                            }
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            if ($scope.page == 'archival') {
                                $state.go("app.iasnTrnArchivalSummary");
                            }
                            else {
                                $state.go("app.iasnWomWorkOrderSummary");
                            }
                        }
                    });

                }
            }
        }

        $scope.CloseWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name, originalmail_Subject) {
            var modalInstance = $modal.open({
                templateUrl: '/closeWIContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;
                $scope.originalmail_Subject = originalmail_Subject;

                $scope.CloseWIUpdate = function () {

                    var params = {
                        email_gid: localStorage.getItem("email_gid"),
                        decision: 'Close',
                        employee_gid: '',
                        employee_name: '',
                        remarks: $scope.close_remarks,
                        close_acknowledge: $scope.Acknowledge_mail_trigger,
                        mailcontent: 'Close',
                        customer_gid: '',
                        customer_name: '',
                        subject: '',
                        tomail_id: '',
                        ccmail_id: '',
                        bccmail_id: ''
                    }


                    var url = 'api/IasnTrnWorkItem/PostDecision';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')
                            if ($scope.page == 'archival') {
                                $state.go("app.iasnTrnArchivalSummary");
                            }
                            else {
                                $state.go("app.iasnWomWorkOrderSummary");
                            }

                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning')
                            if ($scope.page == 'archival') {
                                $state.go("app.iasnTrnArchivalSummary");
                            }
                            else {
                                $state.go("app.iasnWomWorkOrderSummary");
                            }
                        }

                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
        $scope.uploadattachment = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('project_flag', "Default");



            $scope.uploadfrm = frm;
            var url = 'api/IasnTrnWorkItem/MailAttchment';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                unlockUI();
                $("#addupload").val('');
                $scope.txtdocument_title = '';
                console.log(resp.data);
                if (resp.data.status == true) {

                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                    var url = 'api/IasnTrnWorkItem/MailAttchment';

                    SocketService.get(url).then(function (resp) {

                        $scope.uploaddocument = resp.data.MdlDocDetails;

                    });
                }
                else {

                    Notify.alert('File Format Not Supported!')

                }

            });

        }

        $scope.transferto_change = function (val) {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to transfer the work item?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes,Transfer it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var params = {
                        email_gid: localStorage.getItem("email_gid"),
                        employee_gid: val,
                        employee_name: $('#transfer_to :selected').text()
                    }
                    var url = "api/IasnTrnWorkItem/AssignTo";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Work Item Transfered Successfully!');

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
                else {
                    SweetAlert.swal('Error Occured');
                }

            });
        }

        $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

        $scope.onchangecopy = function (val) {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to move the work item to yours bin?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes,Move it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var params = {
                        email_gid: localStorage.getItem("email_gid"),
                        employee_gid: null,
                        employee_name: null
                    }
                    var url = "api/IasnTrnWorkItem/AssignTo";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Work Item Moved to Your Bin Successfully!');

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
                else {
                    SweetAlert.swal('Error Occured');
                }

            });
        }
        $scope.assignto_change = function (val) {
            var params = {
                email_gid: localStorage.getItem("email_gid"),
                employee_gid: val,
                employee_name: $('#assign_to :selected').text()
            }
            lockUI();
            var url = "api/IasnTrnWorkItem/AssignTo";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, 'success');
                }
                else {
                    Notify.alert(resp.data.message, 'warning');
                }
            });

        }
        $scope.updateDesicion = function () {
            var emp_gid;
            var emp_name;
            var customer_gid;
            var customer_name;
            if ($scope.txtremarks == undefined) {
                Notify.alert('Enter the Remarks', 'warning');
                return;
            }
            if ($scope.decision == 'Pushback') {
                //   console.log(mailConent.pushbackcontent);
                //     if(mailConent.pushbackcontent==undefined){
                //         console.log(mailConent.pushbackcontent);
                //         Notify.alert('Enter the compose Mail Content','warning');
                //         return;
                //     }



                emp_gid = $scope.rmemployee_gid;
                emp_name = $scope.rmemployee_name;

            }

            if ($scope.decision == 'Forward') {
                if ($scope.forward_to == undefined) {
                    Notify.alert('Select the forward to person', 'warning');
                    return;
                }
                emp_gid = $scope.forward_to;
                emp_name = $('#forward_to :selected').text();
            }

            if ($scope.decision == 'Archival') {
                if ($scope.customer == undefined) {
                    Notify.alert('Select the Customer', 'warning');
                    return;
                }
                emp_gid = '';
                emp_name = '';
                $scope.mailcontent = 'No Content';
            }

            if ($scope.decision == 'Close') {
                emp_gid = '';
                emp_name = '';
                $scope.mailcontent = 'No Content';
            }
            if ($scope.customer == undefined) {
                customer_gid = '';
                customer_name = '';
            }
            else {
                customer_gid = $scope.customer,
                customer_name = $('#customer :selected').text()
            }

            var params = {
                email_gid: localStorage.getItem("email_gid"),
                decision: $scope.decision,
                employee_gid: emp_gid,
                employee_name: emp_name,
                remarks: $scope.txtremarks,
                mailcontent: 'test',
                customer_gid: customer_gid,
                customer_name: customer_name,
                subject: $scope.pushback_subject,
                tomail_id: $scope.tomail_pushback,
                ccmail_id: $scope.cc_pushback,
                bccmail_id: $scope.bcc_pushback
            }
            console.log(params);

            var url = 'api/IasnTrnWorkItem/PostDecision';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, 'success')

                    var params = {
                        lsemail_gid: localStorage.getItem("email_gid")
                    };

                    var url = "api/IasnTrnWorkItem/DecisionHistory";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.decisionHistoryList = resp.data.MdlDecisionhistory;
                        }
                        else {

                        }
                    });
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, 'warning')
                }
            });


        }

        $scope.UploadDocCancel = function (id) {
            var params = {
                mailattachment_gid: id
            }
            var url = 'api/IasnTrnWorkItem/DeleteAttchment';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert('Document deleted Successfully..!!', 'success')

                    var url = 'api/IasnTrnWorkItem/MailAttchment';

                    SocketService.get(url).then(function (resp) {

                        $scope.uploaddocument = resp.data.MdlDocDetails;

                    });
                }
                else {

                    Notify.alert('Error Occurred')

                }

            });
        }
        $scope.decisionchange = function (val) {
            $scope.all = true;
            if (val == "Pushback") {
                $scope.pushback = true;
                $scope.forward = false;

                $scope.archival = false;
                $scope.IsVisibleemployee = false;

                $scope.cc_pushback = $scope.to_mail + ";" + $scope.cc_mail;
                $scope.pushback_subject = "Pushback : " + $scope.email_subject;
                $scope.lsShowPushbackCC = true;

                $scope.tomail_pushback = $scope.rmemployee_mailid;

            }
            if (val == "Forward") {
                $scope.pushback = false;
                $scope.forward = true;
                $scope.archival = false;
                $scope.IsVisibleemployee = true;
                $scope.lsShowPushbackCC = true;
                $scope.tomail_pushback = "";
                $scope.cc_pushback = $scope.to_mail + ";" + $scope.cc_mail;
                $scope.pushback_subject = "Forward : " + $scope.email_subject;
            }
            if (val == "Close") {
                $scope.pushback = false;
                $scope.forward = false;
                $scope.archival = false;
                $scope.IsVisibleemployee = false;
            }


            if (val == "Archival") {
                $scope.pushback = false;
                $scope.forward = false;
                $scope.archival = true;
                $scope.IsVisibleemployee = false;
            }
        }

        $scope.mergeworkitem = function (email_gid, subject, ref_no) {
            localStorage.setItem('email_gid', email_gid);
            localStorage.setItem('email_subject', subject);
            localStorage.setItem('workitemref_no', ref_no);
            if ($scope.page == undefined) {
                localStorage.setItem('page', $scope.page);
            }
            else if ($scope.page == 'archival') {
                localStorage.setItem('page', 'archival');
            }
            else {
                localStorage.setItem('page', 'workitem');
            }

            $state.go('app.iasnWomMergeWorkItem');
        }
        $scope.forwardtochange = function (val) {

            var url = "api/IasnTrnWorkItem/EmployeeEmailID";
            var params = {
                employee_gid: val
            }
            SocketService.getparams(url, params).then(function (resp) {

                $scope.tomail_pushback = resp.data.employee_emailid;

            });
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

        $scope.archivalWI = function () {
            var modalInstance = $modal.open({
                templateUrl: '/archivalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {

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
                    var WorkItem_List = [];
                    var email_gid;
                    var sanctionref_no = '';
                    var sanction_gid = '';

                    email_gid = localStorage.getItem("email_gid")
                    WorkItem_List.push(email_gid);
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
                        email_gid: WorkItem_List,
                        archival_type: $scope.archival.types_of_archival,
                        remarks: $scope.archival.Remarks,
                        customer_gid: $scope.customer_gid,
                        customer_name: $scope.customer,
                        sanctionref_no: sanctionref_no,
                        sanction_gid: sanction_gid
                    }

                    var url = 'api/IasnTrnWorkItem/PostArchival';
                    lockUI()
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')
                            if ($scope.page == 'archival') {
                                $state.go("app.iasnTrnArchivalSummary");
                            }
                            else {
                                $state.go("app.iasnWomWorkOrderSummary");
                            }
                        }
                        else {

                            modalInstance.close('closed');
                            Notify.alert(resp.data.message)
                            if ($scope.page == 'archival') {
                                $state.go("app.iasnTrnArchivalSummary");
                            }
                            else {
                                $state.go("app.iasnWomWorkOrderSummary");
                            }
                        }

                    });

                }
            }
        }

        $scope.pushback = function () {
            $scope.ccMail = $scope.cc_mail;
            // $scope.pushback_subject="Pushback : "+$scope.email_subject;
            // $scope.lsShowPushbackCC=true;
            // $scope.tomail=$scope.rmemployee_mailid;

            localStorage.setItem('email_gid', localStorage.getItem("email_gid"));
            localStorage.setItem('toMail', $scope.rmemployee_mailid);
            localStorage.setItem('ccMail', $scope.ccMail);
            localStorage.setItem('bccMail', $scope.bcc_mail);
            localStorage.setItem('email_subject', $scope.email_subject);
            localStorage.setItem('message_id', $scope.message_id);
            localStorage.setItem('reference_id', $scope.reference_id);
            localStorage.setItem('rmemployee_gid', $scope.rmemployee_gid);
            localStorage.setItem('rmemployee_name', $scope.rmemployee_name);
            localStorage.setItem('decision', 'Pushback');
            localStorage.setItem('lspage', 'workitem');
            $state.go('app.composeMail');

        }

        $scope.forward = function () {
            $scope.ccMail = $scope.cc_mail;
            // $scope.pushback_subject="Pushback : "+$scope.email_subject;
            // $scope.lsShowPushbackCC=true;
            // $scope.tomail=$scope.rmemployee_mailid;
            localStorage.setItem('email_gid', localStorage.getItem("email_gid"));
            localStorage.setItem('toMail', $scope.rmemployee_mailid);
            localStorage.setItem('ccMail', $scope.ccMail);
            localStorage.setItem('bccMail', $scope.bcc_mail);
            localStorage.setItem('email_subject', $scope.email_subject);
            localStorage.setItem('message_id', $scope.message_id);
            localStorage.setItem('reference_id', $scope.reference_id);
            localStorage.setItem('rmemployee_gid', $scope.rmemployee_gid);
            localStorage.setItem('rmemployee_name', $scope.rmemployee_name);
            localStorage.setItem('decision', 'Forward');
            localStorage.setItem('lspage', 'myworkitem');
            $state.go('app.composeMail');


        }
        $scope.back = function () {
            $state.go("app.iasnTrnAllotedSummary");
        }


    }
})();

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
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnWorkItemSummary', iasnTrnWorkItemSummary);

    iasnTrnWorkItemSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnTrnWorkItemSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnWorkItemSummary';

        activate();
        lockUI();
        function activate() {
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummary')
            }
            $scope.page = localStorage.getItem('page');
            var url = 'api/IasnTrnWorkItem/WorkItemCounts';
            SocketService.get(url).then(function (resp) {
                $scope.count_workitempending = resp.data.count_workitempending;
                $scope.count_workitemassigned = resp.data.count_workitemassigned;
                $scope.count_pushback = resp.data.count_pushback;
                $scope.count_forward = resp.data.count_forward;
                $scope.count_close = resp.data.count_close;
                $scope.count_archival = resp.data.count_archival;
                $scope.count_composemail = resp.data.count_composemail;

            });
            var url = 'api/IasnTrnWorkItem/WorkItemPendingSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });

            var url = 'api/IasnTrnWorkItem/WorkItemSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemAllotted_List = resp.data.MdlWorkItem;

                if ($scope.WorkItemAllotted_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemAllotted_List.length;
                    if ($scope.WorkItemAllotted_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemAllotted_List.length;
                    }
                }
            });
            var url = 'api/IasnTrnWorkItem/WorkItemForwardSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemForward_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemForward_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemForward_List.length;
                    if ($scope.WorkItemForward_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemForward_List.length;
                    }
                }

            });
        }

        $scope.refresh = function () {
            lockUI();
            activate();
        }

        $scope.EmployeeProfile = function (emp_gid) {
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params = {
                employee_gid: emp_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.user_code = resp.data.user_code;
                    $scope.user_name = resp.data.user_name;
                    $scope.user_photo = resp.data.user_photo;
                    $scope.user_designation = resp.data.user_designation;
                    $scope.user_department = resp.data.user_department;
                    $scope.user_mobileno = resp.data.user_mobileno;
                }
                else {
                    $scope.user_code = "-";
                    $scope.user_name = "-";
                    $scope.user_photo = "N";
                    $scope.user_designation = "-";
                    $scope.user_department = "-";
                }
            });

        }
        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };
        // Action Work Item 360
        $scope.WorkItem360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', $scope.page)
            }
            localStorage.setItem('page', 'Workitem')
            $state.go("app.iasnTrnWorkItem360");
        }

        // Action Work Item Allotted 360
        $scope.WorkItemAllotted360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', $scope.page)
            }

            $state.go("app.iasnTrnWorkItemAllotted360");
        }



        $scope.WorkItem = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });
        }

        $scope.Pushback = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemPushbackSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPushback_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPushback_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPushback_List.length;
                    if ($scope.WorkItemPushback_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPushback_List.length;
                    }
                }

            });
        }


        $scope.CloseTab = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemCloseSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemClose_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemClose_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemClose_List.length;
                    if ($scope.WorkItemClose_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemClose_List.length;
                    }
                }

            });
        }


        $scope.AssignZone = function (email_gid, workitemref_no, email_from, email_subject, zone_gid, zone_name) {

            var modalInstance = $modal.open({
                templateUrl: '/assignZoneContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });


            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;

                $scope.rdb_acks = "N";
                var url = "api/IasnMstZone/ZoneSummary";
                SocketService.get(url).then(function (resp) {

                    $scope.zone_list = resp.data.MdlZoneSummary;

                });

                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;

                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.AssignToUpdate = function () {


                    var params = {
                        email_gid: email_gid,
                        zone_gid: $scope.zone_name,
                        zone_name: $('#zone_name :selected').text(),
                        zone_flag: $scope.zone_flag,
                        acknowledgement_flag: $scope.rdb_acks,
                    }

                    var url = "api/IasnTrnWorkItem/AssignZone";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });

                }
            }
        }

        $scope.TransferWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name, assign_to) {

            var modalInstance = $modal.open({
                templateUrl: '/transferWIContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });


            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;
                $scope.zone_name = zone_name;
                $scope.checkeremployee_name = assign_to;

                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;

                });

                var params = {
                    lsemail_gid: email_gid
                }
                var url = 'api/IasnTrnWorkItem/TransferLog';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        $scope.showtransfer = true;
                    }
                    else {
                        $scope.showtransfer = false;
                    }


                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.transferWIUpdate = function () {

                    if ($scope.transfer_to == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        email_gid: email_gid,
                        employee_gid: $scope.transfer_to,
                        employee_name: $('#transfer_to :selected').text(),
                        zone_gid: '',
                        zone_name: '',
                        zone_flag: 'Y'
                    }

                    var url = "api/IasnTrnWorkItem/AssignTo";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });

                }
            }
        }

        $scope.CloseWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/closeWIContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;

                $scope.CloseWIUpdate = function () {

                    var params = {
                        email_gid: email_gid,
                        decision: 'Close',
                        employee_gid: '',
                        employee_name: '',
                        remarks: $scope.close_remarks,
                        close_acknowledge: $scope.Acknowledge_mail_trigger,
                        mailcontent: 'Close',
                        customer_gid: '',
                        customer_name: '',
                        subject: '',
                        tomail_id: '',
                        ccmail_id: '',
                        bccmail_id: ''
                    }


                    var url = 'api/IasnTrnWorkItem/PostDecision';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, 'success')
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning')
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
        // Add Work Item Code Ends

        // Merge Work Item Code Starts
        $scope.mergeworkitem = function (email_gid, subject, ref_no) {
            localStorage.setItem('email_gid', email_gid);
            localStorage.setItem('email_subject', subject);
            localStorage.setItem('workitemref_no', ref_no);

            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', 'workitemsummarypage')
            }

            $state.go('app.iasnWomMergeWorkItem');
        }
        // Merge Work Item Code Ends
        $scope.Alloted = function () {
            $state.go('app.iasnTrnAllotedSummary');
        }
        $scope.Pushback = function () {
            $state.go('app.iasnTrnPushbackSummary');
        }
        $scope.Forward = function () {
            $state.go('app.iasnTrnForwardSummary');
        }
        $scope.Close = function () {
            $state.go('app.iasnTrnCloseSummary');
        }
        $scope.WorkItem = function () {
            $state.go('app.iasnTrnWorkItemSummary');
        }
        $scope.ComposeMail = function () {
            $state.go('app.iasnWomWorkOrderSummary');
        }

        $scope.holdworkitem = function (email_gid, workitemref_no, email_from, email_subject) {
            var modalInstance = $modal.open({
                templateUrl: '/holdworkitempopup.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false,
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.email_gid = email_gid;
                $scope.workitemref_no = workitemref_no;
                $scope.from = email_from;
                $scope.email_subject = email_subject;

                var params = {
                    lsemail_gid: email_gid,
                    assigned_flag: 'N',
                }
                var url = 'api/IasnTrnWorkItem/HoldLogDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.holdlog_list = resp.data.MdlholdLog;
                        $scope.showholdworkitem = true;
                    }
                    else {
                        $scope.showholdworkitem = false;
                    }


                });

                $scope.HoldWISubmit = function () {
                    var params = {
                        email_gid: email_gid,
                        workitemhold_reason: $scope.hold_remarks,
                        assigned_flag: 'N',
                    }
                    var url = "api/IasnTrnWorkItem/HoldWorkItem";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            })
                            $modalInstance.close('closed');
                        }
                    });
                }

                $scope.close = function () {
                    $modalInstance.close('closed');
                };
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnWomMergeWorkItem', iasnWomMergeWorkItem);

        iasnWomMergeWorkItem.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce'];

    function iasnWomMergeWorkItem($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,$sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnWomMergeWorkItem';
        var lsemail_gid;
        activate();
        $scope.UnTaggedWI_List=undefined;
        $scope.ShowSearch=false;

        function activate() {
            $scope.page = localStorage.getItem('page')
            lsemail_gid=localStorage.getItem('email_gid')
            $scope.email_subject=localStorage.getItem('email_subject')
            $scope.workitemref_no=localStorage.getItem('workitemref_no');
         
            var params={
                email_gid:lsemail_gid
            }
            var url = 'api/IasnMergeWorkItem/TaggedWISummary';
            SocketService.getparams(url,params).then(function (resp) {
                $scope.TaggedWI_List = resp.data.MdlWorkItem;
                
                if($scope.TaggedWI_List!=null){
                    $scope.count_workitem=$scope.TaggedWI_List.length;
                    $scope.Tagtotal=$scope.TaggedWI_List.length;
                }

            });
        }

        $scope.complete_searchrm=function(string){
                
            if(string.length >=3){
             $scope.rmmessage="";
             var url = 'api/IasnMergeWorkItem/WIEmailFromList';
             var params={
                email_from:string 
              }
              SocketService.getparams(url,params).then(function (resp) {
                  if(resp.data.status==true){
                     $scope.rmmessage="";
                     $scope.rm_list = resp.data.MdlWISearch;
                  }
                  else{
                      $scope.searchrm_name="";
                     $scope.rmmessage="No Records";
                  }
                 
                 
          });
    }
    else{
     $scope.rm_list=null;
        $scope.rmmessage="Type atleast three character";
    }
           
        
         }
         $scope.fillTextboxRM=function(val){
         
             $scope.searchrm_name=val;
           
             $scope.rm_list=null;

    }


    $scope.complete_searchsubject=function(string){
                
        if(string.length >=3){
         $scope.subjectmessage="";
         var url = 'api/IasnMergeWorkItem/WISubjectList';
         var params={
            email_subject:string 
          }
          SocketService.getparams(url,params).then(function (resp) {
              if(resp.data.status==true){
                 $scope.subjectmessage="";
                 $scope.subject_list = resp.data.MdlWISearch;
              }
              else{
                $scope.searchsubject="";
                 $scope.subjectmessage="No Records";
              }
             
             
      });
}
else{
 $scope.subject_list=null;
    $scope.subjectmessage="Type atleast three character";
}
       
    
     }
     $scope.fillTextboxSubject=function(val){
     
         $scope.searchsubject=val;
       
         $scope.subject_list=null;

}

$scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
$scope.SearchWorkItem=function(){
    lockUI();
    $scope.ShowSearch=true;
    if($scope.searchrm_name==undefined){
        $scope.searchrm_name=''
    }

    if($scope.searchsubject==undefined){
        $scope.searchsubject=''
    }

    if($scope.searchzone_name==undefined){
        $scope.searchzone_name=''
    }

    var params={
        email_gid:localStorage.getItem('email_gid'),
        email_from:$scope.searchrm_name,
        email_subject:$scope.searchsubject,
        zone_name:$scope.searchzone_name
    }

    var url = 'api/IasnMergeWorkItem/UnTaggedWISummary';
    SocketService.post(url,params).then(function (resp) {
        $scope.UnTaggedWI_List = resp.data.MdlWorkItem;
        
        if($scope.UnTaggedWI_List!=null){
            $scope.count_UnTagworkitem=$scope.UnTaggedWI_List.length;
            $scope.UnTagTotal=$scope.UnTaggedWI_List.length;
        }

    });

    unlockUI();
}

$scope.MergeWorkItem=function(){
   
    if($scope.UnTaggedWI_List==undefined){
        Notify.alert('Search the Work Item')
       
        return;
    }
    var emailgid_list=[];
    var tocheck_data;
    angular.forEach($scope.UnTaggedWI_List, function (val) {

        if (val.checked == true) {
            tocheck_data=val.email_gid;
            emailgid_list.push(val.email_gid);

           
        }
    });
   
    if (tocheck_data ==undefined)
        {
        
        Notify.alert('Select Atleast One Record!')
       
        return;
    }

    var params={
        mergeemail_gid:emailgid_list,
        email_gid:localStorage.getItem('email_gid')
    }

    var url = 'api/IasnMergeWorkItem/WIMerge';
    lockUI();
    SocketService.post(url,params).then(function (resp) {
        $scope.UnTaggedWI_List=null;
        unlockUI();
        activate();
        if(resp.data.status==true){
            $scope.searchrm_name='';
            $scope.searchsubject='';
            $scope.searchzone_name='';
            Notify.alert(resp.data.message,'success')
        }
        else{
            Notify.alert(resp.data.message,'warning')
        }
    });

   
    
}

$scope.complete_searchzone=function(string){
                
    if(string.length >=3){
     $scope.zonemessage="";
     var url = 'api/IasnMergeWorkItem/WIZoneList';
     var params={
        zone_name:string 
      }
      SocketService.getparams(url,params).then(function (resp) {
          if(resp.data.status==true){
             $scope.zonemessage="";
             $scope.zone_list = resp.data.MdlWISearch;
          }
          else{
            $scope.searchzone_name="";
             $scope.zonemessage="No Records";
          }
         
         
  });
}
else{
$scope.zone_list=null;
$scope.zonemessage="Type atleast three character";
}
   

 }

 $scope.checkall = function (selected) {
    angular.forEach($scope.UnTaggedWI_List, function (val) {  
        
            val.checked = selected;
    });
}

$scope.back=function(){
    if($scope.page =="workitem"){
        $state.go("app.iasnTrnWorkItem360");

    }
    else if($scope.page =="workitemsummary"){
        localStorage.setItem = ('page','workitemsummary')
        $state.go("app.iasnTrnWorkItem360");     

    }
    else if($scope.page =="myworkitem")
    {
        $state.go("app.iasnTrnMyWorkItem360");

    }
    else if($scope.page =="MyWorkItemSummary"){
        localStorage.setItem = ('page','MyWorkItemSummary')
        $state.go("app.iasnTrnMyWorkItem360");
    }
    else if ($scope.page == "archival"){
        localStorage.setItem = ('page','archival')
        $state.go("app.iasnTrnWorkItem360");
    }
    else if($scope.page =="workitemsummarypage")
    {
        $state.go("app.iasnWomWorkOrderSummary");

    }
    else if($scope.page =="MyWorkItemSummarypage")
    {
        $state.go("app.iasnTrnMyWorkItemSummary");

    }
    
}

 $scope.fillTextboxZone=function(val){
 
     $scope.searchzone_name=val;
   
     $scope.zone_list=null;

}

$scope.EmployeeProfile=function(emp_gid){
    var url = 'api/IasnTrnWorkItem/EmployeeProfile';
    var params={
        employee_gid:emp_gid
    }
    SocketService.getparams(url,params).then(function (resp) {
       if(resp.data.status==true){
        $scope.user_code=resp.data.user_code;
        $scope.user_name=resp.data.user_name;
        $scope.user_photo=resp.data.user_photo;
        $scope.user_designation=resp.data.user_designation;
        $scope.user_department=resp.data.user_department;
        $scope.user_mobileno=resp.data.user_mobileno;
       }
       else{
        $scope.user_code="-";
        $scope.user_name="-";
        $scope.user_photo="N";
        $scope.user_designation="-";
        $scope.user_department="-";
       }
    });

}


        $scope.WorkItem360=function(val){
            localStorage.setItem('email_gid',val)
            $state.go("app.iasnTrnWorkItem360");
        }

        $scope.UndoMail = function (val) {
            var params = {
                email_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Undo the Mail Merge ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, Undo it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/IasnMergeWorkItem/WIUndoMerge";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Reverted!');
                            unlockUI();
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
       
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnWomWorkOrderSummary', iasnWomWorkOrderSummary);

        iasnWomWorkOrderSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnWomWorkOrderSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnWomWorkOrderSummary';

        activate();

        function activate() {
            lockUI();
            $scope.total=0;
            $scope.totalDisplayed=100;
            if ( $scope.page ==undefined)
            {
                localStorage.setItem('page','workitemsummary')
            }
            $scope.page = localStorage.getItem('page');
            var url = 'api/IasnTrnWorkItem/WorkItemCounts';
            SocketService.get(url).then(function (resp) {
                $scope.count_workitempending = resp.data.count_workitempending;
                $scope.count_workitemassigned=resp.data.count_workitemassigned;
                $scope.count_pushback = resp.data.count_pushback;
                $scope.count_forward=resp.data.count_forward;
                $scope.count_close = resp.data.count_close;
                $scope.count_archival=resp.data.count_archival;
                $scope.count_composemail = resp.data.count_composemail;
                
            });
         
            var url = 'api/IasnTrnWorkItem/ComposeMailSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.ComposeMail_List = resp.data.MdlWorkItem;
                if ($scope.ComposeMail_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.ComposeMail_List.length;
                    if ($scope.ComposeMail_List.length < 100) {
                        $scope.totalDisplayed = $scope.ComposeMail_List.length;
                    }
                }
            });
        }
            
        $scope.EmployeeProfile = function (emp_gid) {
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params = {
                employee_gid: emp_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.user_code = resp.data.user_code;
                    $scope.user_name = resp.data.user_name;
                    $scope.user_photo = resp.data.user_photo;
                    $scope.user_designation = resp.data.user_designation;
                    $scope.user_department = resp.data.user_department;
                    $scope.user_mobileno = resp.data.user_mobileno;
                }
                else {
                    $scope.user_code = "-";
                    $scope.user_name = "-";
                    $scope.user_photo = "N";
                    $scope.user_designation = "-";
                    $scope.user_department = "-";
                }
            });

        }
        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };
        // Action Work Item 360
        $scope.WorkItem360=function(val){
            localStorage.setItem('email_gid',val)
            var params={
                email_gid:val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url,params).then(function (resp) {
            });
            if ($scope.page ==undefined)
            {
                localStorage.setItem('page','workitemsummarypage')
            }
            else{
                localStorage.setItem('page',$scope.page)
            }
           
            $state.go("app.iasnTrnWorkItem360");
        }

        // Action Work Item Allotted 360
        $scope.WorkItemAllotted360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', $scope.page)
            }

            $state.go("app.iasnTrnWorkItemAllotted360");
        }

        $scope.Allotted=function(){
            var url = 'api/IasnTrnWorkItem/WorkItemSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemAllotted_List = resp.data.MdlWorkItem;
              
                if ($scope.WorkItemAllotted_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemAllotted_List.length;
                    if ($scope.WorkItemAllotted_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemAllotted_List.length;
                    }
                }
            });

        }
        
        $scope.WorkItem=function(){
            var url = 'api/IasnTrnWorkItem/WorkItemPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });
        }

        $scope.Pushback=function(){
            var url = 'api/IasnTrnWorkItem/WorkItemPushbackSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPushback_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPushback_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPushback_List.length;
                    if ($scope.WorkItemPushback_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPushback_List.length;
                    }
                }

            });
        }

        $scope.Forward=function(){
            var url = 'api/IasnTrnWorkItem/WorkItemForwardSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemForward_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemForward_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemForward_List.length;
                    if ($scope.WorkItemForward_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemForward_List.length;
                    }
                }

            });
        }

        $scope.CloseTab=function(){
            var url = 'api/IasnTrnWorkItem/WorkItemCloseSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemClose_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemClose_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemClose_List.length;
                    if ($scope.WorkItemClose_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemClose_List.length;
                    }
                }

            });
        }

       
        $scope.Alloted = function () {
            $state.go('app.iasnTrnAllotedSummary');
        }
        $scope.Pushback = function () {
            $state.go('app.iasnTrnPushbackSummary');
        }
        $scope.Forward = function () {
            $state.go('app.iasnTrnForwardSummary');
        }
        $scope.Close = function () {
            $state.go('app.iasnTrnCloseSummary');
        }
        $scope.WorkItem = function () {
            $state.go('app.iasnTrnWorkItemSummary');
        }
        $scope.composemaildt = function () {
            $state.go('app.iasnTrnWorkItemMail');
        }
        $scope.ComposeMail = function () {
            $state.go('app.iasnWomWorkOrderSummary');
        }

        $scope.archivalWI = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/archivalContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
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

        $scope.ComposeMail360 = function (val) {
            localStorage.setItem('composemail_gid', val)
            $state.go('app.iasnTrnComposeMail360');
        }

        $scope.forward = function (val, val1, val2, val3, val4) {
            $scope.ccMail = $scope.cc_mail;
            localStorage.setItem('composemail_gid', val);
            localStorage.setItem('toMail', val2);
            localStorage.setItem('ccMail', val3);
            localStorage.setItem('bccMail', val4);
            localStorage.setItem('email_subject', val1);
            localStorage.setItem('decision', 'Forward');
            localStorage.setItem('lspage', 'composemailsummary');
            $state.go('app.iasnTrnForwardMail');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('isanconsolidatedview', isanconsolidatedview);

    isanconsolidatedview.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function isanconsolidatedview($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'isanconsolidatedview';

        var email_gid = $location.search().lsemail_gid;
        $scope.email_gid = email_gid;
        var lstab = $location.search().lstab;

        activate();

        function activate() {
            $scope.IsVisibleteam = false;
            $scope.IsVisibleemployee = false;
            $scope.pushback = false;
            $scope.forward = false;
            $scope.all = false;
            $scope.archival = false;
            $scope.typeE = "";
            $scope.IsLogShow = false;

            var url = 'api/IasnTrnWorkItem/WorkItemView';
            var params = {
                lsemail_gid: $scope.email_gid
            };
            SocketService.getparams(url, params).then(function (resp) {

                $scope.email_from = resp.data.email_from;
                $scope.email_date = resp.data.email_date;
                $scope.email_subject = resp.data.email_subject;
                $scope.email_content = resp.data.email_content;
                $scope.created_date = resp.data.created_date;
                $scope.cc_mail = resp.data.cc;
                $scope.bcc_mail = resp.data.bcc;
                $scope.to_mail = resp.data.email_to;
                $scope.workitemref_no = resp.data.workitemref_no;
                $scope.zone_gid = resp.data.zone_gid;
                $scope.zone_name = resp.data.zone_name;
                $scope.rmemployee_gid = resp.data.rmemployee_gid;
                $scope.rmemployee_name = resp.data.rmemployee_name;
                $scope.rmemployee_mailid = resp.data.rmemployee_mailid;
                $scope.checkeremployee_name = resp.data.checkeremployee_name;
                $scope.attch_list = resp.data.MdlAttachmentList;
                $scope.allottedby_on = resp.data.allottedby_on;
                $scope.aging = resp.data.aging;
                $scope.status = resp.data.status;
                $scope.archivalremarks = resp.data.archivalremarks;
                if ($scope.archivalremarks == '' || $scope.archivalremarks == null) {
                    $scope.archiverem = false;
                }
                else {
                    $scope.archiverem = true;
                }
                $scope.closedremarks = resp.data.closedremarks;
                if ($scope.closedremarks == '' || $scope.closedremarks == null) {
                    $scope.closerem = false;
                }
                else {
                    $scope.closerem = true;
                }
                $scope.updatedby_on = resp.data.updatedby_on;
                $scope.message_id = resp.data.message_id;
                $scope.reference_id = resp.data.reference_id;

                if (resp.data.employee_gid != null) {

                    $scope.assign_to = resp.data.employee_gid;

                }


            });


            var params = {
                email_gid: $scope.email_gid
            };

            var url = "api/IasnTrnWorkItem/ReferenceMail";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.referenceMail = resp.data.MdlReferenceMail;
                    // $scope.attch_list=resp.data.MdlAttachmentList;  
                }
                else {

                }
            });




            var url = 'api/IasnTrnAuditLog/PostAuditView';

            var params = {
                email_gid: $scope.email_gid
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


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

        $scope.logdetails = function () {

            if ($scope.IsLogShow == true) {
                $scope.IsLogShow = false;
            }
            else {
                $scope.IsLogShow = true;

                var url = 'api/IasnTrnWorkItem/TransferLog';

                var params = {
                    lsemail_gid: $scope.email_gid
                };

                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        if ($scope.transferlog_list == null) {
                            $scope.transfershow = true;
                        }
                        else {
                            $scope.transfershow = false;
                        }
                    }
                    else {

                    }

                });

                var url = 'api/IasnTrnAuditLog/AuditLog';

                var params = {
                    email_gid: $scope.email_gid
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

        $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

        $scope.forwardtochange = function (val) {

            var url = "api/IasnTrnWorkItem/EmployeeEmailID";
            var params = {
                employee_gid: val
            }
            SocketService.getparams(url, params).then(function (resp) {

                $scope.tomail_pushback = resp.data.employee_emailid;

            });
        }

        $scope.back = function () {
            if (lstab == 'ConsolidatedReport') {
                $state.go("app.iasnTrnConsolidatedReport");
            }
            else {
                $state.go("app.iasnConsolidatedWorkItem");
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstTeamEdit', iasnMstTeamEdit);

        iasnMstTeamEdit.$inject =['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function iasnMstTeamEdit($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnMstTeamEdit';

        activate();

        function activate() {
          
            // var url = 'api/employee/employee';
            // SocketService.get(url).then(function (resp) {
            //     $scope.employee_list = resp.data.employee_list;
               
            // });
          
            var params={
                team_gid:localStorage.getItem('team_gid')
            }
            var url = 'api/IasnMstTeam/EditTeam';
            SocketService.getparams(url,params).then(function (resp) {
             
                $scope.TeamCode=resp.data.team_code;
                $scope.TeamName = resp.data.team_name;
                $scope.TeamMail=resp.data.team_mailid;
                $scope.zone=resp.data.zonal_name;
                $scope.description=resp.data.description;
                $scope.employee_list = resp.data.employee_list;
             
                if (resp.data.MdlRmList != null) {
                    $scope.rmlist = [];
                    var count = resp.data.MdlRmList.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.employee_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.MdlRmList[i].employee_gid);
                        $scope.rmlist.push($scope.employee_list[indexs]);
                    }
                }

                if (resp.data.MdlCheckerList != null) {
                    var count = resp.data.MdlCheckerList.length;
                    $scope.checkerlist = [];
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.employee_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.MdlCheckerList[i].employee_gid);
                        $scope.checkerlist.push($scope.employee_list[indexs]);
                    }
                }
            });

         }

         $scope.Update=function(){

            if($scope.TeamName==undefined){
                Notify.alert('Team Name is Mandatory','warning');
                return;
            }
            if($scope.TeamMail==undefined){
                Notify.alert('Team Mail is Mandatory','warning');
                return;
            }
            if($scope.description==undefined){
                Notify.alert('Description is Mandatory','warning');
                return;
            }
            if($scope.zone==undefined){
                Notify.alert('Zone is Mandatory','warning');
                return;
            }
            if($scope.rmlist==undefined){
                Notify.alert('Kindly select the RM List','warning');
                return;
            }
            if($scope.checkerlist==undefined){
                Notify.alert('Kindly select the Checker List','warning');
                return;
            }
           
             var params={
                team_gid:localStorage.getItem('team_gid'),
                team_name:$scope.TeamName,
                description:$scope.description,
                zonal_name:$scope.zone,
                team_mailid:$scope.TeamMail,
                MdlRmList:$scope.rmlist,
                MdlCheckerList:$scope.checkerlist
             }
            

             var url="api/IasnMstTeam/UpdateTeam";
             lockUI();
             SocketService.post(url,params).then(function (resp) {
                unlockUI();
               if(resp.data.status==true){
                   $state.go('app.iasnMstTeamManagement');
                Notify.alert(resp.data.message,'success');
               }
               else{
                Notify.alert(resp.data.message,'warning');
               }
            });
         }

         $scope.Back=function(){
             $state.go('app.iasnMstTeamManagement');
         }
    }
})();
