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
         lsdecision =localStorage.getItem('decision');
         $scope.send = true;
         $scope.close = true;
        
         console.log($scope.message_id);

         var url = 'api/IasnTrnEmailSignature/GetEmailSignature';
                SocketService.get(url).then(function (resp) {
                    
                    $scope.pushbackcontent=resp.data.emailsignature;   
                       
                });
            

             }
             $scope.uploadattachment = function (val,val1,name) {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
               
                
            
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
            $scope.closeDesicion = function(){
                $window.close();
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
              
                var url='api/IasnTrnWorkItem/PostDecision';
                lockUI();
                SocketService.post(url,params).then(function (resp) {
                    unlockUI();
                    if(resp.data.status==true){
                       
                        Notify.alert(resp.data.message,'success')
                          $scope.close = false;
                          $scope.send = false;             
                        if($scope.page == WokItem)
                        {
                           
                            $state.go('app.iasnWomWorkOrderSummary')
                        }
                        else
                        {
                           
                            $state.go("app.iasnTrnMyWorkItemSummary");
                        }
                     
                    }
                    else{
                        $scope.close = false;
                        $scope.send = false;               
                        Notify.alert(resp.data.message,'warning')                       
                     
                        if($scope.page == WokItem)
                        {                           
                            $state.go('app.iasnWomWorkOrderSummary')
                        }
                        else
                        {                          
                            $state.go("app.iasnTrnMyWorkItemSummary");
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

    }
})();
