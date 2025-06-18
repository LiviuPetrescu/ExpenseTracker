<template>
  <div class="min-h-screen flex flex-col bg-background text-text dark:bg-background-dark dark:text-text-dark">
    <div class="max-w-[1280px] mx-auto p-4 w-full">
      <!-- Header -->
      <div class="flex items-center space-x-3 mb-6">
        <img src="/expense.png" alt="Logo" class="h-10 w-10" />
        <h1 class="text-2xl sm:text-3xl font-bold text-heading dark:text-heading-dark">
          Expense Tracker
        </h1>
      </div>

      <!-- Navigation Bar (to be created next) -->
<TheNavBar class="mb-6" />

      <!-- Dynamic page content -->
      <router-view />
    </div>
  </div>
</template>

<script setup>
import TheNavBar from './components/layout/TheNavBar.vue'
</script>
