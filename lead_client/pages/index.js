import Head from 'next/head'
import Image from 'next/image'
import {useRouter} from "next/router";
import {useSession} from "next-auth/react";
import {useEffect} from "react";

export default function Home() {
  const router = useRouter();

  useEffect(()=>{
    router.push('/file-manager')
  },[])

  return (
    <div>

    </div>
  )
}
