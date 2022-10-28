<template>
  <button type="button" class="content-top-btn-agree">
  Có
  </button>
</template>

<style>
.content-top-btn-agree {
  height: 38px;
  padding: 0 21px;
  border: none;
  font-size: 15px;
  text-align: center;
  background-color: #29A21A;
  color: #ffff;
  border-radius: 4px;
  cursor: pointer;
  width: 60px;
  position: absolute;
  right: 0;
  margin-top: 20px;
  margin-right: 35px;
}
</style>

<script>
export default {
  emit: ["custom-handle-click"],
  methods: {
    
  },
};
</script>