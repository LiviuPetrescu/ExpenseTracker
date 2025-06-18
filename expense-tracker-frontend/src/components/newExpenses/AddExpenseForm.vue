<template>
  <form
    @submit.prevent="handleSubmit"
    class="mb-4 p-3 sm:p-5 border border-border rounded-lg shadow bg-background-soft dark:bg-background-soft-dark space-y-3 max-w-xl mx-auto text-sm"
  >
    <!-- Form Grid -->
    <div class="grid grid-cols-1 sm:grid-cols-2 gap-3">
      <!-- Category -->
      <CategorySelect v-model="form.category" :categories="categories" />

      <!-- Title -->
      <BaseInput v-model="form.title" label="Title" required />

      <!-- Amount -->
      <BaseInput v-model="form.amount" type="number" label="Amount (RON)" required />

      <!-- Date (FlatPickr) -->
      <DatePicker v-model="form.date" label="Date" />
    </div>

    <!-- Notes -->
    <BaseInput v-model="form.notes" label="Notes" :multiline="true" />

    <!-- Actions -->
    <FormActions :isEditMode="isEditMode" @cancel="cancelEdit" />
  </form>
</template>

<script setup>
import { ref, watch, onMounted } from 'vue'
import FlatPickr from 'vue-flatpickr-component'
import 'flatpickr/dist/flatpickr.css'
import axios from 'axios'
import CategorySelect from '@/components/newExpenses/CategorySelect.vue'
import DatePicker from '@/components/newExpenses/DatePicker.vue'
import FormActions from '@/components/newExpenses/FormActions.vue'
import BaseInput from '@/components/base/BaseInput.vue'

const emit = defineEmits(['expense-added', 'edit-finished'])
const props = defineProps({ editData: Object })

const emptyForm = {
  id: null,
  title: '',
  amount: null,
  category: '',
  date: '',
  notes: '',
}

const form = ref({ ...emptyForm })
const isEditMode = ref(false)
const categories = ref([])

const fetchCategories = async () => {
  try {
    const { data } = await axios.get('http://localhost:5079/api/expenses/categories')
    categories.value = data
  } catch (e) {
    console.error('Failed to fetch categories:', e)
  }
}

onMounted(fetchCategories)

watch(
  () => props.editData,
  (newVal) => {
    if (newVal) {
      const [day, month, year] = newVal.date.split('-')
      form.value = {
        ...newVal,
        date: `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`,
      }
      isEditMode.value = true
    }
  },
  { immediate: true },
)

const handleSubmit = async () => {
  try {
    let formattedDate = form.value.date
    if (formattedDate.includes('-') && formattedDate.split('-')[0].length === 2) {
      const [day, month, year] = formattedDate.split('-')
      formattedDate = `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`
    }

    const payload = { ...form.value, date: formattedDate }

    if (isEditMode.value && form.value.id) {
      await axios.put(`http://localhost:5079/api/expenses/${form.value.id}`, payload)
      emit('expense-added', 'Expense updated successfully! ✅')
      emit('edit-finished')
    } else {
      await axios.post('http://localhost:5079/api/expenses', payload)
      emit('expense-added', 'Expense added successfully! ✅')
    }

    resetForm()
  } catch (e) {
    console.error('Failed to submit expense:', e)
    alert('Failed to save expense.')
  }
}

const cancelEdit = () => {
  resetForm()
  emit('edit-finished')
}

const resetForm = () => {
  form.value = { ...emptyForm }
  isEditMode.value = false
}
</script>
