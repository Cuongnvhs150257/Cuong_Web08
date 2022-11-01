<template>
        <input type="text" class="item-input" :class="{ 'item-input-red': !inValue, 'item-input-green': !inValueChange }"   :value="modelValue" @input="handleInput" :tabindex="tab" :maxlength="maxlength">
</template>
<style>

    .item-input{
        height: 28px;
        width: 100%;
        border: 1px solid black;
        margin-top: 5px;
        outline: none;
        padding-left: 10px; 
        border-radius: 3px;
    }
    .item-input:focus{
        border: 1px solid #50B83C;
        outline: none;
    }

    
</style>

<script>

export default {
    
    props:{
        modelValue: String,
        toolTip: String,
        inValue: {
            type: Boolean,
            default: true,
        },
        tab: Number,
        refs: String,
        maxlength: Number,
    },
    methods:{
         /**
         * hàm lưu giá trị nhập
         * Nguyễn Văn Cương 26/09/2022
         */
        handleInput(event){
            this.inValueChange = this.inValue;
            this.$emit("update:modelValue",event.target.value);
        }
    },
    data(){
        return{
            inValueChange: {
                type: Boolean,
                default: true,
            }
        }
    }

    
}
</script>