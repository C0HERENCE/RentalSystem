<template>
  <div class="container">
    <h1>个人中心</h1>
    <b-row>
      <b-list-group class="col-sm-3 col-xl-2">
        <b-list-group-item>
          <router-link to="/profile/personal">个人资料</router-link>
        </b-list-group-item>
        <b-list-group-item>
          <router-link to="/profile/reset">修改密码</router-link>
        </b-list-group-item>
        <b-list-group-item>
          <router-link to="/profile/goods">发布的商品</router-link>
        </b-list-group-item>
      </b-list-group>
      <router-view class="col-sm-9 col-xl-10"></router-view>
    </b-row>
  </div>
</template>
