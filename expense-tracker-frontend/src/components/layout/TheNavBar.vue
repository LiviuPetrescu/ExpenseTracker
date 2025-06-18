<template>
  <nav class="bg-blue-600 p-3 rounded-xl flex space-x-4">
    <router-link to="/home">
      <BaseButton variant="nav" :active="isActive('/home')"> All Expenses </BaseButton>
    </router-link>

    <router-link to="/add">
      <BaseButton variant="nav" :active="isActive('/add')"> Add Expense </BaseButton>
    </router-link>

    <router-link to="/charts">
      <BaseButton variant="nav" :active="isActive('/charts')"> Charts </BaseButton>
    </router-link>
  </nav>
</template>

<script setup>
import { useRoute } from 'vue-router'
import BaseButton from '@/components/base/BaseButton.vue'

const route = useRoute()
const isActive = (path) => route.path === path
</script>
