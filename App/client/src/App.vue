<template>
  <div id="app">
    <Header v-if="!['ResetPassword', 'LoginPage', 'About'].includes($route.name)" />
      <router-view :key="$route.fullPath"></router-view> 
    <!-- <router-view/> -->
    <notifications group="custom-template"
                   :duration="5000"
                   :width="500"
                   animation-name="v-fade-left"
                   position="bottom right">

       <template slot="body" slot-scope="props">
        <div class="custom-template">
          <div class="notiIndicator" :class="props.item.type"></div>

          <div class="custom-template-content">
            <div class="custom-template-title">
              {{props.item.title}}
            </div>
            <div class="custom-template-text"
                 v-html="props.item.text">
            </div>
          </div>
          <div class="custom-template-close"
               @click="props.close">
            <i class="fa fa-times" style="color: #D93915;" aria-hidden="true"></i>
          </div>
        </div>

      </template>
    </notifications>
  </div>
  
</template>

<style>
#app {
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
.custom-template {
	display: flex;

	flex-direction: row;
	flex-wrap: nowrap;
	text-align: left;
  height: fit-content;
	font-size: 13px;
  width: auto;
	margin-right: 10px;
	margin-bottom: 17px;
	align-items: center;
	justify-content: center;
	background: white;
  box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);

}

.custom-template:hover{
 background-color: rgba(255, 255, 255, 0.5);

    -webkit-backdrop-filter: blur(10px);
  backdrop-filter: blur(10px);
}

.custom-template,
.custom-template>div {
	box-sizing: border-box;
}

.custom-template .custom-template-icon {
	flex: 0 1 auto;
	font-size: 50px;
	padding: 0 10px;
}

.custom-template .custom-template-close {
	flex: 0 1 auto;
	padding: 0 20px;
	font-size: 16px;
	opacity: 0.6;
	cursor: pointer;

}

.custom-template .custom-template-close:hover {
	opacity: 1;
  
}

.custom-template .custom-template-content {
	padding: 10px;
	flex: 1 0 auto;
}

.custom-template .custom-template-content .custom-template-title {
	letter-spacing: 1px;
	text-transform: uppercase;
	font-size: 20px;
	font-weight: 600;
}

.notiIndicator {
  flex: 1 1 auto;
  
  align-self: stretch;
}

.notiIndicator.success{
  background: #48c78e;
}

.notiIndicator.warning {
	background: #ffe08a;
}

.notiIndicator.info {
	background: #3e8ed0;
}

.notiIndicator.danger {
	background: #f14668;
}

.custom-template:active {
	opacity: 0.8;
}

.v-fade-left-enter-active,
.v-fade-left-leave-active,
.v-fade-left-move {
	transition: all 0.5s;
}

.v-fade-left-enter,
.v-fade-left-leave-to {
	opacity: 0;
	transform: translateX(-500px) scale(0.2);
}
</style>
<script>
import Header from "@/components/common/Header";
export default {
  components: {Header}
}
</script>