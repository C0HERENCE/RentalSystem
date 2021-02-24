<template>
  <div>
    <h1>个人资料</h1>
    <b-textarea :model="description"></b-textarea>
    <b-button @click="edit_profile" :disabled="disabled">修改</b-button>
  </div>
</template>

<script>
export default {
name: "PersonalInfo",
  data() {
    return {
      description: "",
      disabled: false
    }
  },
  methods: {
    edit_profile() {
      this.disabled = true;
      this.$http.put("/user", {
        description: this.description
      }).then(response=>{
        this.$bvToast.toast(response.data.result);
      }).finally(() => {
        this.disabled = false;
      })
    }
  },
}
</script>

<style scoped>

</style>