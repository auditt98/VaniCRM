(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-98d203aa"],{"498a":function(s,t,r){"use strict";var o=r("23e7"),a=r("58a8").trim,e=r("c8d2");o({target:"String",proto:!0,forced:e("trim")},{trim:function(){return a(this)}})},"5e39":function(s,t,r){"use strict";r.r(t);var o=function(){var s=this,t=s.$createElement,r=s._self._c||t;return r("div",{staticClass:"w-100 login-page"},[r("div",{staticClass:"row mx-0"},[r("div",{staticClass:"col-sm-6 mx-auto pt-5"},[r("div",{staticClass:"col-sm-12 m-auto"},[s._m(0),r("form",{staticClass:"rơmt-2",on:{submit:function(t){return t.preventDefault(),s.onSubmit(t)}}},[r("div",{staticClass:"form-group row"},[r("label",{staticClass:"col-sm-4 col-form-label",attrs:{for:"inputPassword"}},[s._v("New password")]),r("div",{staticClass:"col-sm-8"},[r("input",{directives:[{name:"model",rawName:"v-model",value:s.password,expression:"password"}],staticClass:"form-control",class:{"input-error-red":s.error&&s.error.includes("NEW_PASS")},attrs:{type:"password",id:"inputPassword",placeholder:""},domProps:{value:s.password},on:{input:function(t){t.target.composing||(s.password=t.target.value)}}})])]),r("div",{staticClass:"form-group row"},[r("label",{staticClass:"col-sm-4 col-form-label",attrs:{for:"password"}},[s._v("Confirm new password")]),r("div",{staticClass:"col-sm-8"},[r("input",{directives:[{name:"model",rawName:"v-model",value:s.passwordConfirm,expression:"passwordConfirm"}],staticClass:"form-control",class:{"input-error-red":s.error&&s.error.includes("NEW_PASS_CONFIRM")},attrs:{type:"password",id:"password",placeholder:""},domProps:{value:s.passwordConfirm},on:{input:function(t){t.target.composing||(s.passwordConfirm=t.target.value)}}})])]),r("button",{staticClass:"btn btn-reset w-100 mt-2",attrs:{type:"submit"}},[s._v("Reset password")])])])])])])},a=[function(){var s=this,t=s.$createElement,r=s._self._c||t;return r("div",{staticClass:"mb-5"},[r("p",{staticClass:"text-center font-weight-bold login-text"},[s._v("Reset Password")]),r("p",{staticClass:"under-line w-50 mx-auto mb-5"}),r("p",{staticClass:"text-center"},[s._v("Enter your new password.")])])}],e=(r("498a"),r("bb4d")),i=r("a18c"),n={name:"ResetPassword",data:function(){return{password:"",passwordConfirm:"",key:"",error:Array(String)}},methods:{onSubmit:function(){this.error=[],this.password&&""!==this.password.trim()||this.error.push("NEW_PASS"),this.passwordConfirm&&""!==this.passwordConfirm.trim()||this.error.push("NEW_PASS_CONFIRM"),this.error.length>0||(this.password===this.passwordConfirm?e["a"].updateNewPass(this.password,this.key).then((function(s){s&&i["a"].push("/login")}),(function(s){console.log(s),alert(s)})):alert("Ko trung"))}},created:function(){if(e["a"].currentUserValue)return i["a"].push("/");this.key=this.$route.query.key||null,this.key||i["a"].push("/")}},l=n,c=(r("e185"),r("2877")),u=Object(c["a"])(l,o,a,!1,null,"64583a94",null);t["default"]=u.exports},"8fb8":function(s,t,r){},c8d2:function(s,t,r){var o=r("d039"),a=r("5899"),e="​᠎";s.exports=function(s){return o((function(){return!!a[s]()||e[s]()!=e||a[s].name!==s}))}},e185:function(s,t,r){"use strict";r("8fb8")}}]);
//# sourceMappingURL=chunk-98d203aa.91aa15f2.js.map