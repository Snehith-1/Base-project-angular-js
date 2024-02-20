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
